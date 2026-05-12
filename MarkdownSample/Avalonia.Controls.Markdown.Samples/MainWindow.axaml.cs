using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Svg;

namespace Avalonia.Controls.Samples;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var baseDirectory = AppContext.BaseDirectory;

        var assetsPath = Path.Combine(baseDirectory, "Assets");

        if (!Directory.Exists(assetsPath))
        {
            return;
        }

        var files = Directory.EnumerateFiles(assetsPath).Where(path => Path.GetExtension(path) == ".md");

        foreach (var file in files)
        {
            tabControl.Items.Add(new TabItem
            {
                Header = Path.GetFileNameWithoutExtension(file),
                Content = new Markdown { Text = File.ReadAllText(file) }
            });
        }
    }
}

public class CustomImageLoader : MarkdownImageLoader
{
    public override async Task<IImage?> LoadImageAsync(string url)
    {
        IImage? image = null;

        if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            Stream? stream = null;

            if (uri.Scheme == "http" || uri.Scheme == "https")
            {
                stream = await DownloadImage(uri);
            }
            else if (uri.Scheme == "file" && File.Exists(uri.LocalPath))
            {
                stream = File.OpenRead(uri.LocalPath);
            }

            if (stream is null)
            {
                return null;
            }

            using (stream)
            {
                if (IsSvgFile(stream))
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        var svg = await reader.ReadToEndAsync();

                        var svgImage = new SvgImage
                        {
                            Source = SvgSource.LoadFromSvg(svg)
                        };

                        image = svgImage;
                    }
                }
                else
                {
                    image = new Bitmap(stream);
                }
            }
        }

        return image;
    }

    internal static async Task<Stream> DownloadImage(Uri? url)
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync(url).ConfigureAwait(false);
        using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream).ConfigureAwait(false);

        memoryStream.Position = 0;

        return memoryStream;
    }

    private static bool IsSvgFile(Stream? stream)
    {
        if (stream is null)
        {
            return false;
        }

        try
        {
            if (stream.Length == 0)
            {
                return false;
            }

            const int bufferSize = 512;

            byte[] buffer = new byte[Math.Min(bufferSize, stream.Length)];

            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            string header = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            return header.Contains("<svg", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
        finally
        {
            stream.Position = 0;
        }
    }
}

using System;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Documents.Serialization;
using Avalonia.Controls.Documents.Serialization.Docx;
using Avalonia.Controls.Documents.Serialization.PlainText;
using Avalonia.Controls.Documents.Serialization.Rtf;
using Avalonia.Controls.Documents.Serialization.Snapshot;
using Avalonia.Controls.Documents.Serialization.Xaml;
using Avalonia.Controls.Documents.TextModel;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

namespace RichTextEditor.Demo;

public partial class MainWindow : Window
{
    private string _currentFileName = "Untitled";

    public MainWindow()
    {
        InitializeComponent();
        Editor.Loaded += Editor_Loaded;
    }

    private void Editor_Loaded(object? sender, RoutedEventArgs e)
    {
        SubscribeToEditorEvents();
        UpdateStatusBar();
        UpdateTitle();
    }

    private void SubscribeToEditorEvents()
    {
        Editor.SelectionChanged += (_, _) => UpdateStatusBar();
        Editor.ContentChanged += (_, _) => UpdateStatusBar();
    }

    private void UpdateStatusBar()
    {
        var textDoc = Editor.Document.TextDocument;
        if (textDoc == null)
        {
            WordCountText.Text = "Words: 0";
            CharCountText.Text = "Characters: 0";
            CursorPositionText.Text = "Offset: 0";
            return;
        }

        int totalLength = textDoc.Length;
        CharCountText.Text = $"Characters: {totalLength}";

        // Word count: enumerate the snapshot's text runs and treat block boundaries
        // (paragraphs, list items, table cells, line breaks, ...) as word separators.
        // This avoids allocating a full plain-text string and also avoids merging
        // the last word of one block with the first word of the next.
        var snapshot = textDoc.CreateSnapshot();
        int wordCount = CountWords(snapshot);
        WordCountText.Text = $"Words: {wordCount}";

        // Cursor offset
        var selection = Editor.Selection;
        if (selection != null)
        {
            int offset = selection.CaretPosition.Offset;
            if (selection.IsEmpty)
            {
                CursorPositionText.Text = $"Offset: {offset}";
            }
            else
            {
                CursorPositionText.Text = $"Selection: {selection.Length} chars";
            }
        }
    }

    private static int CountWords(DocumentSnapshot snapshot)
    {
        int count = 0;
        bool inWord = false;

        foreach (var node in snapshot.EnumerateNodes())
        {
            var kind = node.Kind;
            if (kind == TextDocumentNodeKind.Run)
            {
                int remaining = node.Length;
                if (remaining == 0)
                    continue;

                int pos = node.StartOffset;
                while (remaining > 0)
                {
                    var chunk = snapshot.GetTextMemory(pos, remaining);
                    if (chunk.IsEmpty)
                        break;

                    var span = chunk.Span;
                    for (int i = 0; i < span.Length; i++)
                    {
                        if (char.IsWhiteSpace(span[i]))
                        {
                            inWord = false;
                        }
                        else if (!inWord)
                        {
                            inWord = true;
                            count++;
                        }
                    }

                    pos += chunk.Length;
                    remaining -= chunk.Length;
                }
            }
            else if (kind == TextDocumentNodeKind.LineBreak
                || (kind.Flags & NodeKindFlags.Block) != 0)
            {
                // Block boundary or hard line break — never let a word straddle it.
                inWord = false;
            }
        }

        return count;
    }

    private void UpdateTitle()
    {
        Title = $"{_currentFileName} - RichTextEditor Demo";
    }

    private void NewDocument_Click(object? sender, RoutedEventArgs e)
    {
        Editor.Document = new FlowDocument();
        _currentFileName = "Untitled";
        UpdateTitle();
        UpdateStatusBar();
    }

    private async void OpenRtf_Click(object? sender, RoutedEventArgs e)
    {
        await OpenFileAsync("Open RTF Document", new RtfSerializer(),
            new FilePickerFileType("RTF Documents") { Patterns = ["*.rtf"] });
    }

    private async void OpenDocx_Click(object? sender, RoutedEventArgs e)
    {
        await OpenFileAsync("Open DOCX Document", new DocxSerializer(),
            new FilePickerFileType("Word Documents") { Patterns = ["*.docx"] });
    }

    private async void OpenXaml_Click(object? sender, RoutedEventArgs e)
    {
        await OpenFileAsync("Open XAML Document", new XamlSerializer(),
            new FilePickerFileType("XAML Documents") { Patterns = ["*.axaml", "*.xaml"] });
    }

    private async void OpenText_Click(object? sender, RoutedEventArgs e)
    {
        await OpenFileAsync("Open Text Document", new PlainTextSerializer(),
            new FilePickerFileType("Text Files") { Patterns = ["*.txt"] });
    }

    private async void SaveRtf_Click(object? sender, RoutedEventArgs e)
    {
        await SaveFileAsync("Save RTF Document", "rtf", new RtfSerializer(),
            new FilePickerFileType("RTF Documents") { Patterns = ["*.rtf"] });
    }

    private async void SaveDocx_Click(object? sender, RoutedEventArgs e)
    {
        await SaveFileAsync("Save DOCX Document", "docx", new DocxSerializer(),
            new FilePickerFileType("Word Documents") { Patterns = ["*.docx"] });
    }

    private async void SaveXaml_Click(object? sender, RoutedEventArgs e)
    {
        await SaveFileAsync("Save XAML Document", "axaml", new XamlSerializer(),
            new FilePickerFileType("XAML Documents") { Patterns = ["*.axaml"] });
    }

    private async void SaveText_Click(object? sender, RoutedEventArgs e)
    {
        await SaveFileAsync("Save Text Document", "txt", new PlainTextSerializer(),
            new FilePickerFileType("Text Files") { Patterns = ["*.txt"] });
    }

    private async Task OpenFileAsync(string title, IDocumentSerializer serializer, FilePickerFileType fileType)
    {
        var storage = TopLevel.GetTopLevel(this)?.StorageProvider;
        if (storage == null)
            return;

        var files = await storage.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            FileTypeFilter = [fileType, new FilePickerFileType("All Files") { Patterns = ["*.*"] }]
        });

        if (files.Count == 0)
            return;

        try
        {
            await using var stream = await files[0].OpenReadAsync();
            Editor.Load(stream, serializer);
            _currentFileName = files[0].Name;
            UpdateTitle();
            UpdateStatusBar();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    private async Task SaveFileAsync(string title, string defaultExtension, IDocumentSerializer serializer, FilePickerFileType fileType)
    {
        var storage = TopLevel.GetTopLevel(this)?.StorageProvider;
        if (storage == null)
            return;

        var file = await storage.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            DefaultExtension = defaultExtension,
            FileTypeChoices = [fileType]
        });

        if (file == null)
            return;

        try
        {
            await using var stream = await file.OpenWriteAsync();
            Editor.Save(stream, serializer);
            _currentFileName = file.Name;
            UpdateTitle();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    private void Undo_Click(object? sender, RoutedEventArgs e)
    {
        if (Editor.CanUndo)
            Editor.Undo();
    }

    private void Redo_Click(object? sender, RoutedEventArgs e)
    {
        if (Editor.CanRedo)
            Editor.Redo();
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}

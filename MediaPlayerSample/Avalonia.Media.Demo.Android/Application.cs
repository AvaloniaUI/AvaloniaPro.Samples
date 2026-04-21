using Android.App;
using Android.Runtime;
using Avalonia.Android;
using Avalonia.Vulkan;

namespace Avalonia.Media.Demo.Android
{
    [Application]
    public class Application : AvaloniaAndroidApplication<App>
    {
        protected Application(nint javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            return base.CustomizeAppBuilder(builder)
                .UseAndroidPlayer(this)
                .With(new VulkanOptions()
                {
                    VulkanDeviceCreationOptions = new VulkanDeviceCreationOptions()
                    {
                        DeviceExtensions = new[] { "VK_ANDROID_external_memory_android_hardware_buffer", "VK_EXT_queue_family_foreign" }
                    }
                })
                .WithInterFont()
                .LogToTrace();
        }
    }
}

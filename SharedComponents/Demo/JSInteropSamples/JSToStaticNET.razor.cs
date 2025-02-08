using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace SharedComponents.Demo.JSInteropSamples
{
    [SupportedOSPlatform("browser")]
    public partial class JSToStaticNET
    {
        [JSExport]
        internal static string GetMessageFromNET()
        {
            return ".NET";
        }

        [JSImport("showMessage", "jstonet")]
        internal static partial void ShowMessage();
    }
}

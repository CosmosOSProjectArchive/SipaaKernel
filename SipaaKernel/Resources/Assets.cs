using IL2CPU.API.Attribs;
using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel
{
    public class Assets
    {
        public const string Base = "SipaaKernel.Resources.";
        [ManifestResourceStream(ResourceName = Base + "wallpaper.bmp")] public readonly static byte[] WallpaperB;
        [ManifestResourceStream(ResourceName = Base + "cursor.bmp")] public readonly static byte[] CursorB;
        [ManifestResourceStream(ResourceName = Base + "skboot.bmp")] public readonly static byte[] BootB;
        [ManifestResourceStream(ResourceName = Base + "startupv1.wav")] public readonly static byte[] StartupWave;
        [ManifestResourceStream(ResourceName = Base + "kernelpanic.bmp")] public readonly static byte[] KernelPanicBmp;

        // Images
        public static Graphics Wallpaper = Image.FromBitmap(WallpaperB);
        public static Graphics BootBitmap = Image.FromBitmap(BootB);
        public static Graphics Cursor = Image.FromBitmap(CursorB);
        public static Graphics KernelPanicBitmap = Image.FromBitmap(KernelPanicBmp);
    }
}

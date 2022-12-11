using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.System.GraphicsExtensions
{
    /// <summary>
    /// PC Screen Font renderer for Terminal.cs's Graphics 
    /// </summary>
    public static class PSFRenderer
    {
        /// <summary>
        /// Draw string with the PCScreenFont renderer.
        /// </summary>
        /// <param name="str">string to draw.</param>
        /// <param name="aFont">Font used.</param>
        /// <param name="color">Color.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public static void DrawPSFString(this PrismGL2D.Graphics g, int x, int y, string str, Font aFont, PrismGL2D.Color color)
        {
            for (int i = 0; i < str.Length; i++)
            {
                g.DrawPSFChar(x, y, str[i], aFont, color);
                x += aFont.Width;
            }
        }


        /// <summary>
        /// Draw char with the PCScreenFont renderer.
        /// </summary>
        /// <param name="str">char to draw.</param>
        /// <param name="aFont">Font used.</param>
        /// <param name="pen">Color.</param>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public static void DrawPSFChar(this PrismGL2D.Graphics g, int x, int y, char c, Font aFont, PrismGL2D.Color color)
        {
            int p = aFont.Height * (byte)c;

            for (int cy = 0; cy < aFont.Height; cy++)
            {
                for (byte cx = 0; cx < aFont.Width; cx++)
                {
                    if (aFont.ConvertByteToBitAddres(aFont.Data[p + cy], cx + 1))
                    {
                        g[(ushort)(x + (aFont.Width - cx)), (ushort)(y + cy)] = color;
                    }
                }
            }
        }
    }
}

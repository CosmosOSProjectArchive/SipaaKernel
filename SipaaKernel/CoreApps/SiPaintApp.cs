using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;
using Cosmos.System;
using PrismGL2D;
using PrismGL3D.Types;
using SipaaKernel.Core;
using SipaaKernel.UI;

namespace SipaaKernel.CoreApps
{
    internal class SiPaintApp : Application
    {
        Color CurrentColor = Color.White;

        public override void AppMain()
        {
            var h = WindowManager.CreateWindow(new WindowOptions { Title = "SiPaint (beta)", Width = 250, Height = 250, X = VBE.getModeInfo().width / 2 - 250 / 2, Y = VBE.getModeInfo().height / 2 - 250 / 2 }, true);
            var w = WindowManager.WindowFromHandle(h);

            var paintGraphics = new Graphics(w.Width, w.Height - Window.TitleBarHeight);

            w.OnDraw = (g) =>
            {
                g.DrawImage(w.X, w.Y + (int)Window.TitleBarHeight, paintGraphics, true);
            };

            w.OnUpdate = () =>
            {
                if (MouseManager.X > w.X && MouseManager.X < w.X + w.Width && MouseManager.Y > w.Y && MouseManager.Y < w.Y + w.Height && MouseManager.MouseState == MouseState.Left)
                {
                    var x = MouseManager.X - w.X;
                    var y = MouseManager.Y - Window.TitleBarHeight - w.Y;

                    paintGraphics[(int)x, (int)y] = CurrentColor;
                }
            };
        }

        public override void InitAppInfo()
        {
            this.ApplicationIcon = Assets.SiPaintAppLogo;
            this.ApplicationDisplayName = "SiPaint";
            this.ApplicationName = "SipaaPaint";
            this.ApplicationPackageName = "fr.raphmar2019.sipaa.paint";
        }
    }
}

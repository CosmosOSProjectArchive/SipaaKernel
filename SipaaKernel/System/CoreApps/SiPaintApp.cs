using Cosmos.Core;
using Cosmos.System;
using SipaaGL;
using SipaaKernel.Core;
using SipaaKernel.UI;
using SipaaKernel.UI.Widgets;

namespace SipaaKernel.System.CoreApps
{
    internal class SiPaintApp : Application
    {
        Color CurrentColor = Color.White;

        public override void AppMain()
        {
            var h = WindowManager.CreateWindow(new WindowOptions { Title = "SiPaint (beta)", Width = 250, Height = 250, X = VBE.getModeInfo().width / 2 - 250 / 2, Y = VBE.getModeInfo().height / 2 - 250 / 2 }, true);
            var w = WindowManager.WindowFromHandle(h);
            var m = new MenuBar() { X = w.X, Y = w.Y + (int)Window.TitleBarHeight, Width = w.Width, Height = 24, IsAccentued = true };

            m.AddMenuButton("File");
            m.AddMenuButton("Colors");
            m.AddMenuButton("Help");


            var paintGraphics = new Graphics(w.Width, w.Height - Window.TitleBarHeight - m.Height);

            w.Widgets.Add(m);

            w.OnDraw = (g) =>
            {
                g.DrawImage(w.X, w.Y + (int)Window.TitleBarHeight + (int)m.Height, paintGraphics, true);
            };

            w.OnUpdate = () =>
            {
                // Update the menu bar infos
                m.X = w.X; m.Y = w.Y + (int)Window.TitleBarHeight;

                // Draw pixels on the graphics
                if (CursorUtil.IsCursorOnRectangle(w.X, w.Y + (int)Window.TitleBarHeight + (int)m.Height, paintGraphics.Width, paintGraphics.Height) && MouseManager.MouseState == MouseState.Left)
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

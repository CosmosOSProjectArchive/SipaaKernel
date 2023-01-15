using Cosmos.Core;
using SipaaGL;
using SipaaKernel.Core;
using SipaaKernel.UI;
using SipaaKernel.UI.Widgets;
using System.Collections.Generic;

namespace SipaaKernel
{
    public class SKDE
    {
        private List<Button> applicationButtons;
        private int currentX = 0;

        public SKDE()
        {
            applicationButtons = new();
        }

        public void AddAppToLauncher(Application app)
        {
            Button btn = new();

            btn.OnClick = (x, y) => { app.AppMain(); };
            btn.Height = 40;
            btn.Width = 40;
            btn.X = currentX;
            btn.Y = (int)VBE.getModeInfo().height - (int)btn.Height;

            if (app.ApplicationIcon != null)
            {
                btn.Image = app.ApplicationIcon;
            }
            else
            {
                btn.Text = "" + app.ApplicationName.ToUpper()[0] + app.ApplicationName.ToUpper()[1];
            }

            applicationButtons.Add(btn);
        }

        public void Draw(Graphics g)
        {
            // Draw hour notch
            g.DrawFilledRectangle((int)g.Width / 2 - 50 / 2, 14, 50, 24, 4, UI.SysTheme2.ThemeManager.GetCurrentTheme().GetComponentBackgroundColor().RemoveAlphaIfIsVM());
            g.DrawStringBF((int)g.Width / 2, 14 + 24 / 2, $"{Cosmos.HAL.RTC.Hour}:{Cosmos.HAL.RTC.Minute}", BitFont.Fallback, UI.SysTheme2.ThemeManager.GetCurrentTheme().GetForegroundColor(), true);

            // Draw launcher

            //g.DrawFilledRectangle(0, (int)g.Height - 40, g.Width, 40, 0, GetCurrentTheme().GetComponentBackgroundColor().RemoveAlphaIfIsVM()); This line brick SipaaKernel, so don't uncomment it

            foreach (Button appButton in applicationButtons)
            {
                appButton.OnDraw(g);
            }
        }

        public void Update()
        {
            foreach (Button appButton in applicationButtons)
            {
                appButton.OnUpdate();
            }
        }
    }
}

using Cosmos.Core;
using Cosmos.System.FileSystem.VFS;
using SipaaGL;
using SipaaGL.Extentions;
using SipaaKernel.Core;
using SipaaKernel.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel
{
    /**
    This class is useless, for the moment.
    public class Setup
    {
        int rectX = VBE.getModeInfo().width / 2 - 500 / 2;
        int rectY = VBE.getModeInfo().height / 2 - 500 / 2;
        uint rectSize = 500;
        Button installBtn;
        bool isInstalled = false;

        public Setup()
        {
            installBtn = new();
            installBtn.Text = "Install Now";
            installBtn.Width = 150;
            installBtn.Height = 40;
            installBtn.X = rectX + (int)rectSize - 10 - (int)installBtn.Width;
            installBtn.Y = rectY + (int)rectSize - 10 - (int)installBtn.Height;
        }

        public void Draw(Graphics g)
        {
            g.DrawFilledRectangle(rectX, rectY, rectSize, rectSize, 0, SipaaKernel.UI.SysTheme2.ThemeManager.GetCurrentTheme().GetComponentBackgroundColor());
            g.DrawString(rectX + 10, rectY + 10, "SipaaKernel is not installed, Press 'Install now' to install SipaaKernel", Font.Fallback, SipaaKernel.UI.SysTheme2.ThemeManager.GetCurrentTheme().GetForegroundColor());
            installBtn.OnDraw(g);
        }

        public void Update(VBECanvas g)
        {
            installBtn.OnUpdate();
            if (installBtn.CurrentWidgetState == UI.WidgetState.Clicked)
            {
                g.Clear(Color.Black);
                g.DrawString(10, 10, "Installing SipaaKernel, Please wait...", Font.Fallback, Color.White);
                g.Update();
                VFSManager.GetDisks()[0].FormatPartition(0, "FAT", true);
                SipaaKernelInstallationManager.Install();
                Cosmos.HAL.Global.PIT.Wait(10000);
                g.Clear(Color.Black);
                g.DrawString(10, 10, "SipaaKernel is installed! Press any key to reboot", Font.Fallback, Color.White);
                g.Update();
                Console.ReadKey();
                Cosmos.System.Power.Reboot();
            }
        }
    }**/
}

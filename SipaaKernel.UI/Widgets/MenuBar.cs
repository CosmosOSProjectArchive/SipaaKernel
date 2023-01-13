using SipaaGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.Widgets
{
    public class MenuBar : Widget
    {
        public uint MebuButtonWidth { get { return menuButtonWidth; } set { UpdateButtonsWidth(value); menuButtonWidth = value; } }

        private void UpdateButtonsWidth(uint value)
        {
            foreach (Button b in MenuButtons)
            {
                b.Width = value;
            }
        }

        public List<Button> MenuButtons = new List<Button>();
        private uint currentX = 0;
        private uint menuButtonWidth = 50;

        public void AddMenuButton(string text)
        {
            var btn = new Button();
            btn.Width = menuButtonWidth;
            btn.Height = this.Height;
            btn.X = this.X + (int)currentX;
            btn.Y = this.Y;
            btn.OverrideBorderRadius = this.OverrideBorderRadius;
            btn.CornerRadius = this.CornerRadius;
            btn.IsAccentued = this.IsAccentued;
            btn.Text = text;
            btn.OnClick = (x, y) =>
            {
                // Not implemented currently
            };
            currentX += btn.Width;
            MenuButtons.Add(btn);
        }

        public void UpdateButtons(Button btn)
        {
            btn.X = this.X + (int)currentX;
            btn.Y = this.Y;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            currentX = 0;
            foreach (Button menuButton in MenuButtons)
            {
                UpdateButtons(menuButton);
                currentX += menuButton.Width;
                menuButton.OnUpdate();
            }
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);
            foreach (Button menuButton in MenuButtons)
            {
                menuButton.OnDraw(g);
            }
        }
    }
}

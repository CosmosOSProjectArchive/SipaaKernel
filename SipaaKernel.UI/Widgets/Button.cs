using Cosmos.System;
using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SipaaKernel.UI.SysTheme2.ThemeManager;

namespace SipaaKernel.UI.Widgets
{
    public class Button : Widget
    {
        MouseState LastMouseState;
        public Graphics Image { get; set; }

        public override Graphics RenderWidget()
        {
            Graphics _Graphics = new(Width, Height);
            _Graphics.Clear(Color.Transparent);
            if (IsAccentued)
            {
                if (OverrideBorderRadius)
                {
                    _Graphics.DrawFilledRectangle(0, 0, Width, Height, CornerRadius, GetCurrentTheme().GetAccentBackgroundColor(CurrentWidgetState));
                }
                else
                {
                    _Graphics.DrawFilledRectangle(0, 0, Width, Height, (uint)GetCurrentTheme().GetBorderRadius(), GetCurrentTheme().GetAccentBackgroundColor(CurrentWidgetState));
                }

                if (Image != null && String.IsNullOrEmpty(Text))
                {
                    _Graphics.DrawImage((int)_Graphics.Width / 2 - (int)Image.Width / 2, (int)_Graphics.Height / 2 - (int)Image.Height / 2, Image, true);
                }else if (Image != null && !String.IsNullOrEmpty(Text))
                {
                    _Graphics.DrawImage(8, (int)_Graphics.Height / 2 - (int)Image.Height / 2, Image, true);
                    _Graphics.DrawString((int)Width / 2, (int)Height / 2, Text, Font.Fallback, GetCurrentTheme().GetAccentForegroundColor(), true);
                }
                else if (Image == null)
                {
                    _Graphics.DrawString((int)Width / 2, (int)Height / 2, Text, Font.Fallback, GetCurrentTheme().GetAccentForegroundColor(), true);
                }
            }
            else
            {
                if (OverrideBorderRadius)
                {
                    _Graphics.DrawFilledRectangle(0, 0, Width, Height, CornerRadius, GetCurrentTheme().GetWidgetBackgroundColor(CurrentWidgetState));
                }
                else
                {
                    _Graphics.DrawFilledRectangle(0, 0, Width, Height, (uint)GetCurrentTheme().GetBorderRadius(), GetCurrentTheme().GetWidgetBackgroundColor(CurrentWidgetState));
                }
                if (Image != null && String.IsNullOrEmpty(Text))
                {
                    _Graphics.DrawImage((int)_Graphics.Width / 2 - (int)Image.Width / 2, (int)_Graphics.Height / 2 - (int)Image.Height / 2, Image, true);
                }
                else if (Image != null && !String.IsNullOrEmpty(Text))
                {
                    _Graphics.DrawImage(8, (int)_Graphics.Height / 2 - (int)Image.Height / 2, Image, true);
                    _Graphics.DrawString((int)Width / 2, (int)Height / 2, Text, Font.Fallback, GetCurrentTheme().GetForegroundColor(), true);
                }
                else if (Image == null)
                {
                    _Graphics.DrawString((int)Width / 2, (int)Height / 2, Text, Font.Fallback, GetCurrentTheme().GetForegroundColor(), true);
                }
            }
            return _Graphics;
        }

        public override void OnUpdate()
        {
            if (this.CurrentWidgetState == WidgetState.Clicked && MouseManager.MouseState != LastMouseState)
                if (OnClick != null)
                    OnClick.Invoke((int)MouseManager.X, (int)MouseManager.Y);
            LastMouseState = MouseManager.MouseState;
            base.OnUpdate();
        }
    }
}

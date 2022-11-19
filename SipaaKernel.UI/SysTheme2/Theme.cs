using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismGL2D;

namespace SipaaKernel.UI.SysTheme2
{
    public abstract class Theme
    {
        #region Properties
        protected ColorDictionnary ColorDictionnary;
        protected int BorderRadius = 0;
        protected bool CenterWindowTitle = false;

        protected abstract ColorDictionnary InitColorDictionnary();
        #endregion

        public Theme()
        {
            ColorDictionnary = InitColorDictionnary();
        }

        public Color GetAccentBackgroundColor(WidgetState state)
        {
            switch (state)
            {
                case WidgetState.Idle:
                    return ColorDictionnary.AccentIdle.RemoveAlphaIfIsVM();
                case WidgetState.Hovered:
                    return ColorDictionnary.AccentHover.RemoveAlphaIfIsVM();
                case WidgetState.Clicked:
                    return ColorDictionnary.AccentClicked.RemoveAlphaIfIsVM();
            }
            return null;
        }
        public Color GetAccentForegroundColor()
        {
            return ColorDictionnary.AccentForeground;
        }
        public Color GetWidgetBackgroundColor(WidgetState state)
        {
            switch (state)
            {
                case WidgetState.Idle:
                    return ColorDictionnary.WidgetIdle.RemoveAlphaIfIsVM();
                case WidgetState.Hovered:
                    return ColorDictionnary.WidgetHover.RemoveAlphaIfIsVM();
                case WidgetState.Clicked:
                    return ColorDictionnary.WidgetClicked.RemoveAlphaIfIsVM();
            }
            return null;
        }
        public Color GetWindowBackgroundColor()
        {
            return ColorDictionnary.WindowBackground.RemoveAlphaIfIsVM();
        }
        public Color GetComponentBackgroundColor()
        {
            return ColorDictionnary.Component.RemoveAlphaIfIsVM();
        }
        public Color GetForegroundColor()
        {
            return ColorDictionnary.Foreground.RemoveAlphaIfIsVM();
        }

        public ColorDictionnary GetColorDictionnary()
        {
            return ColorDictionnary;
        }

        public int GetBorderRadius()
        {
            return BorderRadius;
        }
        public bool IsWindowTitleCentered()
        {
            return CenterWindowTitle;
        }
    }
}

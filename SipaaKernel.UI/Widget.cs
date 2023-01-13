using Cosmos.System;
using SipaaGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SipaaKernel.UI.SysTheme2.ThemeManager;

namespace SipaaKernel.UI
{
    public class Widget
    {
        private int _X, _Y;
        private uint _Width, _Height;
        private Graphics _Graphics;
        private Action<Graphics> _OnDrawEvent;
        private Action _OnUpdateEvent;
        private Action<KeyEvent> _OnKeyEvent;
        private WidgetState state;

        public bool OverrideBorderRadius { get; set; } = false;
        public uint CornerRadius { get; set; } = 0;
        public int X { get => _X; set => _X = value; }
        public int Y { get => _Y; set => _Y = value; }
        public uint Width { get => _Width; set => _Width = value; }
        public uint Height { get => _Height; set => _Height = value; }
        public string Text { get; set; }
        public WidgetState CurrentWidgetState { get; internal set; } = WidgetState.Idle;

        public bool IsAccentued { get; set; } = false;

        public Action<Graphics> OnDrawEvent { get => _OnDrawEvent; set => _OnDrawEvent = value; }
        public Action OnUpdateEvent { get => _OnUpdateEvent; set => _OnUpdateEvent = value; }
        public Action<KeyEvent> OnKeyEvent { get => _OnKeyEvent; set => _OnKeyEvent = value; }
        public Action<int, int> OnClick { get; set; } = null;

        public virtual Graphics RenderWidget()
        {
            _Graphics = new(Width, Height);
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
            }
            return _Graphics;
        }

        public virtual void OnDraw(Graphics g)
        {
            g.DrawImage(X, Y, RenderWidget(), true);
            if (_OnDrawEvent != null)
                _OnDrawEvent.Invoke(g);
        }

        public virtual void OnUpdate()
        {
            if (MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + Height)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    CurrentWidgetState = WidgetState.Clicked;
                }
                else
                {
                    CurrentWidgetState = WidgetState.Hovered;
                }
            }
            else
            {
                CurrentWidgetState = WidgetState.Idle;
            }
            if (_OnUpdateEvent != null)
                _OnUpdateEvent.Invoke();
        }

        public virtual void OnKey(KeyEvent e)
        {
            if (_OnKeyEvent != null)
                _OnKeyEvent.Invoke(e);
        }
    }
}

using Cosmos.System;
using SipaaGL;
using SipaaKernel.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public class Window
    {
        // Window properties
        public int X { get; set; } = 125;
        public int Y { get; set; } = 125;
        public uint Width { get; set; } = 150;
        public uint Height { get; set; } = 150;
        public string Title { get; set; } = "Window";
        public const uint TitleBarHeight = 32;

        // Window style
        public bool EnableWindowFrame { get; set; } = true;
        public bool Visible { get; set; } = true;

        // Window content
        public List<Widget> Widgets = new();

        // Window identification
        public uint Handle { get; set; }

        // Window buttons
        private Button closeBtn;

        // Window dragging
        private bool IsWindowMoving;
        private bool pressed;
        private int px;
        private int py;
        private bool lck;

        // Events
        public Action<Graphics> OnDraw;
        public Action OnUpdate;

        // Constructor

        public Window()
        {
            Handle = (uint)new Random().Next(1, int.MaxValue);

            closeBtn = new();
            closeBtn.Text = "X";
            closeBtn.Width = 32;
            closeBtn.Height = 32;
            closeBtn.X = X + (int)Width - (int)closeBtn.Width;
            closeBtn.Y = Y;
            closeBtn.IsAccentued = true;

            WindowManager.Windows.Add(this);
        }

        // Methods

        public void Draw(Graphics g)
        {
            var t = SysTheme2.ThemeManager.GetCurrentTheme();

            if (Visible)
            {
                g.DrawFilledRectangle(X, Y, Width, Height, (uint)t.GetBorderRadius(), t.GetWindowBackgroundColor());

                if (EnableWindowFrame)
                {
                    g.DrawFilledRectangle(X, Y, Width, TitleBarHeight, (uint)t.GetBorderRadius(), t.GetAccentBackgroundColor(WidgetState.Idle));
                    g.DrawStringBF(X + (int)Width / 2, Y + (int)TitleBarHeight / 2, Title, BitFont.Fallback, t.GetAccentForegroundColor(), true);
                    closeBtn.OnDraw(g);
                }

                if (OnDraw != null) // NEVER INVOKE AN EVENT WITHOUT THIS LINE
                    OnDraw.Invoke(g);

                foreach (Widget w in Widgets)
                {
                    w.OnDraw(g);
                }
            }
        }

        public void Update()
        {
            if (Visible)
            {
                // Window dragging & title bar buttons
                if (EnableWindowFrame)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        if (!IsWindowMoving & MouseManager.X > X && MouseManager.X < X + Width && MouseManager.Y > Y && MouseManager.Y < Y + TitleBarHeight)
                        {
                            IsWindowMoving = true;

                            this.pressed = true;
                            if (!lck)
                            {
                                px = (int)((int)MouseManager.X - this.X);
                                py = (int)((int)MouseManager.Y - this.Y);
                                lck = true;
                            }
                        }
                    }
                    else
                    {
                        pressed = false;
                        lck = false;
                        IsWindowMoving = false;
                    }

                    if (pressed)
                    {
                        X = (int)MouseManager.X - px;
                        Y = (int)MouseManager.Y - py;
                    }


                    closeBtn.X = X + (int)Width - (int)closeBtn.Width;
                    closeBtn.Y = Y;
                    closeBtn.OnUpdate();
                }

                if (OnUpdate != null) // NEVER INVOKE AN EVENT WITHOUT THIS LINE
                    OnUpdate.Invoke();


                foreach (Widget w in Widgets)
                {
                    w.OnUpdate();
                }
            }
        }
    }
}

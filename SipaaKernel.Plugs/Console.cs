using Cosmos.System;
using sys = System;
using SipaaGL;
using SipaaGL.Extentions;
using Cosmos.Core;
using static Cosmos.HAL.Global;
using IL2CPU.API.Attribs;
using System.Collections.Generic;

namespace Cosmos.System.Plugs.System.System
{
    /// <summary>
	/// VBE Console class, used to write text output to a high resolution console.
	/// Supports images, shapes, and everything else.
	/// Not completed.
	/// </summary>
	[Plug(Target = typeof(global::System.Console))]
    public static unsafe class VBEConsole
    {
        #region Methods

        #region WriteLine

        public static void WriteLine(string Format, object Value1, object Value2, object Value3) => WriteLine(string.Format(Format, Value1, Value2, Value3));
        public static void WriteLine(string Format, object Value1, object Value2) => WriteLine(string.Format(Format, Value1, Value2));
        public static void WriteLine(string Format, params object[] Value) => WriteLine(string.Format(Format, Value));
        public static void WriteLine(char[] Value, int Index, int Count) => WriteLine(Value[Index..(Index + Count)]);
        public static void WriteLine(string Format, object Value) => WriteLine(string.Format(Format, Value));
        public static void WriteLine(string Value) => Write(Value + global::System.Environment.NewLine);
        public static void WriteLine(object Value) => WriteLine(Value ?? string.Empty);
        public static void WriteLine(char[] Value) => WriteLine(new string(Value));
        public static void WriteLine(double Value) => WriteLine(Value.ToString());
        public static void WriteLine(float Value) => WriteLine(Value.ToString());
        public static void WriteLine(ulong Value) => WriteLine(Value.ToString());
        public static void WriteLine(bool Value) => WriteLine(Value.ToString());
        public static void WriteLine(long Value) => WriteLine(Value.ToString());
        public static void WriteLine(uint Value) => WriteLine(Value.ToString());
        public static void WriteLine(int Value) => WriteLine(Value.ToString());
        public static void WriteLine() => Write(global::System.Environment.NewLine);

        /* Decimal type is not working yet... */
        //public static void WriteLine(decimal aDecimal) => WriteLine(aDecimal.ToString());

        #endregion

        #region ReadLine

        public static string ReadLine(bool RedirectOutput = false)
        {
            string Input = string.Empty;

            while (true)
            {
                if (KeyboardManager.TryReadKey(out KeyEvent Key))
                {
                    IsCursorEnabled = false;

                    switch (Key.Key)
                    {
                        case ConsoleKeyEx.Enter:
                            Write('\n');
                            IsCursorEnabled = true;
                            return Input;
                        case ConsoleKeyEx.Backspace:
                            if (Input.Length > 0)
                            {
                                if (X < SpacingX)
                                {
                                    Y -= (int)BitFont.Fallback.Size;
                                    X = (int)BitFont.Fallback.MeasureString(Buffer.Split('\n')[Y]);
                                }
                                else
                                {
                                    X -= (int)BitFont.Fallback.MeasureString(Buffer[^1].ToString());
                                }

                                Buffer = Buffer[..^1];
                                Input = Input[..^1];

                                Canvas.DrawFilledRectangle(X, Y, BitFont.Fallback.Size, BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
                                Canvas.Update();
                            }
                            IsCursorEnabled = true;
                            break;
                        default:
                            if (!char.IsControl(Key.KeyChar))
                            {
                                Input += Key.KeyChar;
                                if (!RedirectOutput)
                                {
                                    Write(Key.KeyChar);
                                }
                            }
                            IsCursorEnabled = true;
                            break;
                    }
                }
            }
        }
        public static string ReadLine()
        {
            return ReadLine(false);
        }

        #endregion

        #region Write

        public static void Write(string Format, object Value1, object Value2, object Value3) => Write(string.Format(Format, Value1, Value2, Value3));
        public static void Write(string Format, object Value1, object Value2) => Write(string.Format(Format, Value1, Value2));
        public static void Write(string Format, params object[] Value) => Write(string.Format(Format, Value));
        public static void Write(string Format, object Value) => Write(string.Format(Format, Value));
        public static void Write(object Value) => Write(Value ?? string.Empty);
        public static void Write(char[] Value) => Write(new string(Value));
        public static void Write(double Value) => Write(Value.ToString());
        public static void Write(float Value) => Write(Value.ToString());
        public static void Write(ulong Value) => Write(Value.ToString());
        public static void Write(long Value) => Write(Value.ToString());
        public static void Write(uint Value) => Write(Value.ToString());
        public static void Write(int Value) => Write(Value.ToString());
        public static void Write(char[] Value, int Index, int Count)
        {
            if ((Value.Length - Index) < Count)
            {
                throw new global::System.ArgumentException($"Specified count '{nameof(Count)}' is more than the buffer length.");
            }
            if (Value == null)
            {
                throw new global::System.ArgumentNullException(nameof(Value));
            }
            if (Index < 0)
            {
                throw new global::System.ArgumentOutOfRangeException(nameof(Index));
            }
            if (Count < 0)
            {
                throw new global::System.ArgumentOutOfRangeException(nameof(Count));
            }

            for (int I = 0; I < Count; I++)
            {
                Write(Value[Index + I]);
            }
        }
        public static void Write(string Value)
        {
            // Erase Cursor
            Canvas.DrawFilledRectangle(X, Y, 1, BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));

            for (int I = 0; I < Value.Length; I++)
            {
                WriteCore(Value[I]);
            }

            // Draw Cursor
            Canvas.DrawFilledRectangle(X, Y, 1, BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
            Canvas.Update();
        }
        public static void Write(char Value)
        {
            // Erase Cursor
            Canvas.DrawFilledRectangle(X, Y, 1, BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));

            WriteCore(Value);

            // Draw Cursor
            Canvas.DrawFilledRectangle(X, Y, 1, BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
            Canvas.Update();
        }
        public static void Write(bool Value)
        {
            Write(Value.ToString());
        }

        /* Decimal type is not working yet... */
        //public static void Write(decimal aDecimal) => Write(aDecimal.ToString());

        #endregion

        #region Misc

        private static void WriteCore(char C)
        {
            Buffer += C;

            switch (C)
            {
                case '\0':
                    break;
                case '\r':
                    break;
                case '\n':
                    X = SpacingX;
                    Y += (int)BitFont.Fallback.Size;
                    break;
                case '\b':
                    X -= (int)BitFont.Fallback.MeasureString(Buffer[^1].ToString());
                    break;
                case '\t':
                    X += (int)(BitFont.Fallback.Size * 4);
                    break;
                default:
                    if (char.IsAscii(C))
                    {
                        Canvas.DrawFilledRectangle(X, Y, BitFont.Fallback.MeasureString(C.ToString()), BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
                        Canvas.DrawChar(X, Y, C, BitFont.Fallback, Color.FromConsleColor(ForegroundColor), false);

                        if (X >= Canvas.Width - SpacingX)
                        {
                            X = SpacingX;
                            Y += (int)BitFont.Fallback.Size;
                        }
                        else
                        {
                            X += (int)BitFont.Fallback.MeasureString(C.ToString());
                        }
                        if (Y == Canvas.Height)
                        {
                            MemoryOperations.Copy(Canvas.Internal, Canvas.Internal + (Canvas.Width * BitFont.Fallback.Size), (int)(Canvas.Size - (Canvas.Width * BitFont.Fallback.Size)));
                        }
                    }
                    break;
            }
        }

        public static void ResetColor()
        {
            ForegroundColor = global::System.ConsoleColor.White;
            BackgroundColor = global::System.ConsoleColor.Black;
        }
        public static void Clear()
        {
            X = SpacingX;
            Y = SpacingY;
            Canvas.Clear();
            Canvas.Update();
            Buffer = string.Empty;
        }
        public static void Init()
        {
            /******PIT.RegisterTimer(new(() =>
            {
                if (IsCursorVisible && IsCursorEnabled)
                {
                    // Draw Cursor
                    Canvas.DrawFilledRectangle(X, Y, 1, BitFont.Fallback.Size, 0, Color.FromConsleColor(ForegroundColor));
                    Canvas.Update();
                    IsCursorVisible = false;
                    return;
                }
                else
                {
                    // Erase Cursor
                    Canvas.DrawFilledRectangle(X, Y, 1, BitFont.Fallback.Size, 0, Color.FromConsleColor(BackgroundColor));
                    Canvas.Update();
                    IsCursorVisible = true;
                    return;
                }
            }, 500000000, true));*****************************************very funny*/
        }

        #endregion

        #endregion

        #region Fields

        public static bool IsCursorVisible { get; set; } = true;
        public static bool IsCursorEnabled { get; set; } = true;
        public static string Buffer { get; set; } = string.Empty;
        public static VBECanvas Canvas { get; set; } = new();
        public static int SpacingX { get; set; } = 5;
        public static int SpacingY { get; set; } = 5;
        public static int X { get; set; } = 5;
        public static int Y { get; set; } = 5;

        public static global::System.ConsoleColor ForegroundColor { get; set; } = global::System.ConsoleColor.White;
        public static global::System.ConsoleColor BackgroundColor { get; set; } = global::System.ConsoleColor.Black;

        #endregion
    }
}

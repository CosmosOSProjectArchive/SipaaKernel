using Cosmos.System.Graphics;
using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismGL2D;
using SipaaKernel.System.Shard2;
using SipaaKernel.System.GraphicsExtensions;

namespace SipaaKernel.System
{
    public struct Cell
    {
        public char? Char;
        public Color Colour;
    }
    public class GUI
    {
        public void DrawFilledRectangle(Color pen, int x_start, int y_start, int width, int height)
        {
            if (height == -1)
            {
                height = width;
            }

            for (int y = y_start; y < y_start + height; y++)
            {
                Kernel.c.DrawLine(x_start, y, x_start + width + 1, y, pen);
            }
        }

        public void WriteByte(char ch, int mX, int mY, Color pen)
        {
            Kernel.c.DrawPSFChar(mX, mY, (char)ch, Assets.ConsoleFont, pen);
        }

        public void SetCursorPos(int mX, int mY)
        {
            if (Kernel.Console.CursorVisible)
            {
                DrawFilledRectangle(Kernel.Console.ForegroundPen, (int)0 + mX * Assets.ConsoleFont.Width,
                    (int)0 + mY * Assets.ConsoleFont.Height + Assets.ConsoleFont.Height, 8, 4);
            }
        }
    }
    public class Console
    {
        GUI Graphics;

        internal const char LineFeed = '\n';
        internal const char CarriageReturn = '\r';
        internal const char Tab = '\t';
        internal const char Space = ' ';

        private static uint[] Pallete = new uint[16];

        Cell[][] Text;

        List<string> Commands = new List<string>();
        private int CommandIndex = 0;
        public string Command = string.Empty;

        protected int mX = 0;
        public int X
        {
            get { return mX; }
            set
            {
                mX = value;
            }
        }


        protected int mY = 0;
        public int Y
        {
            get { return mY; }
            set
            {
                mY = value;
            }
        }

        public static int mWidth;
        public int Width
        {
            get { return mWidth; }
        }

        public static int mHeight;
        public int Height
        {
            get { return mHeight; }
        }

        public static int mCols;
        public int Cols
        {
            get { return mCols; }
        }

        public static int mRows;
        public int Rows
        {
            get { return mRows; }
        }

        public Color ForegroundPen = Color.White;
        public static uint foreground = (byte)ConsoleColor.White;
        public ConsoleColor Foreground
        {
            get { return (ConsoleColor)foreground; }
            set
            {
                foreground = (uint)value;
                ForegroundPen = new Color();
                ForegroundPen.ARGB = Pallete[foreground];
            }
        }

        public Color BackgroundPen = Color.Black;
        public static uint background = (byte)ConsoleColor.Black;
        public ConsoleColor Background
        {
            get { return (ConsoleColor)background; }
            set
            {
                background = (uint)value;
                BackgroundPen = new Color();
                BackgroundPen.ARGB = Pallete[background];
            }
        }

        public int CursorSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CursorVisible;

        public Console(uint width, uint height, uint x = 0, uint y = 0)
        {
            Graphics = new GUI();

            Pallete[0] = 0xFF000000; // Black
            Pallete[1] = 0xFF0000AB; // Darkblue
            Pallete[2] = 0xFF008000; // DarkGreen
            Pallete[3] = 0xFF008080; // DarkCyan
            Pallete[4] = 0xFF800000; // DarkRed
            Pallete[5] = 0xFF800080; // DarkMagenta
            Pallete[6] = 0xFF808000; // DarkYellow
            Pallete[7] = 0xFFC0C0C0; // Gray
            Pallete[8] = 0xFF808080; // DarkGray
            Pallete[9] = 0xFF5353FF; // Blue
            Pallete[10] = 0xFF55FF55; // Green
            Pallete[11] = 0xFF00FFFF; // Cyan
            Pallete[12] = 0xFFAA0000; // Red
            Pallete[13] = 0xFFFF00FF; // Magenta
            Pallete[14] = 0xFFFFFF55; // Yellow
            Pallete[15] = 0xFFFFFFFF; //White

            mWidth = (int)width;
            mHeight = (int)height;

            mCols = mWidth / Assets.ConsoleFont.Width - 1;
            mRows = mHeight / Assets.ConsoleFont.Height - 2;
            

            ClearText();

            CursorVisible = true;

            mX = 0;
            mY = 0;

            Command = string.Empty;
            Command = string.Empty;
        }

        public void Update()
        {
            KeyEvent keyEvent = null;

            if (KeyboardManager.TryReadKey(out keyEvent))
            {
                switch (keyEvent.Key)
                {
                    case ConsoleKeyEx.Enter:
                        if (ScrollMode)
                        {
                            break;
                        }
                        if (Command.Length > 0)
                        {
                            mX -= Command.Length;

                            WriteLine(Command);

                            //Kernel.CommandManager.Execute(Command);
                            //WriteLine(Command);
                            var result = Shard2.CommandRunner.FindAndRunCommand(this, Command);
                            if (result == CommandResult.NotFinded)
                            {
                                WriteLine("The command than you typed can't be found.");
                            }
                            else if (result == CommandResult.Error)
                            {
                                WriteLine("The command you runned ran into an error.");
                            }
                            else if (result == CommandResult.InvalidArgs)
                            {
                                WriteLine("The command has been found but the arguments is invalid.");
                            }
                            else if (result == CommandResult.Fatal)
                            {
                                WriteLine("The command you runned ran into an fatal error.");
                                WriteLine("The system needs to be rebooted.");
                                WriteLine("Press any key to reboot.");
                                global::System.Console.ReadKey();
                                Cosmos.System.Power.Reboot();
                            }else if (result == CommandResult.NeedsUpdateMethodExit)
                            {
                                return;
                            }

                            Commands.Add(Command);
                            CommandIndex = Commands.Count - 1;

                            Command = string.Empty;
                        }
                        else
                        {
                            WriteLine();
                            WriteLine();
                        }

                        BeforeCommand();
                        break;
                    case ConsoleKeyEx.Backspace:
                        if (ScrollMode)
                        {
                            break;
                        }
                        if (Command.Length > 0)
                        {
                            Command = Command.Remove(Command.Length - 1);
                            mX--;
                        }
                        break;
                    case ConsoleKeyEx.UpArrow:
                        if (KeyboardManager.ControlPressed)
                        {
                            ScrollUp();
                        }
                        else
                        {
                            if (CommandIndex >= 0)
                            {
                                mX -= Command.Length;
                                Command = Commands[CommandIndex];
                                CommandIndex--;
                                mX += Command.Length;
                            }
                        }
                        break;
                    case ConsoleKeyEx.DownArrow:
                        if (KeyboardManager.ControlPressed)
                        {
                            ScrollDown();
                        }
                        else
                        {
                            if (CommandIndex < Commands.Count - 1)
                            {
                                mX -= Command.Length;
                                CommandIndex++;
                                Command = Commands[CommandIndex];
                                mX += Command.Length;
                            }
                        }
                        break;
                    default:
                        if (ScrollMode)
                        {
                            break;
                        }
                        if (char.IsLetterOrDigit(keyEvent.KeyChar) || char.IsPunctuation(keyEvent.KeyChar) || char.IsSymbol(keyEvent.KeyChar) || (keyEvent.KeyChar == ' '))
                        {
                            Command += keyEvent.KeyChar;
                            mX++;
                        }
                        break;
                }
            }

            //Kernel.c.DrawFilledRectangle((int)0, (int)0, Kernel.c.Width, Kernel.c.Height, 0, Color.Black);
            Kernel.c.DrawImage(0, 0, Assets.Wallpaper, false);

            DrawTerminal();

            if (!ScrollMode)
            {
                DrawCursor();
            }
        }

        void DrawTerminal()
        {
            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mCols; j++)
                {
                    if (Text[i][j].Char == null || Text[i][j].Char == '\n')
                        continue;

                    Graphics.WriteByte((char)Text[i][j].Char, (int)0 + j * Assets.ConsoleFont.Width, (int)0+ i * Assets.ConsoleFont.Height, Text[i][j].Colour);
                }
            }

            if (Command.Length > 0)
            {
                int baseX = mX - Command.Length;

                for (int i = 0; i < Command.Length; i++)
                {
                    Graphics.WriteByte(Command[i], (int)0 + ((baseX + i) * Assets.ConsoleFont.Width), (int)0 + mY * Assets.ConsoleFont.Height, ForegroundPen);
                }
            }
        }

        private void ClearText()
        {
            Text = new Cell[mRows][];
            for (int i = 0; i < mRows; i++)
            {
                Text[i] = new Cell[mCols];
            }
        }

        public void Clear()
        {
            ClearText();
            mX = 0;
            mY = -1;
        }

        public void DrawCursor()
        {
            Graphics.SetCursorPos(mX, mY);
        }

        /// <summary>
        /// Scroll the console up and move crusor to the start of the line.
        /// </summary>
        private void DoLineFeed()
        {
            mY++;
            mX = 0;
            if (mY == mRows)
            {
                Scroll();
                mY--;
            }
        }

        List<Cell[]> TerminalHistory = new List<Cell[]>();
        int TerminalHistoryIndex = 0;
        bool ScrollMode = false;

        private void Scroll()
        {
            TerminalHistory.Add(Text[0]);
            TerminalHistoryIndex++;

            for (int i = 0; i < mRows - 1; i++)
            {
                Text[i] = Text[i + 1];
            }

            Text[mRows - 1] = new Cell[mCols];
        }

        private void ScrollUp()
        {
            if (TerminalHistoryIndex > 0)
            {
                ScrollMode = true;

                for (int i = Rows - 1; i > 0; i--)
                {
                    Text[i] = Text[i - 1];
                }

                TerminalHistoryIndex--;

                Text[0] = TerminalHistory[TerminalHistoryIndex];
            }
        }

        private void ScrollDown()
        {
            TerminalHistoryIndex = 0;

            TerminalHistory.Clear();

            ScrollMode = false;

            ClearText();
            mX = 0;
            mY = 0;
            BeforeCommand();
        }

        private void DoCarriageReturn()
        {
            mX = 0;
        }

        private void DoTab()
        {
            Write(Space);
            Write(Space);
            Write(Space);
            Write(Space);
        }

        /// <summary>
        /// Write char to the console.
        /// </summary>
        /// <param name="aChar">A char to write</param>
        public void Write(char aChar)
        {
            Text[mY][mX] = new Cell() { Char = aChar, Colour = ForegroundPen };
            mX++;
            if (mX == mCols)
            {
                DoLineFeed();
            }
        }

        public void Write(uint aInt) => Write(aInt.ToString());

        public void Write(ulong aLong) => Write(aLong.ToString());

        public void WriteLine() => Write(Environment.NewLine);

        public void WriteLine(string aText) => Write(aText + Environment.NewLine);

        public void Write(string aText)
        {
            for (int i = 0; i < aText.Length; i++)
            {
                switch (aText[i])
                {
                    case LineFeed:
                        DoLineFeed();
                        break;

                    case CarriageReturn:
                        DoCarriageReturn();
                        break;

                    case Tab:
                        DoTab();
                        break;

                    /* Normal characters, simply write them */
                    default:
                        Write(aText[i]);
                        break;
                }
            }
        }

        #region BeforeCommand

        /// <summary>
        /// Display the line before the user input and set the console color.
        /// </summary>
        public void BeforeCommand()
        {
            /**Foreground = ConsoleColor.Blue;
            Write(UserLevel.TypeUser);

            Foreground = ConsoleColor.Yellow;
            Write(Kernel.userLogged);

            Foreground = ConsoleColor.DarkGray;
            Write("@");

            Foreground = ConsoleColor.Blue;
            Write(Kernel.ComputerName);**/

            Foreground = ConsoleColor.Gray;
            Write("> ");

            //Foreground = ConsoleColor.DarkGray;
            //Write(Kernel.CurrentDirectory + "~ ");

            Foreground = ConsoleColor.White;
        }

        #endregion
    }
}

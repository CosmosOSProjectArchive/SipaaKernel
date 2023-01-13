using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public class CursorUtil
    {
        public static bool IsCursorOnRectangle(int x, int y, uint width, uint height)
        {
            if (MouseManager.X > x && MouseManager.X < x + width && MouseManager.Y > y && MouseManager.Y < y + height)
            {
                return true;
            }
            return false;
        }
    }
}

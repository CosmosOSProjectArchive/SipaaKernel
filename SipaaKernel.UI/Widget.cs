using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public class Widget
    {
        private int _X, _Y;
        private uint _Width, _Height;
        private Graphics _Graphics;

        public int X { get => _X; set => _X = value; }
        public int Y { get => _Y; set => _Y = value; }
        public uint Width { get => _Width; set => _Width = value; }
        public uint Height { get => _Height; set => _Height = value; }

    }
}

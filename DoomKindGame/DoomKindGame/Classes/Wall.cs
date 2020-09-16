using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomKindGame.Classes
{
    public partial class Wall
    {

        public double x1, y1, x2, y2, length;

        public double l1,l2, a1,a2; //полярные координаты

        public Wall(double X1, double Y1, double X2, double  Y2)
        {
            x1 = X1;
            x2 = X2;
            y1 = Y1;
            y2 = Y2;

            length = Math.Sqrt((x2 - x1)* (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

    }
}

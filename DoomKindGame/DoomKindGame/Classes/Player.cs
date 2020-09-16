using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomKindGame.Classes
{
    public class Player
    {
        public double X, Y, a;//pos x; pos y; angle a

        public double FieldOfView;

        public Player(double Xstart = 0, double Ystart = 0, double Astart = 0, double FOV =80)
        {
            X = 8;
            Y = 0;
            a = Math.PI;
            FieldOfView = FOV / 360 * 2 * Math.PI;

        }
    }


}

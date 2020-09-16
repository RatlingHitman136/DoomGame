using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DoomKindGame.Classes;

namespace DoomKindGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Wall> Level = new List<Wall>();
        Player MainPlayer;
        DispatcherTimer gameTimer = new DispatcherTimer();

        bool turningLeft, turningRight, movingForw,movingBack, movingRight,movingLeft;

        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += GameUpdate;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            MainPlayer = new Player();
            //Level.Add(new Wall(8, 1, 4, 2));
            //Level.Add(new Wall(0, -10, 0, 10));


            //Level.Add(new Wall(0, 2, 3, 3));
            //Level.Add(new Wall(3, 3, 4, 1));
            //Level.Add(new Wall(0, -2, 2, -2));
            //Level.Add(new Wall(2, -2, 4, -1));
            //Level.Add(new Wall(5, 3, 5, -2));


            Level.Add(new Wall(0, 4, 2, 3));
            Level.Add(new Wall(3, 2, 2, 3));
            Level.Add(new Wall(3, 2, 4, 0));
            Level.Add(new Wall(3, -2, 4, 0));
            Level.Add(new Wall(3, -2, 2, -3));
            Level.Add(new Wall(0, -4, 2, -3));
            Level.Add(new Wall(0, -4, -2, -3));
            Level.Add(new Wall(-3, -2, -2, -3));
            Level.Add(new Wall(-3, -2, -2, 3));



            //Level.Add(new Wall(3, -1, 3, 1));
            //Level.Add(new Wall(3, 1, 4, 2));
            //Level.Add(new Wall(7, -1, 7, 1));
            //Level.Add(new Wall(6, 2, 4, 2));
            //Level.Add(new Wall(6, 2, 7, 1));
            //Level.Add(new Wall(7, -1, 6, -2));
            //Level.Add(new Wall(6, -2, 4, -2));
            //Level.Add(new Wall(3, -1, 4, -2));

            //Level.Add(new Wall(2, -1, 2, 1));
            //Level.Add(new Wall(4, -2, 4, 2));
            //Level.Add(new Wall(6, -3, 6, 3));



            //Level.Add(new Wall(2, -1, 2, 1));
            //Level.Add(new Wall(3, -1, 3, 1));
            //Level.Add(new Wall(4, -1, 4, 1));

            MainCanvas.Focus();

        }

        void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                turningLeft = true;
            }
            if (e.Key == Key.E)
            {
                turningRight = true;
            }

            if (e.Key == Key.W)
            {
                movingForw = true;
            }
            if (e.Key == Key.S)
            {
                movingBack = true;
            }

            if (e.Key == Key.A)
            {
                movingLeft = true;
            }
            if (e.Key == Key.D)
            {
                movingRight = true;
            }
        }

        void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                turningLeft = false;
            }
            if (e.Key == Key.E)
            {
                turningRight = false;
            }

            if (e.Key == Key.W)
            {
                movingForw = false;
            }
            if (e.Key == Key.S)
            {
                movingBack = false;
            }
            if (e.Key == Key.A)
            {
                movingLeft = false;
            }
            if (e.Key == Key.D)
            {
                movingRight = false;
            }
        }

        private void GameUpdate(object sender, EventArgs e)
        {
            
            if (turningLeft)
            {
                MainPlayer.a += .05;
                FrameRender();
            }
            if (turningRight)
            {
                MainPlayer.a -= .05;
                FrameRender();
            }

            if (movingForw)
            {
                MainPlayer.X += .25 * Math.Cos(MainPlayer.a);
                MainPlayer.Y += .25 * Math.Sin(MainPlayer.a);
                FrameRender();
            }
            if (movingBack)
            {

                MainPlayer.X -= .25 * Math.Cos(MainPlayer.a);
                MainPlayer.Y -= .25 * Math.Sin(MainPlayer.a);
                FrameRender();
            }

            if (movingLeft)
            {

                MainPlayer.X -= .25 * Math.Sin(MainPlayer.a);
                MainPlayer.Y += .25 * Math.Cos(MainPlayer.a);
                FrameRender();
            }
            if (movingRight)
            {

                MainPlayer.X += .25 * Math.Sin(MainPlayer.a);
                MainPlayer.Y -= .25 * Math.Cos(MainPlayer.a);
                FrameRender();
            }
            if (MainPlayer.a >= Math.PI * 2) MainPlayer.a -= Math.PI * 2;
            if (MainPlayer.a < 0) MainPlayer.a += Math.PI * 2;

        }
        private void FrameRender()
        {
            List<Wall> ToRender = new List<Wall>();
            //нашли стены которые попали в зону обзора
            for(int i =0;i<Level.Count();i++)
            {
                Wall rellWall = new Wall(Level[i].x1 - MainPlayer.X, Level[i].y1 - MainPlayer.Y, Level[i].x2 - MainPlayer.X, Level[i].y2 - MainPlayer.Y);
                // полярные координаты  стены
                double l1, a1, l2, a2;
                l1 = Math.Sqrt(rellWall.x1 * rellWall.x1 + rellWall.y1 * rellWall.y1); a1 = 0;
                l2 = Math.Sqrt(rellWall.x2 * rellWall.x2 + rellWall.y2 * rellWall.y2); a2 = 0;

                if (rellWall.y1 >= 0) a1 = Math.Acos(rellWall.x1 / l1);
                if (rellWall.y1 < 0) a1 = Math.PI * 2 - Math.Acos(rellWall.x1 / l1);

                if (rellWall.y2 >= 0) a2 = Math.Acos(rellWall.x2 / l2);
                if (rellWall.y2 < 0) a2 = Math.PI * 2 - Math.Acos(rellWall.x2 / l2);

                rellWall.a1 = a1; rellWall.l1 = l1;
                rellWall.a2 = a2; rellWall.l2 = l2;


                if (MainPlayer.a >= MainPlayer.FieldOfView / 2 && MainPlayer.a <= Math.PI * 2 - MainPlayer.FieldOfView / 2)
                {
                    if ((rellWall.a1 > MainPlayer.a - MainPlayer.FieldOfView / 2 && rellWall.a1 < MainPlayer.a + MainPlayer.FieldOfView / 2) && (rellWall.a2 > MainPlayer.a - MainPlayer.FieldOfView / 2 && rellWall.a2 < MainPlayer.a + MainPlayer.FieldOfView / 2))
                    {
                        ToRender.Add(rellWall);
                        continue;
                    }
                }
                else
                {
                    if ((rellWall.a1 > ((MainPlayer.a - MainPlayer.FieldOfView / 2) < 0 ? (MainPlayer.a - MainPlayer.FieldOfView / 2) + Math.PI * 2 : (MainPlayer.a - MainPlayer.FieldOfView / 2)) || rellWall.a1 < ((MainPlayer.a + MainPlayer.FieldOfView / 2) >= Math.PI * 2 ? (MainPlayer.a + MainPlayer.FieldOfView / 2) - Math.PI * 2 : (MainPlayer.a + MainPlayer.FieldOfView / 2))) && (rellWall.a2 > ((MainPlayer.a - MainPlayer.FieldOfView / 2) < 0 ? (MainPlayer.a - MainPlayer.FieldOfView / 2) + Math.PI * 2 : (MainPlayer.a - MainPlayer.FieldOfView / 2)) || rellWall.a2 < ((MainPlayer.a + MainPlayer.FieldOfView / 2) >= Math.PI * 2 ? (MainPlayer.a + MainPlayer.FieldOfView / 2) - Math.PI * 2 : (MainPlayer.a + MainPlayer.FieldOfView / 2))))
                    {
                        ToRender.Add(rellWall);
                        continue;
                    }
                }
                double MainPlayerA1 = (MainPlayer.a - MainPlayer.FieldOfView / 2 > 0) ? MainPlayer.a - MainPlayer.FieldOfView / 2 : MainPlayer.a - MainPlayer.FieldOfView / 2 + 2 * Math.PI;
                double MainPlayerA2 = (MainPlayer.a + MainPlayer.FieldOfView / 2 < Math.PI*2) ? MainPlayer.a + MainPlayer.FieldOfView / 2 : MainPlayer.a + MainPlayer.FieldOfView / 2 - 2 * Math.PI;
            
                Wall rellWallChanged = new Wall(rellWall.x1, rellWall.y1, rellWall.x2, rellWall.y2);

                if (Math.Abs(rellWall.a1 - rellWall.a2) <= Math.PI && (MainPlayerA1 >= Math.Min(rellWall.a1, rellWall.a2) && MainPlayerA1 <= Math.Max(rellWall.a1, rellWall.a2)) && (MainPlayerA2 >= Math.Min(rellWall.a1, rellWall.a2) && MainPlayerA2 <= Math.Max(rellWall.a1, rellWall.a2)))
                {
                    double alpha = Math.Abs(MainPlayerA2 - rellWall.a1);
                    double beta = Math.Acos((rellWall.length * rellWall.length + rellWall.l1 * rellWall.l1 - rellWall.l2 * rellWall.l2) / (2 * rellWall.length * rellWall.l1));
                    double lLeft = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);
                    alpha = Math.Abs(MainPlayerA1 - rellWall.a1);
                    double lRight = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                    rellWallChanged.a1 = MainPlayerA2;
                    rellWallChanged.l1 = lLeft;
                    rellWallChanged.a2 = MainPlayerA1;
                    rellWallChanged.l2 = lRight;
                    ToRender.Add(rellWallChanged);
                    continue;

                }
                if (Math.Abs(rellWall.a1 - rellWall.a2) > Math.PI && (MainPlayerA1 < Math.Min(rellWall.a1, rellWall.a2) || MainPlayerA1 > Math.Max(rellWall.a1, rellWall.a2)) && (MainPlayerA2 < Math.Min(rellWall.a1, rellWall.a2) || MainPlayerA2 > Math.Max(rellWall.a1, rellWall.a2)))
                {

                    double alpha;
                    double beta = Math.Acos((rellWall.length * rellWall.length + rellWall.l1 * rellWall.l1 - rellWall.l2 * rellWall.l2) / (2 * rellWall.length * rellWall.l1));
                    if (rellWall.a1 > rellWall.a2)
                    {
                        alpha = (MainPlayerA2 - rellWall.a1 >= 0) ? MainPlayerA2 - rellWall.a1 : Math.PI * 2 + MainPlayerA2 - rellWall.a1;
                        double lLeft = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);
                        alpha = (MainPlayerA1 - rellWall.a1 >= 0) ? MainPlayerA1 - rellWall.a1 : Math.PI * 2 + MainPlayerA1 - rellWall.a1;
                        double lRight = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);
                        rellWallChanged.a1 = MainPlayerA2;
                        rellWallChanged.l1 = lLeft;
                        rellWallChanged.a2 = MainPlayerA1;
                        rellWallChanged.l2 = lRight;
                        ToRender.Add(rellWallChanged);
                        continue;
                    }
                    else
                    {
                        alpha = (rellWall.a1 - MainPlayerA2 >= 0) ? rellWall.a1 - MainPlayerA2 : Math.PI * 2 + rellWall.a1 - MainPlayerA2;
                        double lLeft = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);
                        alpha = (rellWall.a1 - MainPlayerA1 >= 0) ? rellWall.a1 - MainPlayerA1 : Math.PI * 2 + rellWall.a1 - MainPlayerA1;
                        double lRight = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);
                        rellWallChanged.a1 = MainPlayerA2;
                        rellWallChanged.l1 = lLeft;
                        rellWallChanged.a2 = MainPlayerA1;
                        rellWallChanged.l2 = lRight;

                        ToRender.Add(rellWallChanged);
                        continue;
                    }
                    
                }

                if (Math.Abs(rellWall.a1 - rellWall.a2) <= Math.PI && (MainPlayerA1 < Math.Max(rellWall.a1, rellWall.a2) && MainPlayerA1 > Math.Min(rellWall.a1, rellWall.a2)) && !(MainPlayerA2 < Math.Max(rellWall.a1, rellWall.a2) && MainPlayerA2 > Math.Min(rellWall.a1, rellWall.a2)))
                {
                    double alpha = Math.Abs(MainPlayerA1 - rellWall.a1);
                    double beta = Math.Acos((rellWall.length * rellWall.length + rellWall.l1 * rellWall.l1 - rellWall.l2 * rellWall.l2) / (2 * rellWall.length * rellWall.l1));
                    double L = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                    rellWallChanged.a1 = MainPlayerA1;
                    rellWallChanged.l1 = L;
                    rellWallChanged.a2 = Math.Max(rellWall.a1, rellWall.a2);
                    rellWallChanged.l2 = rellWall.a1 > rellWall.a2 ? rellWall.l1 : rellWall.l2;
                    ToRender.Add(rellWallChanged);
                    continue;
                }
                if (Math.Abs(rellWall.a1 - rellWall.a2) <= Math.PI && !(MainPlayerA1 < Math.Max(rellWall.a1, rellWall.a2) && MainPlayerA1 > Math.Min(rellWall.a1, rellWall.a2)) && (MainPlayerA2 < Math.Max(rellWall.a1, rellWall.a2) && MainPlayerA2 > Math.Min(rellWall.a1, rellWall.a2)))
                {
                    double alpha = Math.Abs(MainPlayerA2 - rellWall.a1);
                    double beta = Math.Acos((rellWall.length * rellWall.length + rellWall.l1 * rellWall.l1 - rellWall.l2 * rellWall.l2) / (2 * rellWall.length * rellWall.l1));
                    double L = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                    rellWallChanged.a1 = MainPlayerA2;
                    rellWallChanged.l1 = L;
                    rellWallChanged.a2 = Math.Min(rellWall.a1, rellWall.a2);
                    rellWallChanged.l2 = rellWall.a1 > rellWall.a2 ? rellWall.l2 : rellWall.l1;
                    ToRender.Add(rellWallChanged);
                    continue;
                }
                
                if (Math.Abs(rellWall.a1 - rellWall.a2) > Math.PI && (MainPlayerA1 > Math.Max(rellWall.a1, rellWall.a2) || MainPlayerA1 < Math.Min(rellWall.a1, rellWall.a2)) && !(MainPlayerA2 > Math.Max(rellWall.a1, rellWall.a2) || MainPlayerA2 < Math.Min(rellWall.a1, rellWall.a2)))
                {
                    double alpha = 0;
                    if (rellWall.a1 > rellWall.a2) alpha = MainPlayerA1 - rellWall.a1 > 0 ? MainPlayerA1 - rellWall.a1 : Math.PI * 2 + MainPlayerA1 - rellWall.a1;
                    if (rellWall.a1 < rellWall.a2) alpha = rellWall.a1 - MainPlayerA1 > 0 ? rellWall.a1 - MainPlayerA1 : Math.PI * 2 + rellWall.a1 - MainPlayerA1;
                    double beta = Math.Acos((rellWall.length * rellWall.length + rellWall.l1 * rellWall.l1 - rellWall.l2 * rellWall.l2) / (2 * rellWall.length * rellWall.l1));
                    double L = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                    rellWallChanged.a1 = MainPlayerA1;
                    rellWallChanged.l1 = L;
                    rellWallChanged.a2 = Math.Min(rellWall.a1, rellWall.a2);
                    rellWallChanged.l2 = rellWall.a1 > rellWall.a2 ? rellWall.l2 : rellWall.l1;
                    ToRender.Add(rellWallChanged);
                    continue;
                }
                if (Math.Abs(rellWall.a1 - rellWall.a2) > Math.PI && !(MainPlayerA1 > Math.Max(rellWall.a1, rellWall.a2) || MainPlayerA1< Math.Min(rellWall.a1, rellWall.a2)) && (MainPlayerA2 > Math.Max(rellWall.a1, rellWall.a2) || MainPlayerA2 < Math.Min(rellWall.a1, rellWall.a2)))
                {
                    double alpha = 0;
                    if (rellWall.a1 > rellWall.a2) alpha = MainPlayerA2 - rellWall.a1 > 0 ? MainPlayerA2 - rellWall.a1 : Math.PI * 2 + MainPlayerA2 - rellWall.a1;
                    if (rellWall.a1 < rellWall.a2) alpha = rellWall.a1 - MainPlayerA2 > 0 ? rellWall.a1 - MainPlayerA2 : Math.PI * 2 + rellWall.a1 - MainPlayerA2;
                    double beta = Math.Acos((rellWall.length * rellWall.length + rellWall.l1 * rellWall.l1 - rellWall.l2 * rellWall.l2) / (2 * rellWall.length * rellWall.l1));
                    double L = (rellWall.l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                    rellWallChanged.a1 = MainPlayerA2;
                    rellWallChanged.l1 = L;
                    rellWallChanged.a2 = Math.Max(rellWall.a1, rellWall.a2);
                    rellWallChanged.l2 = rellWall.a1 > rellWall.a2 ? rellWall.l1 : rellWall.l2;
                    ToRender.Add(rellWallChanged);
                    continue;
                }

                


            }
            //-----------------------------------------------------------------------------------------------------------------------------------
            for (int i = 0; i < ToRender.Count; i++)
            {
                if (ToRender[i].a1 == ToRender[i].a2) { ToRender.RemoveAt(i); i--; continue; }
                for (int j = 0; j < ToRender.Count; j++)
                {
                    if (j == i) continue;
                    if (ToRender[j].a1 == ToRender[j].a2)
                    {
                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }

                    bool isFullCovered = false;
                    if (i < 0) throw (new Exception());
                    if (Math.Abs(ToRender[i].a1 - ToRender[i].a2) <= Math.PI && (ToRender[j].a1 >= Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a1 <= Math.Max(ToRender[i].a1, ToRender[i].a2)) && (ToRender[j].a2 >= Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a2 <= Math.Max(ToRender[i].a1, ToRender[i].a2)) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    { isFullCovered = true; }
                    if (Math.Abs(ToRender[i].a1 - ToRender[i].a2) > Math.PI && (ToRender[j].a1 >= Math.Max(ToRender[i].a1, ToRender[i].a2) || ToRender[j].a1 <= Math.Min(ToRender[i].a1, ToRender[i].a2)) && (ToRender[j].a2 >= Math.Max(ToRender[i].a1, ToRender[i].a2) || ToRender[j].a2 <= Math.Min(ToRender[i].a1, ToRender[i].a2)) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    { isFullCovered = true; }
                    if (isFullCovered)
                    {
                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }

                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) <= Math.PI && (ToRender[i].a1 >= Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a1 <= Math.Max(ToRender[j].a1, ToRender[j].a2)) && (ToRender[i].a2 >= Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a2 <= Math.Max(ToRender[j].a1, ToRender[j].a2)) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    {
                        double alpha = Math.Abs(Math.Max(ToRender[i].a1, ToRender[i].a2) - ToRender[j].a1);
                        double beta = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l1 * ToRender[j].l1 - ToRender[j].l2 * ToRender[j].l2) / (2 * ToRender[j].length * ToRender[j].l1));

                        double lLeft = (ToRender[j].l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);
                        alpha = Math.Abs(Math.Min(ToRender[i].a1, ToRender[i].a2) - ToRender[j].a1);
                        double lRight = (ToRender[j].l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));
                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));//ToRender[j]
                        if (ToRender[j].a1 > ToRender[j].a2)
                        {
                            ToRender[ToRender.Count - 1].a1 = ToRender[j].a2;
                            ToRender[ToRender.Count - 1].l1 = ToRender[j].l2;
                            ToRender[ToRender.Count - 1].a2 = Math.Min(ToRender[i].a1, ToRender[i].a2);
                            ToRender[ToRender.Count - 1].l2 = lRight;

                            ToRender[ToRender.Count - 2].a1 = Math.Max(ToRender[i].a1, ToRender[i].a2);
                            ToRender[ToRender.Count - 2].l1 = lLeft;
                            ToRender[ToRender.Count - 2].a2 = ToRender[j].a1;
                            ToRender[ToRender.Count - 2].l2 = ToRender[j].l1;
                        }
                        else if (ToRender[j].a1 < ToRender[j].a2)
                        {
                            ToRender[ToRender.Count - 1].a1 = ToRender[j].a1;
                            ToRender[ToRender.Count - 1].l1 = ToRender[j].l1;
                            ToRender[ToRender.Count - 1].a2 = Math.Min(ToRender[i].a1, ToRender[i].a2);
                            ToRender[ToRender.Count - 1].l2 = lRight;

                            ToRender[ToRender.Count - 2].a1 = Math.Max(ToRender[i].a1, ToRender[i].a2);
                            ToRender[ToRender.Count - 2].l1 = lLeft;
                            ToRender[ToRender.Count - 2].a2 = ToRender[j].a2;
                            ToRender[ToRender.Count - 2].l2 = ToRender[j].l2;
                        }

                        ToRender.RemoveAt(j);
                        i = -1;
                        break;

                    }
                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) > Math.PI && (ToRender[i].a1 < Math.Min(ToRender[j].a1, ToRender[j].a2) || ToRender[i].a1 > Math.Max(ToRender[j].a1, ToRender[j].a2)) && (ToRender[i].a2 < Math.Min(ToRender[j].a1, ToRender[j].a2) || (ToRender[i].a2 > Math.Max(ToRender[j].a1, ToRender[j].a2))) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    {
                        double RellIa1, RellIa2; // относительно правого луча стороны j
                        double RellIa1ToOne, RellIa2ToOne; // относительно луча в точку с номером 1 стороны j
                        double beta = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l1 * ToRender[j].l1 - ToRender[j].l2 * ToRender[j].l2) / (2 * ToRender[j].length * ToRender[j].l1));

                        if (ToRender[i].a1 >= Math.Max(ToRender[j].a1, ToRender[j].a2)) RellIa1 = ToRender[i].a1 - Math.Max(ToRender[j].a1, ToRender[j].a2);
                        else RellIa1 = ToRender[i].a1 + (Math.PI * 2 - Math.Max(ToRender[j].a1, ToRender[j].a2));
                        RellIa1ToOne = RellIa1;

                        if (ToRender[i].a2 >= Math.Max(ToRender[j].a1, ToRender[j].a2)) RellIa2 = ToRender[i].a2 - Math.Max(ToRender[j].a1, ToRender[j].a2);
                        else RellIa2 = ToRender[i].a2 + (Math.PI * 2 - Math.Max(ToRender[j].a1, ToRender[j].a2));
                        RellIa2ToOne = RellIa2;

                        if (ToRender[j].a1 < ToRender[j].a2)
                        {
                            RellIa1ToOne = Math.PI * 2 - Math.Abs(ToRender[j].a1 - ToRender[j].a2) - RellIa1ToOne;
                            RellIa2ToOne = Math.PI * 2 - Math.Abs(ToRender[j].a1 - ToRender[j].a2) - RellIa2ToOne;
                        }

                        double lLeft = (ToRender[j].l1 / Math.Sin(Math.PI - Math.Max(RellIa1ToOne, RellIa2ToOne) - beta)) * Math.Sin(beta);
                        double lRight = (ToRender[j].l1 / Math.Sin(Math.PI - Math.Min(RellIa1ToOne, RellIa2ToOne) - beta)) * Math.Sin(beta);

                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));
                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));

                        ToRender[ToRender.Count - 1].a1 = (ToRender[j].a1 > ToRender[j].a2) ? ToRender[j].a1 : ToRender[j].a2;
                        ToRender[ToRender.Count - 1].l1 = (ToRender[j].a1 > ToRender[j].a2) ? ToRender[j].l1 : ToRender[j].l2;
                        ToRender[ToRender.Count - 1].a2 = (Math.Min(RellIa1, RellIa2) + Math.Max(ToRender[j].a1, ToRender[j].a2)) > Math.PI * 2 ? (Math.Min(RellIa1, RellIa2) + Math.Max(ToRender[j].a1, ToRender[j].a2)) - Math.PI * 2 : (Math.Min(RellIa1, RellIa2) + Math.Max(ToRender[j].a1, ToRender[j].a2));
                        ToRender[ToRender.Count - 1].l2 = lRight;

                        ToRender[ToRender.Count - 2].a1 = (Math.Max(RellIa1, RellIa2) + Math.Max(ToRender[j].a1, ToRender[j].a2)) > Math.PI * 2 ? (Math.Max(RellIa1, RellIa2) + Math.Max(ToRender[j].a1, ToRender[j].a2)) - Math.PI * 2 : (Math.Max(RellIa1, RellIa2) + Math.Max(ToRender[j].a1, ToRender[j].a2));
                        ToRender[ToRender.Count - 2].l1 = lLeft;
                        ToRender[ToRender.Count - 2].a2 = (ToRender[j].a1 > ToRender[j].a2) ? ToRender[j].a2 : ToRender[j].a1;
                        ToRender[ToRender.Count - 2].l2 = (ToRender[j].a1 > ToRender[j].a2) ? ToRender[j].l2 : ToRender[j].l1;

                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }

                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) <= Math.PI && (ToRender[i].a1 > Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a1 < Math.Max(ToRender[j].a1, ToRender[j].a2)) && !(ToRender[i].a2 > Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a2 < Math.Max(ToRender[j].a1, ToRender[j].a2)) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    {
                        double alpha = Math.Abs(ToRender[i].a1 - ToRender[j].a1);
                        double beta = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l1 * ToRender[j].l1 - ToRender[j].l2 * ToRender[j].l2) / (2 * ToRender[j].length * ToRender[j].l1));
                        double L = (ToRender[j].l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));
                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));

                        ToRender[ToRender.Count - 1].a1 = ToRender[j].a1;
                        ToRender[ToRender.Count - 1].l1 = ToRender[j].l1;
                        ToRender[ToRender.Count - 1].a2 = ToRender[i].a1;
                        ToRender[ToRender.Count - 1].l2 = L;

                        ToRender[ToRender.Count - 2].a1 = ToRender[i].a1;
                        ToRender[ToRender.Count - 2].l1 = L;
                        ToRender[ToRender.Count - 2].a2 = ToRender[j].a2;
                        ToRender[ToRender.Count - 2].l2 = ToRender[j].l2;

                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }
                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) <= Math.PI && !(ToRender[i].a1 > Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a1 < Math.Max(ToRender[j].a1, ToRender[j].a2)) && (ToRender[i].a2 > Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a2 < Math.Max(ToRender[j].a1, ToRender[j].a2)) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    {
                        double alpha = Math.Abs(ToRender[i].a2 - ToRender[j].a1);
                        double beta = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l1 * ToRender[j].l1 - ToRender[j].l2 * ToRender[j].l2) / (2 * ToRender[j].length * ToRender[j].l1));
                        double L = (ToRender[j].l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));
                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));

                        ToRender[ToRender.Count - 1].a1 = ToRender[j].a1;
                        ToRender[ToRender.Count - 1].l1 = ToRender[j].l1;
                        ToRender[ToRender.Count - 1].a2 = ToRender[i].a2;
                        ToRender[ToRender.Count - 1].l2 = L;

                        ToRender[ToRender.Count - 2].a1 = ToRender[i].a2;
                        ToRender[ToRender.Count - 2].l1 = L;
                        ToRender[ToRender.Count - 2].a2 = ToRender[j].a2;
                        ToRender[ToRender.Count - 2].l2 = ToRender[j].l2;

                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }

                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) > Math.PI && (ToRender[i].a1 < Math.Min(ToRender[j].a1, ToRender[j].a2) || ToRender[i].a1 > Math.Max(ToRender[j].a1, ToRender[j].a2)) && !(ToRender[i].a2 < Math.Min(ToRender[j].a1, ToRender[j].a2) || (ToRender[i].a2 > Math.Max(ToRender[j].a1, ToRender[j].a2))) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    {
                        double alpha = (ToRender[i].a1 >= Math.Max(ToRender[j].a1, ToRender[j].a2)) ? ToRender[i].a1 - Math.Max(ToRender[j].a1, ToRender[j].a2) : ToRender[i].a1 + (Math.PI * 2 - Math.Max(ToRender[j].a1, ToRender[j].a2));
                        double beta = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l1 * ToRender[j].l1 - ToRender[j].l2 * ToRender[j].l2) / (2 * ToRender[j].length * ToRender[j].l1));
                        double L = (ToRender[j].l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));
                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));

                        ToRender[ToRender.Count - 1].a1 = ToRender[j].a2;
                        ToRender[ToRender.Count - 1].l1 = ToRender[j].l2;
                        ToRender[ToRender.Count - 1].a2 = ToRender[i].a1;
                        ToRender[ToRender.Count - 1].l2 = L;

                        ToRender[ToRender.Count - 2].a1 = ToRender[i].a1;
                        ToRender[ToRender.Count - 2].l1 = L;
                        ToRender[ToRender.Count - 2].a2 = ToRender[j].a1;
                        ToRender[ToRender.Count - 2].l2 = ToRender[j].l1;

                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }
                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) > Math.PI && !(ToRender[i].a1 < Math.Min(ToRender[j].a1, ToRender[j].a2) || ToRender[i].a1 > Math.Max(ToRender[j].a1, ToRender[j].a2)) && (ToRender[i].a2 < Math.Min(ToRender[j].a1, ToRender[j].a2) || (ToRender[i].a2 > Math.Max(ToRender[j].a1, ToRender[j].a2))) && ToRender[i].l1 + ToRender[i].l2 < ToRender[j].l1 + ToRender[j].l2)
                    {
                        double alpha = (ToRender[i].a2 >= Math.Max(ToRender[j].a1, ToRender[j].a2)) ? ToRender[i].a2 - Math.Max(ToRender[j].a1, ToRender[j].a2) : ToRender[i].a2 + (Math.PI * 2 - Math.Max(ToRender[j].a1, ToRender[j].a2));
                        double beta = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l1 * ToRender[j].l1 - ToRender[j].l2 * ToRender[j].l2) / (2 * ToRender[j].length * ToRender[j].l1));
                        double L = (ToRender[j].l1 / Math.Sin(Math.PI - alpha - beta)) * Math.Sin(beta);

                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));
                        ToRender.Add(new Wall(ToRender[j].x1, ToRender[j].y1, ToRender[j].x2, ToRender[j].y2));

                        ToRender[ToRender.Count - 1].a1 = ToRender[j].a1;
                        ToRender[ToRender.Count - 1].l1 = ToRender[j].l1;
                        ToRender[ToRender.Count - 1].a2 = ToRender[i].a2;
                        ToRender[ToRender.Count - 1].l2 = L;

                        ToRender[ToRender.Count - 2].a1 = ToRender[i].a2;
                        ToRender[ToRender.Count - 2].l1 = L;
                        ToRender[ToRender.Count - 2].a2 = ToRender[j].a2;
                        ToRender[ToRender.Count - 2].l2 = ToRender[j].l2;

                        ToRender.RemoveAt(j);
                        i = -1;
                        break;
                    }




                }
            }  //отрезаем все куски стен которые перекрыты
               //-----------------------------------------------------------------------------------------------------------------------------------
               //for(int i =0;i< ToRender.Count;i++)// выбираем стенку которая будет пытаться перекрыть другие
               //{
               //    if (ToRender[i].a1 == ToRender[i].a2) { ToRender.RemoveAt(i); i--; continue; }
               //    for (int j = 0; j < ToRender.Count; j++)
               //    {
               //        if (j == i) continue;
               //        if (ToRender[j].a1 == ToRender[j].a2) { ToRender.RemoveAt(j); j--; if (j < i) i--; continue; }

            //        int CountOfInterseptrion = 0;
            //        double x1, y1, l1, a1; //координаты точки пересечения 1 (ХОY / полярные)
            //        double x2, y2, l2, a2; //координаты точки пересечения 2 (ХОY / полярные)

            //        l1 = 0; x1 = 0; y1 = 0; a1 = 0;
            //        l2 = 0; x2 = 0; y2 = 0; a2 = 0;
            //        if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) <= Math.PI)
            //        {
            //            if (ToRender[i].a1 >= Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a1 <= Math.Max(ToRender[j].a1, ToRender[j].a2))
            //            {
            //                double gamma = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l2 * ToRender[j].l2 - ToRender[j].l1 * ToRender[j].l1) / (2 * ToRender[j].l2 * ToRender[j].length));
            //                double alpha = Math.Min(Math.Abs(ToRender[i].a1 - ToRender[j].a2), Math.PI * 2 - Math.Abs(ToRender[i].a1 - ToRender[j].a2));
            //                double betta = Math.PI - alpha - gamma;

            //                l1 = (ToRender[j].l2 * Math.Sin(gamma)) / Math.Sin(betta);
            //                a1 = ToRender[i].a1;
            //                if (l1 < ToRender[i].l1) continue;
            //                else
            //                {
            //                    x1 = l1 * Math.Cos(a1);
            //                    y1 = l1 * Math.Sin(a1);
            //                    CountOfInterseptrion++;
            //                }
            //            }

            //            if (ToRender[i].a2 >= Math.Min(ToRender[j].a1, ToRender[j].a2) && ToRender[i].a2 <= Math.Max(ToRender[j].a1, ToRender[j].a2))
            //            {
            //                double gamma = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l2 * ToRender[j].l2 - ToRender[j].l1 * ToRender[j].l1) / (2 * ToRender[j].l2 * ToRender[j].length));
            //                double alpha = Math.Min(Math.Abs(ToRender[i].a2 - ToRender[j].a2), Math.PI * 2 - Math.Abs(ToRender[i].a2 - ToRender[j].a2));
            //                double betta = Math.PI - alpha - gamma;

            //                l2 = (ToRender[j].l2 * Math.Sin(gamma)) / Math.Sin(betta);
            //                a2 = ToRender[i].a2;
            //                if (l2 < ToRender[i].l2) continue;
            //                else
            //                {
            //                    x2 = l2 * Math.Cos(a2);
            //                    y2 = l2 * Math.Sin(a2);
            //                    CountOfInterseptrion++;
            //                }
            //            }
            //            Wall newWallToRender = new Wall(0, 0, 0, 0);
            //            switch (CountOfInterseptrion)
            //            {
            //                case 0:
            //                    /*if(ToRender[j].a1 >= Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a1 <= Math.Max(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a2 >= Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a2 <= Math.Max(ToRender[i].a1, ToRender[i].a2))
            //                    {
            //                        ToRender.RemoveAt(j);
            //                        j--;
            //                        if (j < i) i--;
            //                    }*/
            //                    break;
            //                case 1:
            //                    if (a1 + a2 == ToRender[j].a1)
            //                    {
            //                        /*if (ToRender[j].a2 > Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a2 < Math.Max(ToRender[i].a1, ToRender[i].a2))
            //                        {
            //                            ToRender.RemoveAt(j);
            //                            j--;
            //                            if (j < i) i--;
            //                            break;
            //                        }*/
            //                        break;
            //                    }
            //                    if (a1 + a2 == ToRender[j].a2)
            //                    {
            //                        /*if (ToRender[j].a1 > Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a1 < Math.Max(ToRender[i].a1, ToRender[i].a2))
            //                        {
            //                            ToRender.RemoveAt(j);
            //                            j--;
            //                            if (j < i) i--;
            //                            break;
            //                        }*/
            //                        break;
            //                    }
            //                    if (ToRender[j].a1 >= Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a1 <= Math.Max(ToRender[i].a1, ToRender[i].a2))
            //                    {
            //                        newWallToRender = new Wall(x1 + x2 , y1 + y2 , ToRender[j].x2, ToRender[j].y2);
            //                        newWallToRender.a1 = a1 + a2;
            //                        newWallToRender.a2 = ToRender[j].a2;
            //                        newWallToRender.l1 = l1 + l2;
            //                        newWallToRender.l2 = ToRender[j].l2;
            //                    }
            //                    if (ToRender[j].a2 >= Math.Min(ToRender[i].a1, ToRender[i].a2) && ToRender[j].a2 <= Math.Max(ToRender[i].a1, ToRender[i].a2))
            //                    {
            //                        newWallToRender = new Wall(x1  + x2, y1 + y2, ToRender[j].x1, ToRender[j].y1);
            //                        newWallToRender.a1 = a1 + a2;
            //                        newWallToRender.a2 = ToRender[j].a1;
            //                        newWallToRender.l1 = l1 + l2;
            //                        newWallToRender.l2 = ToRender[j].l1;
            //                    }
            //                    ToRender.RemoveAt(j);
            //                    ToRender.Insert(j, newWallToRender);
            //                    j--;
            //                    if (j < i) i--;

            //                    break;
            //                case 2:
            //                    if ((ToRender[j].a1 == a1 && ToRender[j].a2 == a2) || (ToRender[j].a1 == a2 && ToRender[j].a2 == a1))
            //                        break;
            //                    if (ToRender[j].a1 == a1)
            //                    {
            //                        newWallToRender = new Wall(x2, y2, ToRender[j].x2, ToRender[j].y2);
            //                        newWallToRender.a1 = a2;
            //                        newWallToRender.a2 = ToRender[j].a2;
            //                        newWallToRender.l1 = l2;
            //                        newWallToRender.l2 = ToRender[j].l2;
            //                    }
            //                    if (ToRender[j].a2 == a2)
            //                    {
            //                        newWallToRender = new Wall(x1, y1, ToRender[j].x1, ToRender[j].y1);
            //                        newWallToRender.a1 = a1;
            //                        newWallToRender.a2 = ToRender[j].a1;
            //                        newWallToRender.l1 = l1;
            //                        newWallToRender.l2 = ToRender[j].l1;
            //                    }
            //                    if (ToRender[j].a2 == a1)
            //                    {
            //                        newWallToRender = new Wall(x2, y2, ToRender[j].x1, ToRender[j].y1);
            //                        newWallToRender.a1 = a2;
            //                        newWallToRender.a2 = ToRender[j].a1;
            //                        newWallToRender.l1 = l2;
            //                        newWallToRender.l2 = ToRender[j].l1;
            //                    }
            //                    if (ToRender[j].a1 == a2)
            //                    {
            //                        newWallToRender = new Wall(x1, y1, ToRender[j].x2, ToRender[j].y2);
            //                        newWallToRender.a1 = a1;
            //                        newWallToRender.a2 = ToRender[j].a2;
            //                        newWallToRender.l1 = l1;
            //                        newWallToRender.l2 = ToRender[j].l2;
            //                    }
            //                    ToRender.RemoveAt(j);
            //                    ToRender.Insert(j, newWallToRender);
            //                    j--;
            //                    if (j < i) i--;

            //                    break;
            //            }
            //        }


            //    }

            //}
            //------------------------------------------------------------------------------------------------------------------------------------

            /*Point[] RayPoints = new Point[10000];
            List<Wall> NewWallsToRender = new List<Wall>();
            for (int i =0;i<10000;i++)//выпускаем лучи 
            {
                double angle = 0;
                if (MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView / 1000.0>0 && MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView / 1000.0<Math.PI*2)
                    angle = MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView/1000.0;
                if (MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView / 1000.0 <= 0)
                    angle = Math.PI * 2 - Math.Abs(MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView / 1000.0);
                if (MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView / 1000.0 >= Math.PI * 2)
                    angle = MainPlayer.a - MainPlayer.FieldOfView / 2 + i * MainPlayer.FieldOfView / 1000.0 - Math.PI * 2;

                double lMin = double.MaxValue;
                for (int j = 0; j < ToRender.Count; j++)
                {
                    bool isCover = false;
                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) <= Math.PI && (angle > Math.Min(ToRender[j].a1, ToRender[j].a2) && angle < Math.Max(ToRender[j].a1, ToRender[j].a2)))
                        isCover = true;
                    if (Math.Abs(ToRender[j].a1 - ToRender[j].a2) > Math.PI && (angle > Math.Max(ToRender[j].a1, ToRender[j].a2) || angle < Math.Min(ToRender[j].a1, ToRender[j].a2)))
                        isCover = true;
                    if(isCover)
                    {
                        double l;
                        double gamma = Math.Acos((ToRender[j].length * ToRender[j].length + ToRender[j].l2 * ToRender[j].l2 - ToRender[j].l1 * ToRender[j].l1) / (2 * ToRender[j].l2 * ToRender[j].length));
                        double alpha = Math.Min(Math.Abs(angle - ToRender[j].a2), Math.PI * 2 - Math.Abs(angle - ToRender[j].a2));
                        double betta = Math.PI - alpha - gamma;
                        l = (ToRender[j].l1 * Math.Sin(gamma)) / Math.Sin(betta);
                        lMin = Math.Min(lMin, l);
                    }
                }
                if (lMin < 10)
                {
                    RayPoints[i] = new Point(lMin * Math.Cos(angle), lMin * Math.Sin(angle));
                }
                else
                    RayPoints[i] = new Point(MainPlayer.X,MainPlayer.Y);
            }

            for(int i =0;i<9999;i++)
            {
                if (!(RayPoints[i].X == MainPlayer.X && RayPoints[i].Y == MainPlayer.Y))
                    if (!(RayPoints[i + 1].X == MainPlayer.X && RayPoints[i + 1].Y == MainPlayer.Y))
                        if (!(RayPoints[i].X == MainPlayer.X && RayPoints[i].Y == MainPlayer.Y && RayPoints[i + 1].X == MainPlayer.X && RayPoints[i + 1].Y == MainPlayer.Y))
                        {
                            NewWallsToRender.Add(new Wall(RayPoints[i].X - MainPlayer.X, RayPoints[i].Y - MainPlayer.Y, RayPoints[i + 1].X - MainPlayer.X, RayPoints[i + 1].Y - MainPlayer.Y));
                            double l1, a1, l2, a2;
                            l1 = Math.Sqrt(NewWallsToRender[NewWallsToRender.Count - 1].x1 * NewWallsToRender[NewWallsToRender.Count - 1].x1 + NewWallsToRender[NewWallsToRender.Count - 1].y1 * NewWallsToRender[NewWallsToRender.Count - 1].y1); a1 = 0;
                            l2 = Math.Sqrt(NewWallsToRender[NewWallsToRender.Count - 1].x2 * NewWallsToRender[NewWallsToRender.Count - 1].x2 + NewWallsToRender[NewWallsToRender.Count - 1].y2 * NewWallsToRender[NewWallsToRender.Count - 1].y2); a2 = 0;

                            if (NewWallsToRender[NewWallsToRender.Count - 1].y1 >= 0) a1 = Math.Acos(NewWallsToRender[NewWallsToRender.Count - 1].x1 / l1);
                            if (NewWallsToRender[NewWallsToRender.Count - 1].y1 < 0) a1 = Math.PI * 2 - Math.Acos(NewWallsToRender[NewWallsToRender.Count - 1].x1 / l1);

                            if (NewWallsToRender[NewWallsToRender.Count - 1].y2 >= 0) a2 = Math.Acos(NewWallsToRender[NewWallsToRender.Count - 1].x2 / l2);
                            if (NewWallsToRender[NewWallsToRender.Count - 1].y2 < 0) a2 = Math.PI * 2 - Math.Acos(NewWallsToRender[NewWallsToRender.Count - 1].x2 / l2);
                            NewWallsToRender[NewWallsToRender.Count - 1].a1 = a1;
                            NewWallsToRender[NewWallsToRender.Count - 1].a2 = a2;
                            NewWallsToRender[NewWallsToRender.Count - 1].l1 = l1;
                            NewWallsToRender[NewWallsToRender.Count - 1].l2 = l2;
                        }
            }*/
            //------------------------------------------------------------------------------------------------------------------------------------
            MiniMap.Children.RemoveRange(0, MiniMap.Children.Count);
            for (int i =0;i<ToRender.Count;i++)
            {
                
                Line l = new Line();
                l.X1 = Math.Cos(ToRender[i].a1) * ToRender[i].l1*10 + 246/2;
                l.Y1 = Math.Sin(ToRender[i].a1) * ToRender[i].l1*10+ 246 / 2;
                l.X2 = Math.Cos(ToRender[i].a2) * ToRender[i].l2*10+ 246 / 2;
                l.Y2 = Math.Sin(ToRender[i].a2) * ToRender[i].l2*10+ 246 / 2;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 1;
                MiniMap.Children.Add(l);
            }
            Ellipse player = new Ellipse()
            {
                Fill = Brushes.Red,
                Height = 10,
                Width = 10,
            };
            Canvas.SetTop(player, 246 / 2-5);
            Canvas.SetLeft(player, 246 / 2-5);
            Line Left, Right;
            Left = new Line()
            {
                X1 = 246 / 2,
                Y1 = 246 / 2,
                X2 = Math.Cos(MainPlayer.a + MainPlayer.FieldOfView / 2) * 100 + 246 / 2,
                Y2 = Math.Sin(MainPlayer.a + MainPlayer.FieldOfView / 2) * 100 + 246 / 2,
                StrokeThickness = 1,
                Stroke = Brushes.Red,
            };
            Right = new Line()
            {
                X1 = 246 / 2,
                Y1 = 246 / 2,
                X2 = Math.Cos(MainPlayer.a - MainPlayer.FieldOfView / 2) * 100 + 246 / 2,
                Y2 = Math.Sin(MainPlayer.a - MainPlayer.FieldOfView / 2) * 100 + 246 / 2,
                StrokeThickness = 1,
                Stroke = Brushes.Red,
            };
            MiniMap.Children.Add(Left);
            MiniMap.Children.Add(Right);
            MiniMap.Children.Add(player);

            MainCanvas.Children.RemoveRange(0, MainCanvas.Children.Count);
            for (int i = 0; i < ToRender.Count; i++)
                RenderProjectionOnPlane(ToRender[i]);
        }


        private void RenderProjectionOnPlane(Wall wall)
        {
            double H = (Application.Current.MainWindow.Width/2) / Math.Tan((MainPlayer.FieldOfView / 2 - 0.115));
            
            double x1, x2; //координаты на экране

            if (MainPlayer.a < 0) MainPlayer.a += Math.PI * 2;
            if (MainPlayer.a > Math.PI * 2) MainPlayer.a -= Math.PI * 2;

            x1 = (Application.Current.MainWindow.Width / 2) - H * Math.Sin(-MainPlayer.a + wall.a1);
            x2 = (Application.Current.MainWindow.Width / 2) - H * Math.Sin(-MainPlayer.a + wall.a2);

            double FullHeight = Application.Current.MainWindow.Height;

            double h1 = 1 / (wall.l1 * Math.Abs(Math.Cos(-MainPlayer.a + wall.a1)));
            double h2 = 1 / (wall.l2 * Math.Abs(Math.Cos(-MainPlayer.a + wall.a2)));
               
            SolidColorBrush BrushToShow3D = new SolidColorBrush();
            BrushToShow3D.Color = Color.FromArgb(255, (byte)(200 * ((h1 + h2) / 2 + 0.5)), (byte)(200 * ((h1 + h2) / 2 + 0.5)), (byte)(200 * ((h1 + h2) / 2 + 0.5)));
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(@"Textures\BrickWall.jpg",UriKind.Relative));
            //imgBrush.Stretch = Stretch.Fill;
            Polygon poly = new Polygon()
            {
                Fill = BrushToShow3D,
            };
            poly.Points.Add(new System.Windows.Point(x1, FullHeight / 2 - h1 * 1200 / 2));
            poly.Points.Add(new System.Windows.Point(x1, FullHeight / 2 + h1 * 1200 / 2));

            poly.Points.Add(new System.Windows.Point(x2, FullHeight / 2 + h2 * 1200 / 2));
            poly.Points.Add(new System.Windows.Point(x2, FullHeight / 2 - h2 * 1200 / 2));


            MainCanvas.Children.Add(poly);
        }

    }

   
}

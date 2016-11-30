using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3d_render
{
    class Vertex
    {
        double _x;
        double _y;
        double _z;
        double _finalX;
        double _finalY;
        Ellipse point;
        List<Vector> _normals;

        public Vertex(double x, double y, double z)
        {
            Z = z;
            X = x;
            Y = y;
            FinalX = Convert.ToInt32(Constants.center + (x - z * Math.Cos(Constants.angl / 180 * Math.PI) / 2)* Constants.zoom);
            FinalY = Convert.ToInt32(Constants.center - (y + z * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            _normals = new List<Vector>();
        }

        public void DrawVertex(Canvas canvas)
        {
            point = new Ellipse();
            point.Width = 2;
            point.Height = 2;
            point.Fill = Brushes.White;
            point.Stroke = Brushes.White;
            point.StrokeThickness = 0.5;
            point.Margin = new System.Windows.Thickness(FinalX, FinalY, 0, 0);
            canvas.Children.Add(point);
        }

        public void HorizontalMove(int direction)
        {
            X += Constants.delta * direction;
            FinalX = Convert.ToInt32(Constants.center + (X - Z * Math.Cos(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            if (point != null)
            {
                point.Margin = new System.Windows.Thickness(FinalX,point.Margin.Top, 0, 0);
                //TranslateTransform tranform = new TranslateTransform(Convert.ToInt32(Constants.center + (Constants.delta * direction - Z * Math.Cos(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom), 0);
                //point.RenderTransform = tranform;
            }
            ////Z += Constants.delta * direction;
            //FinalY += Constants.delta * direction;
        }

        public void VerticalMove(int direction)
        {
            Y += Constants.delta * direction;
            FinalY = Convert.ToInt32(Constants.center - (Y + Z * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            if (point != null)
            {
                //TranslateTransform tranform = new TranslateTransform(0, Convert.ToInt32(Constants.center - (Constants.delta * direction + Z * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom));
                //point.RenderTransform = tranform;
                point.Margin = new System.Windows.Thickness(point.Margin.Left, FinalY, 0, 0);
            }
            //X += Constants.delta * direction;
            ////Z += Constants.delta * direction;
            //FinalX += Constants.delta * direction;
        }

        public void TurnVertex(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            X -= x1; Y -= y1; Z -= z1;

            double length = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));
            double xn = (x2-x1);
            xn *= 1 / length;
            double yn = (y2-y1);
            yn *= 1 / length;
            double zn = (z2-z1);
            zn *= 1 / length;
            double X1 = X;
            double Y1 = Y;
            double Z1 = Z;

            X = X1 * (Math.Cos(Constants.deltaAngl * Math.PI / 180) + (1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * Math.Pow(xn, 2)) +
                Y1 * ((1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * yn * xn + Math.Sin(Constants.deltaAngl * Math.PI / 180) * zn) +
                Z1 * ((1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * zn * xn - Math.Sin(Constants.deltaAngl * Math.PI / 180) * yn);
            Y = Y1 * (Math.Cos(Constants.deltaAngl * Math.PI / 180) + (1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * Math.Pow(yn, 2)) +
                X1 * ((1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * yn * xn - Math.Sin(Constants.deltaAngl * Math.PI / 180) * zn) +
                Z1 * ((1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * zn * yn + Math.Sin(Constants.deltaAngl * Math.PI / 180) * xn);
            Z = Z1 * (Math.Cos(Constants.deltaAngl * Math.PI / 180) + (1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * Math.Pow(zn, 2)) +
                X1 * ((1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * zn * xn + Math.Sin(Constants.deltaAngl * Math.PI / 180) * yn) +
                Y1 * ((1 - Math.Cos(Constants.deltaAngl * Math.PI / 180)) * zn * yn - Math.Sin(Constants.deltaAngl * Math.PI / 180) * xn);

            X += x1; Y += y1; Z += z1;

            FinalX = Convert.ToInt32(Constants.center + (X - Z * Math.Cos(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            FinalY = Convert.ToInt32(Constants.center - (Y + Z * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            if (point != null)
            {
                point.Margin = new System.Windows.Thickness(FinalX, FinalY, 0, 0);
            }
        }

        public void Zoom()
        {
            FinalX = Convert.ToInt32(Constants.center + (X - Z * Math.Cos(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            FinalY = Convert.ToInt32(Constants.center - (Y + Z * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            if (point != null)
            {
                point.Margin = new System.Windows.Thickness(FinalX, FinalY, 0, 0);
            }
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double FinalX
        {
            get { return _finalX; }
            set { _finalX = value; }
        }

        public double FinalY
        {
            get { return _finalY; }
            set { _finalY = value; }
        }

        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }
    }
}

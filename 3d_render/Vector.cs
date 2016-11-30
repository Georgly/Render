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
    class Vector
    {
        double _x1;
        double _y1;
        double _z1;
        double _x2;
        double _y2;
        double _z2;
        Vertex _pointVector;
        double _length;

        public Vector(double x, double y, double z)
        {
            PointVector = new Vertex(x, -y, z);
            _length = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        public Vector(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            X1 = x1;
            Y1 = y1;
            Z1 = z1;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
            PointVector = new Vertex(X2 - X1, -(Y2 - Y1), Z2 - Z1);
            _length = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) + Math.Pow(z2 - z1, 2));
        }

        public void DrawVector(Canvas canvas)
        {
            Line vector = new Line();
            vector.X1 = Convert.ToInt32(Constants.center + (X1 - Z1 * Math.Cos(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            vector.Y1 = Convert.ToInt32(Constants.center - (Y1 + Z1 * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            vector.X2 = Convert.ToInt32(Constants.center + (X2 - Z2 * Math.Cos(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            vector.Y2 = Convert.ToInt32(Constants.center - (Y2 + Z2 * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
            vector.Stroke = Brushes.Red;
            vector.StrokeThickness = 2;
            canvas.Children.Add(vector);
        }

        public void HorizontalMove(int direction)
        {
            //TranslateTransform tranform = new TranslateTransform(Constants.delta * direction, 0);
            PointVector.HorizontalMove(direction);
            X1 += Constants.delta * direction;
            ////Z1 += Constants.delta * direction;
            X2 += Constants.delta * direction;
            ////Z2 += Constants.delta * direction;
            //FinalX += Constants.delta * direction;
            //FinalY += Constants.delta * direction;
        }

        public void VerticalMove(int direction)
        {
            //TranslateTransform tranform = new TranslateTransform(0, Constants.delta * direction);
            //point.RenderTransform = tranform;
            PointVector.VerticalMove(direction);
            //X += Constants.delta * direction;
            //Y += Constants.delta * direction;
            //Z += Constants.delta * direction;
            //FinalX += Constants.delta * direction;
            //FinalY += Constants.delta * direction;
            Y1 += Constants.delta * direction;
            ////Z1 += Constants.delta * direction;
            Y2 += Constants.delta * direction;
            ////Z2 += Constants.delta * direction;
        }

        public static Vector Normalize(Vector vector)
        {
            vector.PointVector.X /= vector.Length;
            vector.PointVector.Y /= vector.Length;
            vector.PointVector.Z /= vector.Length;
            vector._length = 1;
            return vector;
        }

        public double X1
        {
            get { return _x1; }
            set { _x1 = value; }
        }

        public double Y1
        {
            get { return _y1; }
            set { _y1 = value; }
        }

        public double Z1
        {
            get { return _z1; }
            set { _z1 = value; }
        }

        public double X2
        {
            get { return _x2; }
            set { _x2 = value; }
        }

        public double Y2
        {
            get { return _y2; }
            set { _y2 = value; }
        }

        public double Z2
        {
            get { return _z2; }
            set { _z2 = value; }
        }

        public Vertex PointVector
        {
            get { return _pointVector; }
            set { _pointVector = value; }
        }

        public double Length
        {
            get { return _length; }
        }
    }
}

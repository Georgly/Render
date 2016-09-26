using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3d_render
{
    class Vertex
    {
        double _x;
        double _y;
        //double _z;

        public Vertex(double x, double y, double z)
        {
            //_z = z;
            _x = Convert.ToInt32(Constants.center + (x - z * Math.Cos(Constants.angl / 180 * Math.PI) / 2)* Constants.zoom);
            _y = Convert.ToInt32(Constants.center - (y + z * Math.Sin(Constants.angl / 180 * Math.PI) / 2) * Constants.zoom);
        }

        public Ellipse DrawVertex()
        {
            Ellipse point = new Ellipse();
            point.Width = 1;
            point.Height = 1;
            point.Fill = Brushes.White;
            point.Stroke = Brushes.White;
            point.StrokeThickness = 0.5;
            TranslateTransform transform = new TranslateTransform(X, Y);
            point.RenderTransform = transform;
            return point;
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

        //public double Z
        //{
        //    get { return _z; }
        //    set { _z = value; }
        //}
    }
}

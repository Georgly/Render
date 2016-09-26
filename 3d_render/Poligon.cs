using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3d_render
{
    class Poligon
    {
        List<int> _points;

        public Poligon(List<int> points)
        {
            Points = points;
        }

        public Polyline DrawPoligon(List<Vertex> points)
        {
            PointCollection myPoints = new PointCollection();
            for (int i = 0; i < Points.Count; i++)
            {
                myPoints.Add(new System.Windows.Point(points[Points[i] - 1].X, points[Points[i] - 1].Y));
            }
            Polyline poligon = new Polyline();
            poligon.Points = myPoints;
            //poligon.Fill = Brushes.White;
            poligon.Stroke = Brushes.White;
            poligon.StrokeThickness = 0.5;
            //poligon.Width = 1;
            //poligon.Height = 1;

            return poligon;
        }

        public List<int> Points
        {
            get { return _points; }
            set { _points = value; }
        }
    }
}

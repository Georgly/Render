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
    class Poligon
    {
        List<int> _points;
        SolidColorBrush color;
        double _ratio { get; set; }
        Vector _normalVect;
        Polyline poligon;
        Polygon polygon;
        public Vector _sunVect { get; set; }
        //double angleCamera;

        public Poligon(List<int> points)
        {
            Points = points;
            _ratio = 1;
        }

        public void DrawPoligon(List<Vertex> points, Canvas canvas)
        {
            //FindNormal(points[Points[0] - 1], points[Points[1] - 1], points[Points[2] - 1]);
            PointCollection myPoints = new PointCollection();
            for (int i = 0; i < Points.Count; i++)
            {
                myPoints.Add(new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY));
            }
            myPoints.Add(new System.Windows.Point(points[Points[0] - 1].FinalX, points[Points[0] - 1].FinalY));
            color = new SolidColorBrush(Color.FromRgb(Convert.ToByte(255), Convert.ToByte(255), Convert.ToByte(255)));
            poligon = new Polyline();
            poligon.Points = myPoints;
            poligon.Stroke = color;
            poligon.StrokeThickness = 0.5;
            canvas.Children.Add(poligon);
        }

        public void DrawFillPoligon(List<Vertex> points, Vector sun, Canvas canvas/*, Vector camera*/)
        {
            _sunVect = sun;
            //FindNormal(points[Points[0] - 1], points[Points[1] - 1], points[Points[2] - 1]);
            //NormalVector = NormalVector.Normalize(_normalVect);
            //FindCameraAngle(camera, Vector.Normalize(_normalVect));
            //if (angleCamera < 0)
            //{
            //    return;
            //}
            FindRatio(_sunVect, Vector.Normalize(_normalVect));
            if (_ratio < 0)
            {
                _ratio = 0;
            }
            else if (_ratio > 1)
            {
                _ratio = 1;
            }
            PointCollection myPoints = new PointCollection();
            for (int i = 0; i < Points.Count; i++)
            {
                myPoints.Add(new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY));
            }
            Random rand = new Random();
            color = new SolidColorBrush(Color.FromRgb(Convert.ToByte(_ratio * 255), Convert.ToByte(_ratio * 255), Convert.ToByte(_ratio * 255)));
            polygon = new Polygon();
            //Canvas.SetZIndex(polygon, Convert.ToInt32(Model.camera.Z - NormalVector.Z1));
            //SetZIndex();
            polygon.Points = myPoints;
            polygon.Stroke = color;
            polygon.Fill = color;
            polygon.StrokeThickness = 0.5;
            canvas.Children.Add(polygon);
        }

        public void FindNormal(Vertex first, Vertex second, Vertex third)
        {
            double normX = (first.Y - second.Y) * (third.Z - second.Z) - (first.Z - second.Z) * (third.Y - second.Y);
            double normY = (-(first.X - second.X) * (third.Z - second.Z) + (first.Z - second.Z) * (third.X - second.X));
            double normZ = (first.X - second.X) * (third.Y - second.Y) - (first.Y - second.Y) * (third.X - second.X);
            double length = Math.Sqrt(Math.Pow(normX, 2) + Math.Pow(normY, 2) + Math.Pow(normZ, 2));
            double x0 = (first.X + second.X + third.X) / 3;
            double y0 = (first.Y + second.Y + third.Y) / 3;
            double z0 = (first.Z + second.Z + third.Z) / 3;
            NormalVector = new Vector(x0, y0, z0, x0 - normX / length * 10, y0 - normY / length * 10, z0 - normZ / length * 10);
        }

        public void HorizontalMove(int direction, List<Vertex> points)
        {
            //double delX = NormalVector.PointVector.FinalX;
            //NormalVector.HorizontalMove(direction);
            FindNormal(points[Points[0] - 1], points[Points[1] - 1], points[Points[2] - 1]);
            if (poligon != null)
            {
                for (int i = 0; i < poligon.Points.Count - 1; i++)
                {
                    poligon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
                poligon.Points[poligon.Points.Count - 1] = new System.Windows.Point(points[Points[0] - 1].FinalX, points[Points[0] - 1].FinalY);
                //poligon.Margin = new System.Windows.Thickness(poligon.Margin.Left + (NormalVector.PointVector.FinalX - delX), poligon.Margin.Top, 0, 0);
            }
            if (polygon != null)
            {
                FindRatio(_sunVect, Vector.Normalize(_normalVect));
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
            }
        }

        public void VerticalMove(int direction, List<Vertex> points)
        {
            //double delY = NormalVector.PointVector.FinalY;
            //NormalVector.VerticalMove(direction);
            FindNormal(points[Points[0] - 1], points[Points[1] - 1], points[Points[2] - 1]);
            if (poligon != null)
            {
                for (int i = 0; i < poligon.Points.Count - 1; i++)
                {
                    poligon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
                poligon.Points[poligon.Points.Count - 1] = new System.Windows.Point(points[Points[0] - 1].FinalX, points[Points[0] - 1].FinalY);
            }
            if (polygon != null)
            {
                FindRatio(_sunVect, Vector.Normalize(_normalVect));
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
            }
        }

        public void TurnPoligon(/*int x, int y, int z, */List<Vertex> points)
        {
            if (poligon != null)
            {
                for (int i = 0; i < poligon.Points.Count - 1; i++)
                {
                    poligon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
                poligon.Points[poligon.Points.Count - 1] = new System.Windows.Point(points[Points[0] - 1].FinalX, points[Points[0] - 1].FinalY);
            }
        }

        public void TurnFillP(/*int x1, int y1, int z1, int x2, int y2, int z2, */List<Vertex> points)
        {
            FindNormal(points[Points[0] - 1], points[Points[1] - 1], points[Points[2] - 1]);
            if (polygon != null)
            {//изменение положения полигонов
                FindRatio(_sunVect, Vector.Normalize(_normalVect));
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
                //SetZIndex();
            }
        }

        public void Zoom(List<Vertex> points)
        {
            FindNormal(points[Points[0] - 1], points[Points[1] - 1], points[Points[2] - 1]);
            if (poligon != null)
            {
                for (int i = 0; i < poligon.Points.Count - 1; i++)
                {
                    poligon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
                poligon.Points[poligon.Points.Count - 1] = new System.Windows.Point(points[Points[0] - 1].FinalX, points[Points[0] - 1].FinalY);
            }
            if (polygon != null)
            {
                FindRatio(_sunVect, Vector.Normalize(_normalVect));
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new System.Windows.Point(points[Points[i] - 1].FinalX, points[Points[i] - 1].FinalY);
                }
            }
        }

        //void SetZIndex()
        //{
        //    if (Model.camera.Z >= 0)
        //    {
        //        Canvas.SetZIndex(polygon, -Convert.ToInt32(/*Model.camera.Z - (*/Math.Abs(NormalVector.Z1)));
        //    }
        //    else
        //    {
        //        Canvas.SetZIndex(polygon, Convert.ToInt32(/*Model.camera.Z + (*/NormalVector.Z1));
        //    }
        //}

        void FindRatio(Vector sun, Vector normalVector)
        {
            _ratio = (sun.PointVector.X * normalVector.PointVector.X + sun.PointVector.Y * normalVector.PointVector.Y + sun.PointVector.Z * normalVector.PointVector.Z) / (sun.Length * normalVector.Length);
        }

        //void FindCameraAngle(Vector camera, Vector normalVector)
        //{
        //    angleCamera = (camera.PointVector.X * normalVector.PointVector.X + camera.PointVector.Y * normalVector.PointVector.Y + camera.PointVector.Z * normalVector.PointVector.Z) / (camera.Length * normalVector.Length);
        //}

        public Vector NormalVector
        {
            get { return _normalVect; }
            set { _normalVect = value; }
        }

        public List<int> Points
        {
            get { return _points; }
            set { _points = value; }
        }
    }
}

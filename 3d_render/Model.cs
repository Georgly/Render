using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace _3d_render
{
    class Constants
    {
        public const int center = 500;
        public const int angl = 45;
        public static double zoom = 2;
        public const int delta = 15;
        public const int deltaAngl = 15;
    }


    class Model
    {

        class CoordCompare : IComparer<Poligon>
        {
            public int Compare(Poligon first, Poligon second)
            {
                return second.NormalVector.Z1.CompareTo(first.NormalVector.Z1); 
            }
        }

        List<Vertex> _vertexes;
        List<Poligon> _poligones;
        /*public */Vector sun = new Vector(-500, 500, 5500, Constants.center, Constants.center, Constants.center);
        /*public static*/
        Vector camera;/* = new Vector(500, 500, 5500, figureCenter.X1, figureCenter.Y1, figureCenter.Z1);/*Constants.center, Constants.center, Constants.center);*/
        bool ready = false;
        Canvas _canvas = new Canvas();
        BackgroundWorker worker;
        CoordCompare compareZ = new CoordCompare();
        Vector figureCenter;

        public Model(Canvas canvas, int zoomSl)
        {
            Constants.zoom = zoomSl;
            _canvas = canvas;
            _vertexes = new List<Vertex>();
            _poligones = new List<Poligon>();
        }

        public void OpenFile(string fileName)
        {
            StreamReader file = null;
            try
            {
                file = new StreamReader(fileName);
                Parse(file, fileName);
            }
            catch (FileNotFoundException)
            {
                Error.NoFile();
            }
            catch (IOException)
            {
                Error.EnterError();
            }
            catch (Exception)
            {
                Error.SomeError();
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        void Parse(StreamReader file, string fileName)
        {
            StreamReader parsFile = file;
            _vertexes = new List<Vertex>();
            _poligones = new List<Poligon>();

            double sumX = 0;
            double sumY = 0;
            double sumZ = 0;
            int count = 0;

            string textLine;
            while ((textLine = parsFile.ReadLine()) != null)
            {
                if (textLine != "")
                {
                    string[] tempt = textLine.Split(' ');
                    switch (tempt[0])
                    {
                        case "v":
                            {
                                double[] points = new double[3];
                                for (int i = 1, j = 0; i < tempt.Length; i++)
                                {
                                    if (tempt[i] != "")
                                    {
                                        points[j] = Convert.ToDouble(tempt[i].Replace('.', ','));
                                        j++;
                                    }
                                }
                                Vertex point = new Vertex(points[0], points[1], points[2]);
                                sumX += points[0];
                                sumY += points[1];
                                sumZ += points[2];
                                count++;
                                _vertexes.Add(point);
                                break;
                            }
                        case "f":
                            {
                                Poligon poligonPoint;
                                List<int> points = new List<int>();
                                for (int i = 1; i < tempt.Length; i++)
                                {
                                    int j = 0;
                                    string coord = "";
                                    while (j < tempt[i].Length && (int)tempt[i][j] >= 48 && (int)tempt[i][j] <= 57)
                                    {
                                        coord += tempt[i][j];
                                        j++;
                                    }
                                    if (coord != "")
                                    {
                                        points.Add(Convert.ToInt32(coord));
                                    }
                                }
                                poligonPoint = new Poligon(points);
                                _poligones.Add(poligonPoint);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            figureCenter = new Vector(sumX / count, sumY / count, sumZ / count);
            camera = new Vector(500, 500, 5500, figureCenter.PointVector.X, figureCenter.PointVector.Y, figureCenter.PointVector.Z);
            //Normales of poligones in another thread
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
            parsFile.Close();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < _poligones.Count; i++)
            {
                _poligones[i].FindNormal(
                    _vertexes[_poligones[i].Points[0] - 1], _vertexes[_poligones[i].Points[1] - 1], _vertexes[_poligones[i].Points[2] - 1]);
            }
            _poligones.Sort(compareZ);
            ready = true;
            worker.CancelAsync();
        }

        //public void DrawModelPoint(Canvas canvas)
        //{
        //    if (_vertexes != null)
        //    {
        //        for (int i = 0; i < _vertexes.Count; i++)
        //        {
        //            _vertexes[i].DrawVertex(canvas);//);
        //        }
        //    }
        //}

        public void DrawModelPolygon(Canvas canvas)
        {
            for (int i = 0; i < _poligones.Count; i++)
            {
                _poligones[i].DrawPoligon(_vertexes, canvas);
                //_poligones[i].NormalVector.DrawVector(canvas);
            }
        }

        public void DrawModelFill(Canvas canvas)
        {
            while (!ready){}
            for (int i = 0; i < _poligones.Count; i++)
            {
                _poligones[i].DrawFillPoligon(_vertexes, sun, canvas/*, camera*/);
                //poligon.Add(_poligones[i].NormalVector.DrawVector());
            }
        }

        public void HorizontalMove(int direction, int clientCount)
        {
            sun.HorizontalMove(direction);
            switch (clientCount)
            {
                case 1:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].HorizontalMove(direction);
                        }
                        break;
                    }
                case 2:
                case 3:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].HorizontalMove(direction);
                        }
                        for (int i = 0; i < _poligones.Count; i++)
                        {
                            _poligones[i]._sunVect = sun;
                            _poligones[i].HorizontalMove(direction, _vertexes);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public void VerticalMove(int direction, int clientCount)
        {
            sun.VerticalMove(direction);
            switch (clientCount)
            {
                case 1:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].VerticalMove(direction);
                        }
                        break;
                    }
                case 2:
                case 3:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].VerticalMove(direction);
                        }
                        for (int i = 0; i < _poligones.Count; i++)
                        {
                            _poligones[i]._sunVect = sun;
                            _poligones[i].VerticalMove(direction, _vertexes);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public void TurnModel(int clientCount, int x2, int y2, int z2)
        {
            switch (clientCount)
            {
                case 1:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].TurnVertex((int)figureCenter.PointVector.X, (int)figureCenter.PointVector.Y, (int)figureCenter.PointVector.Z, (int)figureCenter.PointVector.X + x2, (int)figureCenter.PointVector.Y + y2, (int)figureCenter.PointVector.Z + z2);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].TurnVertex((int)figureCenter.PointVector.X, (int)figureCenter.PointVector.Y, (int)figureCenter.PointVector.Z, (int)figureCenter.PointVector.X + x2, (int)figureCenter.PointVector.Y + y2, (int)figureCenter.PointVector.Z + z2);
                        }
                        for (int i = 0; i < _poligones.Count; i++)
                        {
                            _poligones[i]._sunVect = sun;
                            _poligones[i].TurnPoligon(/*x1, y1, z1, x2, y2, z2, */_vertexes);
                        }
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].TurnVertex((int)figureCenter.PointVector.X, (int)figureCenter.PointVector.Y, (int)figureCenter.PointVector.Z, (int)figureCenter.PointVector.X + x2, (int)figureCenter.PointVector.Y + y2, (int)figureCenter.PointVector.Z + z2);
                        }
                        for (int i = 0; i < _poligones.Count; i++)
                        {
                            _poligones[i]._sunVect = sun;
                            _poligones[i].TurnFillP(/*x1, y1, z1, */_vertexes);
                        }
                        _poligones.Sort(compareZ);
                        _canvas.Children.Clear();
                        DrawModelFill(_canvas);
                        //for (int i = 0; i < _poligones.Count; i++)
                        //{
                        //    Canvas.SetZIndex(_poligones[i].polygon, i);
                        //}
                        break;
                    }
                default:
                    break;
            }
        }

        public void Zoom(int clientCount, int zoomSl)
        {
            Constants.zoom = zoomSl;
            switch (clientCount)
            {
                case 1:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].Zoom();
                        }
                        break;
                    }
                case 2:
                case 3:
                    {
                        for (int i = 0; i < _vertexes.Count; i++)
                        {
                            _vertexes[i].Zoom();
                        }
                        for (int i = 0; i < _poligones.Count; i++)
                        {
                            _poligones[i]._sunVect = sun;
                            _poligones[i].Zoom(_vertexes);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}

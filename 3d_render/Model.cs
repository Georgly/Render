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

namespace _3d_render
{
    class Constants
    {
        public const int center = 500;
        public const int angl = 45;
        public const int zoom = 2;
    }
    class Model
    {
        static List<Vertex> _vertexes;
        static List<Poligon> _poligones;

        public static void OpenFile(string fileName)
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

        static void Parse(StreamReader file, string fileName)
        {
            StreamReader parsFile = file;
            _vertexes = new List<Vertex>();
            _poligones = new List<Poligon>();

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
                                Vertex point = new Vertex(Convert.ToDouble(tempt[1].Replace('.', ',')), Convert.ToDouble(tempt[2].Replace('.', ',')), Convert.ToDouble(tempt[3].Replace('.', ',')));
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
            parsFile.Close();
        }

        public static List<Ellipse> DrawModelPoint()
        {
            //Canvas myCanvas = new Canvas();
            //myCanvas.Background = Brushes.Black;
            List<Ellipse> points = new List<Ellipse>();
            for (int i = 0; i < _vertexes.Count; i++)
            {
                points.Add(_vertexes[i].DrawVertex());
            }
            return points;
        }

        public static List<Polyline> DrawModelPolygon()
        {
            List<Polyline> poligon = new List<Polyline>();
            for (int i = 0; i < _poligones.Count; i++)
            {
                poligon.Add(_poligones[i].DrawPoligon(_vertexes));
            }
            return poligon;
        }

        //public List<Vertex> Vertexes
        //{
        //    get { return _vertexes; }
        //    set { _vertexes = value; }
        //}

        //public List<Poligon> Poligones
        //{
        //    get { return _poligones; }
        //    set { _poligones = value; }
        //}
    }
}

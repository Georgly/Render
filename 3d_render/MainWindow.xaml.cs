using Microsoft.Win32;
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

namespace _3d_render
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int clickCount = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _3D_Render_Load(object sender, RoutedEventArgs e)
        {

        }

        private void openFileBt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.ShowDialog();
            Model.OpenFile(openFD.FileName);
        }

        private void drawModelBt_Click(object sender, RoutedEventArgs e)
        {
            myCanva.Children.Clear();
            switch (clickCount)
            {
                case 1:
                    {
                        List<Ellipse> elipse = Model.DrawModelPoint();
                        for (int i = 0; i < elipse.Count; i++)
                        {
                            myCanva.Children.Add(elipse[i]);
                        }
                        clickCount++;
                        break;
                    }
                case 2:
                    {
                        List<Polyline> poligons = Model.DrawModelPolygon();
                        for (int i = 0; i < poligons.Count; i++)
                        {
                            myCanva.Children.Add(poligons[i]);
                        }
                        clickCount++;
                        break;
                    }
                case 3:
                    {
                        clickCount = 1;
                        break;
                    }
                default:
                    break;
            }
            //myGrid.Children.Add(Model.DrawModelPoint());
        }
    }
}

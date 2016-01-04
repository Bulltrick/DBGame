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

namespace DBGameMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {

        //List<Tile> 

        public MainWindow1()
        {
            InitializeComponent();
        }

        private void drawMap()
        {
            foreach (Tile t in map)
            {
                Button btn = new Button();
                string content = "";

                if (t.army != null) content += "A ";
                if (t.city != null) content += "C ";
                if (t.construction != null) content += "B ";

                content += "                 £x" + t.x + " £y" + t.y + " ";

                if (t.terrain != null)
                {
                    switch (t.terrain.type)
                    {
                        case 1: btn.Background = new SolidColorBrush(Colors.LightYellow);
                            break;
                        case 2: btn.Background = new SolidColorBrush(Colors.LawnGreen);
                            break;
                        case 3: btn.Background = new SolidColorBrush(Colors.Blue);
                            break;
                        case 4: btn.Background = new SolidColorBrush(Colors.Gray);
                            break;
                        case 0: btn.Background = new SolidColorBrush(Colors.WhiteSmoke);
                            break;
                        default:
                            break;
                    }
                }


                btn.Content = content;
                btn.Height = 45;
                btn.Width = 45;
                Grid.SetColumn(btn, t.x);
                Grid.SetRow(btn, t.y);
                gridMap.Children.Add(btn);
                btn.Click += btn_Click;
                if (t.owner == activePlayer) btn.Foreground = new SolidColorBrush(Colors.Green);
                else if (t.owner == 0) btn.Foreground = new SolidColorBrush(Colors.Gray);
                else btn.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

    }
}

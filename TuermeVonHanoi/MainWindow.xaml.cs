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

namespace TuermeVonHanoi
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public int discCount;

        private Canvas _firstPanel;

        private int _leftPos;
        private int _midPos;
        private int _rightPos;

        private int _recHeight;

        public MainWindow()
        {
            InitializeComponent();
            refresh();

            //new Window1().Show();
        }

        /**
        * UI Element Funktionen
        */
        private void button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void LeftPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("LeftPanel_MouseLeftButtonDown: Called");
            if (_firstPanel == null)
            {
                Rectangle rect = this.LeftPanel.Children.OfType<Rectangle>().FirstOrDefault();

                if (rect == null) return;
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.CadetBlue);
                _firstPanel = LeftPanel;
            }
            else
            {
                moveRectFromTo(_firstPanel, this.LeftPanel);
                _firstPanel = null;
            }
        }

        private void MidPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("MidPanel_MouseLeftButtonDown: Called");
            if (_firstPanel == null)
            {
                Rectangle rect = MidPanel.Children.OfType<Rectangle>().FirstOrDefault();

                if (rect == null) return;
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.CadetBlue);
                _firstPanel = this.MidPanel;
            }
            else
            {
                Console.WriteLine("MidPanel_MouseLeftButtonDown: Called - 2 Click");
                moveRectFromTo(_firstPanel, this.MidPanel);
                Console.WriteLine("MidPanel_MouseLeftButtonDown: Moved");
                _firstPanel = null;
            }
        }

        private void RightPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_firstPanel == null)
            {
                Rectangle rect = this.RightPanel.Children.OfType<Rectangle>().FirstOrDefault();

                if (rect == null) return;
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.CadetBlue);
                _firstPanel = RightPanel;
            }
            else
            {
                moveRectFromTo(_firstPanel, this.RightPanel);
                _firstPanel = null;
            }
        }

        /**
        * Allgemeine Funktionen
        */
        // Löscht alle Elemente in den Panels und stellt die Ausgangssituation wieder her.
        public async void refresh()
        {
            // set amount of discs
            discCount = int.Parse(this.textBox.Text);

            // reset all panels
            this.LeftPanel.Children.Clear();
            this.MidPanel.Children.Clear();
            this.RightPanel.Children.Clear();

            // calculate height
            int maxHeight = 219;
            _recHeight = maxHeight / discCount;

            // calculate width
            int maxWidth = 120;
            int minWidth = 5;
            int widthReduce = (maxWidth - minWidth) / discCount;
            int width;

            // create rectangles
            for (int i = 0; i < discCount; i++)
            {
                // rectangle width
                width = minWidth + (i * widthReduce);

                Rectangle rect = createNewRect(width, new Thickness(0), _recHeight);
                Canvas.SetTop(rect, i * _recHeight);
                Canvas.SetLeft(rect, (maxWidth - width) / 2);
                LeftPanel.Children.Add(rect);
                await Task.Delay(2000);
            }

            // set positions
            _leftPos = 0;
            _midPos = discCount-1;
            _rightPos = discCount-1;
        }

        private Rectangle createNewRect(int width, Thickness margin, int height)
        {
            Rectangle rect = new Rectangle
            {
                Height = height,
                Width = width,
                Margin = margin,
                //VerticalAlignment = VerticalAlignment.Bottom,
                //HorizontalAlignment = HorizontalAlignment.Center,
                Fill = new SolidColorBrush(System.Windows.Media.Colors.YellowGreen)
            };

            return rect;
        }

        private void moveRectFromTo(Canvas panelFrom, Canvas panelTo)
        {
            Console.WriteLine("MoveRectFromTo: Called");
            if (panelFrom == panelTo) return;
            Console.WriteLine("MoveRectFromTo: Panels not even.");
            Rectangle rect = get(panelFrom);
            if (rect == null) return;
            Console.WriteLine("MoveRectFromTo: Got Panel.");
            put(rect, panelTo);
        }

        private void put(Rectangle rect, Canvas panel)
        {
            if (panel == LeftPanel)
            {
                Canvas.SetTop(rect, _leftPos * _recHeight);
                _leftPos--;
            } else if(panel == MidPanel)
            {
                Canvas.SetTop(rect, _midPos * _recHeight);
                _midPos--;
            } else
            {
                Canvas.SetTop(rect, _rightPos * _recHeight);
                _rightPos--;
            }
            panel.Children.Add(rect);
        }

        private Rectangle get(Canvas panel)
        {
            Rectangle rect = panel.Children.OfType<Rectangle>().FirstOrDefault();
            Console.WriteLine("get - " + rect == null);
            rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.YellowGreen);
            panel.Children.Remove(rect);

            if (panel == LeftPanel)
            {
                _leftPos++;
            }
            else if (panel == MidPanel)
            {
                _midPos++;
            }
            else
            {
                _rightPos++;
            }

            return rect;
        }
    }
}

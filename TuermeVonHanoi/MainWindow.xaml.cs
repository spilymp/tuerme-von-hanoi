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
        // Anzahl der Scheiben
        public int discCount;

        // Das erste angeklickte Canvas
        private Canvas _firstPanel;

        // Hilfsvariablen für die Positionierung
        private int _leftPos;
        private int _midPos;
        private int _rightPos;

        private int _recHeight;

        // Threading
        private System.Threading.Tasks.Task t;

        public MainWindow()
        {
            InitializeComponent();
            refresh();
        }

        /**
        * UI Element Funktionen
        */
        private void button_Solve_Click(object sender, RoutedEventArgs e)
        {
            refresh();

            this.button_Solve.Visibility = System.Windows.Visibility.Hidden;
            this.button_Solve.IsEnabled = false;
            this.button_Cancel.Visibility = System.Windows.Visibility.Visible;
            this.button_Cancel.IsEnabled = true;
            this.button_Refresh.IsEnabled = false;

            t = new System.Threading.Tasks.Task(() =>
            {
                solve(LeftPanel, RightPanel, MidPanel, discCount);
            });
            t.Start();
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.button_Solve.Visibility = System.Windows.Visibility.Visible;
            this.button_Solve.IsEnabled = true;
            this.button_Cancel.Visibility = System.Windows.Visibility.Hidden;
            this.button_Cancel.IsEnabled = false;
            this.button_Refresh.IsEnabled = true;

            refresh();
        }

        private void button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void LeftCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_firstPanel == null)
            {
                Rectangle rect = this.LeftPanel.Children.OfType<Rectangle>().LastOrDefault();

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

        private void MidCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_firstPanel == null)
            {
                Rectangle rect = MidPanel.Children.OfType<Rectangle>().LastOrDefault();

                if (rect == null) return;
                rect.Fill = new SolidColorBrush(Colors.CadetBlue);
                _firstPanel = this.MidPanel;
            }
            else
            {
                moveRectFromTo(_firstPanel, this.MidPanel);
                _firstPanel = null;
            }
        }

        private void RightCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_firstPanel == null)
            {
                Rectangle rect = this.RightPanel.Children.OfType<Rectangle>().LastOrDefault();

                if (rect == null) return;
                rect.Fill = new SolidColorBrush(Colors.CadetBlue);
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
        public void refresh()
        {
            // set amount of discs
            discCount = int.Parse(this.textBox.Text);

            // reset all panels
            this.LeftPanel.Children.Clear();
            this.MidPanel.Children.Clear();
            this.RightPanel.Children.Clear();

            // calculate height
            _recHeight = (int)(this.LeftPanel.ActualHeight * 0.9) / discCount;

            // calculate width
            int maxWidth = (int)(this.LeftPanel.ActualWidth * 0.9);
            int minWidth = 5;
            int widthReduce = (maxWidth - minWidth) / discCount;
            int width;

            // create rectangles
            for (int i = discCount; i > 0; i--)
            {
                // rectangle width
                width = minWidth + (i * widthReduce);

                // draw rectangles
                Rectangle rect = createNewRect(width, _recHeight);
                // set position
                Canvas.SetTop(rect, (i - 1) * _recHeight + 20);
                Canvas.SetLeft(rect, (maxWidth - width) / 2 + 7);
                // draw on canvas
                LeftPanel.Children.Add(rect);
            }

            // set positions
            _leftPos = -1;
            _midPos = discCount - 1;
            _rightPos = discCount - 1;
        }

        private Rectangle createNewRect(int width, int height)
        {
            Rectangle rect = new Rectangle
            {
                Height = height,
                Width = width,
                Margin = new Thickness(0),
                Fill = new SolidColorBrush(Colors.YellowGreen)
            };

            return rect;
        }

        private void moveRectFromTo(Canvas canvasFrom, Canvas canvasTo)
        {
            // if click on same canvas -> return
            if (canvasFrom == canvasTo) return;
            // get canvas
            Rectangle rect = get(canvasFrom);
            // if no rectangle on canvas -> return
            if (rect == null) return;

            Rectangle rectOnTarget = canvasTo.Children.OfType<Rectangle>().LastOrDefault();

            Canvas to = rectOnTarget == null ? canvasTo : rectOnTarget.Width < rect.Width ? canvasFrom : canvasTo;

            // put rectangle to other canvas
            put(rect, to);
        }

        private void put(Rectangle rect, Canvas canvas)
        {
            int marginTop = 20;
            int factor;

            // positioning
            if (canvas == LeftPanel)
            {
                factor = _leftPos;
                _leftPos--;
            }
            else if (canvas == MidPanel)
            {
                factor = _midPos;
                _midPos--;
            }
            else
            {
                factor = _rightPos;
                _rightPos--;
            }

            Canvas.SetTop(rect, factor * _recHeight + marginTop);

            // set color to remove selection
            rect.Fill = new SolidColorBrush(Colors.YellowGreen);

            canvas.Children.Add(rect);
        }

        private Rectangle get(Canvas canvas)
        {
            // get last added rectangle on canvas
            Rectangle rect = canvas.Children.OfType<Rectangle>().LastOrDefault();
            // remove it from canvas
            canvas.Children.Remove(rect);

            // positioning
            if (canvas == LeftPanel) _leftPos++;
            else if (canvas == MidPanel) _midPos++;
            else _rightPos++;

            return rect;
        }

        private void solve(Canvas start, Canvas end, Canvas cache, int height)
        {
            if (height > 1) solve(start, cache, end, height - 1);
            this.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Background,
                new Action(() =>
                {
                    moveRectFromTo(start, end);
                })
                    );
            System.Threading.Thread.Sleep(400);
            if (height > 1) solve(cache, end, start, height - 1);
        }
    }
}

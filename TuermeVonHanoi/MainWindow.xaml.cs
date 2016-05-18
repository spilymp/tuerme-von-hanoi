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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            LeftPanel.Children.Clear();

            discCount = int.Parse(this.textBox.Text);

            Thickness margin;

            // calculate height
            int maxHeight = 217;
            int minHeight = 5;
            int height = maxHeight / discCount;

            // calculate width
            int maxWidth = 120;
            int minWidth = 5;
            int widthReduce = (maxWidth - minWidth) / discCount;
            int width;

            // create rectangles
            for (int i = 1; i <= discCount; i++)
            {
                if (i == 1)
                {
                    margin = new Thickness(0, 20, 0, 0);
                }
                else
                {
                    margin = new Thickness(0);
                }

                // rectangle width
                width = minWidth + (i * widthReduce);

                // create rectangle
                LeftPanel.Children.Add(createNewRect(width, margin, height));
            }
        }

        public String DiscCount
        {
            get { return this.discCount.ToString(); }
            set { this.discCount = int.Parse(value); }
        }

        public Rectangle createNewRect(int width, Thickness margin, int height)
        {
            return new Rectangle
            {
                Height = height,
                Width = width,
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Colors.Black),
                Margin = margin,
                VerticalAlignment = VerticalAlignment.Center
            };
        }
    }
}

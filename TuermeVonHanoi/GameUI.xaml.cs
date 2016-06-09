using logic.TuermeVonHanoi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class GameUI : Window
    {
        private Game game;
        private Canvas _tempCanvas = null;

        public int Dics { get; set; } = 3;

        /**
        * Konstruktur
        */
        public GameUI()
        {
            InitializeComponent();
            this.Dics = 3;
            this.game = new Game(this.LeftCanvas, this.MidCanvas, this.RightCanvas);
        }

        /**
        * UI Element Funktionen
        */
        private void button_Start_Click(object sender, RoutedEventArgs e)
        {
            this.game.Dics = Dics;
            this.game.start();

            this.LeftCanvas.IsEnabled = true;
            this.MidCanvas.IsEnabled = true;
            this.RightCanvas.IsEnabled = true;

            _toggleButton(ButtonPlay);
            _toggleButton(ButtonSolve);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);

            this.DiscsWrapper.Visibility = Visibility.Hidden;
        }

        private void button_Solve_Click(object sender, RoutedEventArgs e)
        {
            game.solve();

            _toggleButton(ButtonSolve);
            _toggleButton(ButtonCancel);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);        
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {

            _toggleButton(ButtonSolve);
            _toggleButton(ButtonCancel);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            game.exit();

            this.LeftCanvas.IsEnabled = false;
            this.MidCanvas.IsEnabled = false;
            this.RightCanvas.IsEnabled = false;

            _toggleButton(ButtonPlay);
            _toggleButton(ButtonSolve);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);

            this.DiscsWrapper.Visibility = Visibility.Visible;
        }

        private void button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            game.refresh();
        }
        private void Canvas_Click(Object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            if (_tempCanvas == null)
            {
                Rectangle rect = canvas.Children.OfType<Rectangle>().LastOrDefault();
                if (rect == null) return;
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.CadetBlue);
                _tempCanvas = canvas;
            }
            else
            {
                game.move(_tempCanvas, canvas);
                _tempCanvas = null;
            }
        }

        private void _toggleButton(Button button)
        {
            if (button.IsVisible)
            {
                button.Visibility = Visibility.Hidden;
                button.IsEnabled = false;
            }
            else
            {
                button.Visibility = Visibility.Visible;
                button.IsEnabled = true;
            }
        }


    }
}

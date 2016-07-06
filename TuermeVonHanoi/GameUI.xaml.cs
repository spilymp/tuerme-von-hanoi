using logic.TuermeVonHanoi;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Speech.Recognition;
using TuermeVonHanoi.logic;

namespace TuermeVonHanoi
{

    public partial class GameUI : Window
    {
        /* GameLogic */
        private Game game;

        /* discs, default 3 */
        public int Discs { get; set; } = 3;

        /// <summary>
        /// Game constructor
        /// </summary>
        public GameUI()
        {
            InitializeComponent();
            // init GameLogic
            this.game = new Game(this.LeftCanvas, this.MidCanvas, this.RightCanvas, this.Dispatcher);

            // EventHandle for success
            this.game.Success += this._toggleWinDialog;
            this.game.Exit += this.button_Exit_Click;
            this.game.Message += this.MessageEventFire;
        }

        /// <summary>
        /// Deconstructor
        /// </summary>
        ~GameUI()
        {

        }

        /*
        * UI Element Funktionen
        */
        private void button_Start_Click(object sender, RoutedEventArgs e)
        {
            // set Dics (binding)
            game.Discs = Discs;
            game.start();

            Messages.Text = "Play!";

            // enable clickable canvas
            LeftCanvas.IsEnabled = true;
            MidCanvas.IsEnabled = true;
            RightCanvas.IsEnabled = true;

            // toggle buttons
            _toggleButton(ButtonPlay);
            _toggleButton(ButtonSolve);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);

            // hidden dics settings
            this.DiscsWrapper.Visibility = Visibility.Hidden;
        }

        private void button_Solve_Click(object sender, RoutedEventArgs e)
        {
            // toggle buttons, only cancel button show
            _toggleButton(ButtonSolve);
            _toggleButton(ButtonCancel);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);

            game.solve();
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            // toggle buttons
            _toggleButton(ButtonSolve);
            _toggleButton(ButtonCancel);
            _toggleButton(ButtonRefresh);
            _toggleButton(ButtonExit);

            game.solveStop();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            game.exit();

            Messages.Text = "";

            // canvas not clickable
            LeftCanvas.IsEnabled = false;
            MidCanvas.IsEnabled = false;
            RightCanvas.IsEnabled = false;

            // toggle buttons, only play button show
            _showButton(ButtonPlay);
            _hideButton(ButtonSolve);
            _hideButton(ButtonRefresh);
            _hideButton(ButtonExit);
            _hideButton(ButtonCancel);

            // show dics settings, hidden winDialog(if open)
            DiscsWrapper.Visibility = Visibility.Visible;
            WinDialog.Visibility = Visibility.Hidden;
        }

        private void button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            game.refresh();
        }

        /// <summary>
        /// handle click on canvas element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_Click(Object sender, MouseButtonEventArgs e)
        {
            // object to canvas
            Canvas canvas = (Canvas)sender;

            game.clickHandle(canvas);
        }

        /// <summary>
        /// helper function
        /// </summary>
        /// <param name="button"></param>
        private void _toggleButton(Button button)
        {
            // hide
            if (button.IsVisible)
            {
                _hideButton(button);
            }
            // show
            else
            {
                _showButton(button);
            }
        }

        private void _showButton(Button button)
        {
            button.Visibility = Visibility.Visible;
            button.IsEnabled = true;
        }

        private void _hideButton(Button button)
        {
            button.Visibility = Visibility.Hidden;
            button.IsEnabled = false;
        }

        /// <summary>
        /// handle win event from Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void _toggleWinDialog(object sender, EventArgs args)
        {
            WinDialog.Visibility = Visibility.Visible;
        }

        private void GameGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            game.startGestureRecognition();
        }

        private void GameGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            game.stopGestureRecognition();
        }

        public void setMessage(String text)
        {
            Messages.Text = text;
        }

        private void MessageEventFire(object sender, Game.MessageEventArgs args)
        {
            Dispatcher.BeginInvoke(
           (Action)(() =>
           {
               Messages.Text = args.MessageEventString;
           })
           );
        }

    }
}

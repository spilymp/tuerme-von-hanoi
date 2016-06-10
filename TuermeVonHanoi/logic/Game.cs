using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace logic.TuermeVonHanoi
{
    class Game
    {
        /* canvas elements */
        private Canvas _canvasLeft;
        private Canvas _canvasMiddle;
        private Canvas _canvasRight;

        /* dics, default 3 */
        public int Dics { get; set; } = 3;

        /* help var */
        private double _canvasHeight;
        private double _canvasWidth;
        private double _canvasMarginTop;
        private double _canvasMarginLeft;

        private double _canvasRelativeMargin = 0.05;

        /* event success */
        public event EventHandler Success;

        public Game(Canvas canvasLeft, Canvas canvasMiddle, Canvas canvasRight)
        {
            this._canvasLeft = canvasLeft;
            this._canvasMiddle = canvasMiddle;
            this._canvasRight = canvasRight;
        }

        /// <summary>
        /// game start
        /// </summary>
        public void start()
        {
            _canvasHeight = _canvasLeft.ActualHeight;
            _canvasWidth = _canvasLeft.ActualWidth;

            _canvasMarginTop = _canvasHeight * _canvasRelativeMargin;
            _canvasMarginLeft = _canvasWidth * _canvasRelativeMargin;

            _clearCanvas();
            _buildCanvas();
        }

        /// <summary>
        /// game exit
        /// </summary>
        public void exit()
        {
            _clearCanvas();
        }

        /// <summary>
        /// game refresh
        /// </summary>
        public void refresh()
        {
            start();
        }

        /// <summary>
        /// auto solve
        /// </summary>
        public void solve()
        {
             _solve(_canvasLeft, _canvasMiddle, _canvasRight, Dics);
        }

        /// <summary>
        /// move element
        /// </summary>
        /// <param name="fromCanvas">from canvas</param>
        /// <param name="toCanvas">to canvas</param>
        public void move(Canvas fromCanvas, Canvas toCanvas)
        {
            // get element
            Rectangle rect = _getElement(fromCanvas);
            // color active element to inactive
            rect.Fill = new SolidColorBrush(Colors.YellowGreen);

            if (rect == null || fromCanvas == toCanvas) return;

            // get element from target canvas
            Rectangle rectOnTarget = _getElement(toCanvas);
            Canvas to = (rectOnTarget == null) ? toCanvas : rectOnTarget.Width < rect.Width ? fromCanvas : toCanvas;

            // remove old element
            fromCanvas.Children.Remove(rect);
            // put element to new canvas
            _putElement(rect, to);

            // update layout for solve
            fromCanvas.UpdateLayout();
            toCanvas.UpdateLayout();

            // check if sucess
            isSuccess();
        }

        /// <summary>
        /// get last element from canvas
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        private Rectangle _getElement(Canvas canvas)
        {
            Rectangle rect = canvas.Children.OfType<Rectangle>().LastOrDefault();

            return rect;
        }

        /// <summary>
        /// put element to canvas
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="canvas"></param>
        private void _putElement(Rectangle rect, Canvas canvas)
        {
            // count elements from canvas
            int countElements = (int)canvas.Children.OfType<Rectangle>().LongCount() + 1;
            // set position for element
            Canvas.SetTop(rect, (Dics - countElements) * rect.ActualHeight + _canvasMarginTop);

            rect.Fill = new SolidColorBrush(Colors.YellowGreen);

            // add element to canvas
            canvas.Children.Add(rect);
        }

        /// <summary>
        /// private auto solve function
        /// </summary>
        /// <param name="start"></param>
        /// <param name="cache"></param>
        /// <param name="end"></param>
        /// <param name="dics"></param>
        private void _solve(Canvas start, Canvas cache, Canvas end, int dics)
        {
            if (dics > 0)
            {
                _solve(start, end, cache, dics - 1);

                move(start, end);
                
                _solve(cache, start, end, dics - 1);
            }
        }

        /// <summary>
        /// build elements
        /// </summary>
        private void _buildCanvas()
        {

            // values for calculation
            int dicsWidthMax = (int)Math.Round(_canvasWidth - _canvasMarginLeft * 2);
            int dicsWidthMin = 5;
            int dicsHeight = (int)Math.Round(_canvasHeight - _canvasMarginTop * 2) / Dics;

            int dicsWidthReduce = (dicsWidthMax - dicsWidthMin) / Dics;

            // create elements
            for (int i = Dics; i > 0; i--)
            {
                // get width
                int dicsWidth = dicsWidthMin + (i * dicsWidthReduce);

                // create rectangle
                Rectangle rectangle = new Rectangle
                {
                    Width = dicsWidth,
                    Height = dicsHeight,
                    Margin = new Thickness(0),
                    Fill = new SolidColorBrush(Colors.YellowGreen)
                };

                // set position
                Canvas.SetTop(rectangle, (i - 1) * dicsHeight + _canvasMarginTop);
                Canvas.SetLeft(rectangle, (dicsWidthMax - dicsWidth) / 2 + _canvasMarginLeft);

                // draw rectangle
                _canvasLeft.Children.Add(rectangle);
            }

        }

        /// <summary>
        /// clear elements
        /// </summary>
        private void _clearCanvas()
        {
            _canvasLeft.Children.Clear();
            _canvasMiddle.Children.Clear();
            _canvasRight.Children.Clear();
        }

        /// <summary>
        /// is success, then fire event
        /// </summary>
        public void isSuccess()
        {
            if(_canvasRight.Children.OfType<Rectangle>().LongCount() == Dics)
            {
                EventHandler handler = Success;
                if (handler != null) handler(this, EventArgs.Empty);
            }
        }
    }
}

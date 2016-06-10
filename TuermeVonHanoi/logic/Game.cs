using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace logic.TuermeVonHanoi
{
    class Game
    {
        /* canvas elements */
        private Canvas _canvasLeft;
        private Canvas _canvasMiddle;
        private Canvas _canvasRight;

        /* Discs, default 3 */
        public int Discs { get; set; } = 3;

        /* help var */
        private double _canvasHeight;
        private double _canvasWidth;
        private double _canvasMarginTop;
        private double _canvasMarginLeft;

        private double _canvasRelativeMargin = 0.05;

        /* event success */
        public event EventHandler Success;

        /* for async solve */
        private Task task;
        private Dispatcher Dispatcher;

        /* for canceling async */
        private CancellationTokenSource cts;
        private CancellationToken ct;


        public Game(Canvas canvasLeft, Canvas canvasMiddle, Canvas canvasRight, Dispatcher dispatcher)
        {
            this._canvasLeft = canvasLeft;
            this._canvasMiddle = canvasMiddle;
            this._canvasRight = canvasRight;

            this.Dispatcher = dispatcher;

            this.cts = new CancellationTokenSource();
            this.ct = cts.Token;
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
            task = new Task(() =>
            {
                _solve(_canvasLeft, _canvasMiddle, _canvasRight, Discs);
            }, ct);

            // TODO
            if (task.IsCanceled) task.Dispose();
            task.Start();
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
            Canvas.SetTop(rect, (Discs - countElements) * rect.ActualHeight + _canvasMarginTop);

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
        /// <param name="Discs"></param>
        private void _solve(Canvas start, Canvas cache, Canvas end, int Discs)
        {
            if (ct.IsCancellationRequested) return;

            if (Discs > 0)
            {
                _solve(start, end, cache, Discs - 1);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    move(start, end);
                })
                );

                Thread.Sleep(400);

                _solve(cache, start, end, Discs - 1);
            }
        }

        /// <summary>
        /// build elements
        /// </summary>
        private void _buildCanvas()
        {

            // values for calculation
            int DiscsWidthMax = (int)Math.Round(_canvasWidth - _canvasMarginLeft * 2);
            int DiscsWidthMin = 5;
            int DiscsHeight = (int)Math.Round(_canvasHeight - _canvasMarginTop * 2) / Discs;

            int DiscsWidthReduce = (DiscsWidthMax - DiscsWidthMin) / Discs;

            // create elements
            for (int i = Discs; i > 0; i--)
            {
                // get width
                int DiscsWidth = DiscsWidthMin + (i * DiscsWidthReduce);

                // create rectangle
                Rectangle rectangle = new Rectangle
                {
                    Width = DiscsWidth,
                    Height = DiscsHeight,
                    Margin = new Thickness(0),
                    Fill = new SolidColorBrush(Colors.YellowGreen)
                };

                // set position
                Canvas.SetTop(rectangle, (i - 1) * DiscsHeight + _canvasMarginTop);
                Canvas.SetLeft(rectangle, (DiscsWidthMax - DiscsWidth) / 2 + _canvasMarginLeft);

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
            if (_canvasRight.Children.OfType<Rectangle>().LongCount() == Discs)
            {
                EventHandler handler = Success;
                if (handler != null) handler(this, EventArgs.Empty);
            }
        }

        public void solveStop()
        {
            cts.Cancel();
        }
    }
}

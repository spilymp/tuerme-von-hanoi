using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace logic.TuermeVonHanoi
{
    class Game
    {
        private Canvas _canvasLeft;
        private Canvas _canvasMiddle;
        private Canvas _canvasRight;

        private int _dics = 3;

        private double _canvasHeight;
        private double _canvasWidth;
        private double _canvasMarginTop;
        private double _canvasMarginLeft;

        private double _canvasRelativeMargin = 0.05;

        public Game(Canvas canvasLeft, Canvas canvasMiddle, Canvas canvasRight)
        {
            this._canvasLeft = canvasLeft;
            this._canvasMiddle = canvasMiddle;
            this._canvasRight = canvasRight;
        }

        public void start()
        {
            _canvasHeight = _canvasLeft.ActualHeight;
            _canvasWidth = _canvasLeft.ActualWidth;

            _canvasMarginTop = _canvasHeight * _canvasRelativeMargin;
            _canvasMarginLeft = _canvasWidth * _canvasRelativeMargin;

            _clearCanvas();
            _buildCanvas();
        }

        public void exit()
        {
            _clearCanvas();
        }

        public void refresh()
        {
            start();
        }

        public void solve()
        {
            refresh();
            _solve(_canvasLeft, _canvasRight, _canvasMiddle, _dics);
        }

        private void _solve(Canvas start, Canvas end, Canvas cache, int height)
        {
            if (height > 1) _solve(start, cache, end, height - 1);
            move(start, end);
            if (height > 1) _solve(cache, end, start, height - 1);
        }

        public void move(Canvas fromCanvas, Canvas toCanvas)
        {
            Rectangle rect = _getElement(fromCanvas);
            rect.Fill = new SolidColorBrush(Colors.YellowGreen);

            if (rect == null || fromCanvas == toCanvas) return;

            Rectangle rectOnTarget = toCanvas.Children.OfType<Rectangle>().LastOrDefault();
            Canvas to = (rectOnTarget == null) ? toCanvas : rectOnTarget.Width < rect.Width ? fromCanvas : toCanvas;

            fromCanvas.Children.Remove(rect);
            _putElement(rect, to);
        }

        private Rectangle _getElement(Canvas canvas)
        {
            Rectangle rect = canvas.Children.OfType<Rectangle>().LastOrDefault();
            
            return rect;
        }

        private void _putElement(Rectangle rect, Canvas canvas)
        {
            int countElements = (int)canvas.Children.OfType<Rectangle>().LongCount() + 1;
            Canvas.SetTop(rect, (_dics - countElements) * rect.ActualHeight + _canvasMarginTop);
            
            rect.Fill = new SolidColorBrush(Colors.YellowGreen);

            canvas.Children.Add(rect);
        }

        private void _buildCanvas()
        {

            // values for calculation
            int dicsWidthMax = (int)Math.Round(_canvasWidth - _canvasMarginLeft * 2);
            int dicsWidthMin = 5;
            int dicsHeight = (int)Math.Round(_canvasHeight - _canvasMarginTop * 2) / _dics;
            
            int dicsWidthReduce = (dicsWidthMax - dicsWidthMin) / _dics;

            // create elements
            for (int i = _dics; i > 0; i--)
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

        private void _clearCanvas()
        {
            _canvasLeft.Children.Clear();
            _canvasMiddle.Children.Clear();
            _canvasRight.Children.Clear();
        }

        public int Dics
        {
            get { return _dics; }
            set
            {
                if (value <= 2)
                    value = 3;

                _dics = value;
            }
        }
    }
}

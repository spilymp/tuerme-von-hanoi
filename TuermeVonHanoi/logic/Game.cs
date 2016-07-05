﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;
using TuermeVonHanoi.logic;
using TuermeVonHanoi;

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
        private Task solveTask;
        private Dispatcher Dispatcher;

        /* for canceling async */
        private CancellationTokenSource cts;
        private CancellationToken ct;

        /* Thread for gestures */
        Thread gestureThread = null;
        bool stop = false;
        int count = 0;
        List<double> gesture;
        Worker workerObject;

        public Game(Canvas canvasLeft, Canvas canvasMiddle, Canvas canvasRight, Dispatcher dispatcher)
        {
            this._canvasLeft = canvasLeft;
            this._canvasMiddle = canvasMiddle;
            this._canvasRight = canvasRight;

            this.Dispatcher = dispatcher;
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

            cts = new CancellationTokenSource();
            ct = cts.Token;

            solveTask = new Task(() =>
            {
                _solve(_canvasLeft, _canvasMiddle, _canvasRight, Discs);
            }, ct);

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
            refresh();

            if (solveTask.IsCanceled) solveTask.Dispose();
            solveTask.Start();
        }

        /// <summary>
        /// move element
        /// </summary>
        /// <param name="fromCanvas">from canvas</param>
        /// <param name="toCanvas">to canvas</param>
        public void move(Canvas fromCanvas, Canvas toCanvas)
        {
            // update layout for solve
            _canvasLeft.UpdateLayout();
            _canvasMiddle.UpdateLayout();
            _canvasRight.UpdateLayout();

            // get element
            Rectangle rect = _getElement(fromCanvas);

            if (rect == null) return;

            // color active element to inactive
            rect.Fill = new SolidColorBrush(Colors.YellowGreen);

            if (fromCanvas == toCanvas) return;

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
                Thread.Sleep(400);
                EventHandler handler = Success;
                if (handler != null) handler(this, EventArgs.Empty);
            }
        }

        public void solveStop()
        {
            if (cts != null) cts.Cancel();
        }

        public void startGestureRecognition()
        {
            workerObject = new Worker();
            gestureThread = new Thread(workerObject.DoWork);
            gestureThread.Start();
        }

        public string stopGestureRecognition()
        {
            try
            {
                workerObject.RequestStop();
            }
            catch (System.NullReferenceException e)
            {
                Thread.Sleep(500);
                if (workerObject != null)
                {
                    workerObject.RequestStop();
                } else
                {
                    startGestureRecognition();
                    stopGestureRecognition();
                }
            }
            gestureThread.Join();
            List<double> gesture = workerObject.getGesture();
            Gesture.writeGestureToConsole(gesture);
            Gesture spotter = new Gesture();
            Gestures gestures = spotter.identifyGesture(gesture.ToArray());
            Console.WriteLine(gestures);

            string message = "";

            switch (gestures) {
                case Gestures.ONE2TWO:
                    {
                        message = "Move from 1 to 2";
                        move(_canvasLeft, _canvasMiddle);
                        break;
                    }
                case Gestures.ONE2THREE:
                    {
                        message = "Move from 1 to 3";
                        move(_canvasLeft, _canvasRight);
                        break;
                    }
                case Gestures.TWO2ONE:
                    {
                        message = "Move from 2 to 1";
                        move(_canvasMiddle, _canvasLeft);
                        break;
                    }
                case Gestures.TWO2THREE:
                    {
                        message = "Move from 2 to 3";
                        move(_canvasMiddle, _canvasRight);
                        break;
                    }
                case Gestures.THREE2ONE:
                    {
                        message = "Move from 3 to 1";
                        move(_canvasRight, _canvasLeft);
                        break;
                    }
                case Gestures.THREE2TWO:
                    {
                        message = "Move from 3 to 2";
                        move(_canvasRight, _canvasMiddle);
                        break;
                    }
                case Gestures.SOLVE:
                    {
                        message = "Reset and solve current game.";
                        solve();
                        break;
                    }
                case Gestures.REFRESH:
                    {
                        message = "Refresh current game.";
                        refresh();
                        break;
                    }
                case Gestures.ONE:
                    {
                        message = "ONE";
                        break;
                    }
                case Gestures.TWO:
                    {
                        message = "TWO";
                        break;
                    }
                case Gestures.THREE:
                    {
                        message = "THREE";
                        break;
                    }
                case Gestures.CLOSE_1:
                    {
                        message = "CLOSE Gesture 1";
                        break;
                    }
                case Gestures.CLOSE_2:
                    {
                        message = "CLOSE Gesture 2";
                        break;
                    }
                default:
                    {
                        message = "No Gesture identified.";
                        break;
                    }
            }

            return message;
        }
    }
}

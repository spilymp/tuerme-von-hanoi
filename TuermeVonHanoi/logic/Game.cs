using System;
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
using System.Speech.Recognition;

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
        public event EventHandler Exit;

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

        /* SpeakMode */
        private GameSpeakMode speakMode;

        /* Slot */
        InputTypes inputType = InputTypes.NO;
        KeyWords slot1 = KeyWords.NO;
        KeyWords slot2 = KeyWords.NO;
        KeyWords slot3 = KeyWords.NO;

        public Game(Canvas canvasLeft, Canvas canvasMiddle, Canvas canvasRight, Dispatcher dispatcher)
        {
            this._canvasLeft = canvasLeft;
            this._canvasMiddle = canvasMiddle;
            this._canvasRight = canvasRight;

            this.Dispatcher = dispatcher;

            this.speakMode = new GameSpeakMode();
            this.speakMode.ValueChanged += this.speakHandle;
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

            speakMode.start();

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
            speakMode.stop();

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

        public void isExit()
        {
            EventHandler handler = Exit;
            if (handler != null) handler(this, EventArgs.Empty);
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
                }
                else
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

            if (inputType == InputTypes.NO) inputType = InputTypes.GESTURE;
            if (inputType == InputTypes.GESTURE)
            {


                switch (gestures)
                {
                    /*
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
                        */
                    case Gestures.ONE:
                        {
                            if (slot2 == KeyWords.NO) setSlot2(KeyWords.CANVAS_LEFT);
                            else if (slot3 == KeyWords.NO) setSlot3(KeyWords.CANVAS_LEFT);
                            message = "ONE";
                            break;
                        }
                    case Gestures.TWO:
                        {
                            if (slot2 == KeyWords.NO) setSlot2(KeyWords.CANVAS_MIDDLE);
                            else if (slot3 == KeyWords.NO) setSlot3(KeyWords.CANVAS_MIDDLE);
                            message = "TWO";
                            break;
                        }
                    case Gestures.THREE:
                        {
                            if (slot2 == KeyWords.NO) setSlot2(KeyWords.CANVAS_RIGHT);
                            else if (slot3 == KeyWords.NO) setSlot3(KeyWords.CANVAS_RIGHT);
                            message = "THREE";
                            break;
                        }
                    case Gestures.CLOSE_1:
                        {
                            if (slot1 == KeyWords.NO) setSlot1(KeyWords.CLOSE);
                            message = "CLOSE Gesture 1";
                            break;
                        }
                    case Gestures.CLOSE_2:
                        {
                            if (slot1 == KeyWords.CLOSE) setSlot2(KeyWords.CLOSE);
                            message = "CLOSE Gesture 2";
                            break;
                        }
                    default:
                        {
                            message = "No Gesture identified.";
                            break;
                        }
                }
            }

            return message;
        }

        public void clickHandle(Canvas element)
        {
            KeyWords key;

            if (inputType == InputTypes.NO) inputType = InputTypes.CLICK;
            if (inputType == InputTypes.CLICK)
            {
                if (element == _canvasLeft) key = KeyWords.CANVAS_LEFT;
                else if (element == _canvasMiddle) key = KeyWords.CANVAS_MIDDLE;
                else key = KeyWords.CANVAS_RIGHT;

                if (slot2 == KeyWords.NO) setSlot2(key);
                else if (slot3 == KeyWords.NO) setSlot3(key);
            }        
        }

        /// <summary>
        /// speak handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void speakHandle(object sender, EventArgs e)
        {
            SpeechRecognizedEventArgs ev = (SpeechRecognizedEventArgs)e;
            String[] resultArray = ev.Result.Semantics.Value.ToString().Split(';');
            int result = Int32.Parse(resultArray[0]);

            if (inputType == InputTypes.NO) inputType = InputTypes.SPEAK;
            if (inputType == InputTypes.SPEAK)
            {
                //if slot1
                if (slot1 == KeyWords.NO)
                {
                    KeyWords key = (result == 1) ? KeyWords.PUT : KeyWords.CLOSE;
                    setSlot1(key);
                    speakMode.loadSlot2();
                }

                //if slot2
                else if (slot1 != KeyWords.NO && slot2 == KeyWords.NO)
                {
                    setSlot2(_getKeyWordFromNumber(result));
                    speakMode.loadSlot3();
                }

                //if slot3
                else if (slot1 != KeyWords.NO && slot2 != KeyWords.NO && slot3 == KeyWords.NO)
                {
                    setSlot3(_getKeyWordFromNumber(result));
                }
            }
        }


        enum KeyWords
        {
            NO,
            PUT,
            CLOSE,
            CANVAS_LEFT,
            CANVAS_MIDDLE,
            CANVAS_RIGHT
        }

        enum InputTypes
        {
            NO,
            CLICK,
            SPEAK,
            GESTURE
        }

        private void _resetSlots()
        {
            slot1 = KeyWords.NO;
            slot2 = KeyWords.NO;
            slot3 = KeyWords.NO;
            inputType = InputTypes.NO;

            speakMode.loadSlot1();
        }

        private bool setSlot1(KeyWords keyWord)
        {
            Console.WriteLine("Setze Slot 1 mit " + keyWord);
            slot1 = keyWord;

            return false;
        }

        private bool setSlot2(KeyWords keyWord)
        {
            Console.WriteLine("Setze Slot 2 mit " + keyWord);

            if (slot1 == KeyWords.CLOSE && keyWord == KeyWords.CLOSE)
            {
                _resetSlots();
                isExit();

                return true;
            }
            else
            {
                slot2 = keyWord;
                Canvas temp = _getCanvasFromKeyWord(keyWord);
                if (temp != null) temp.Children.OfType<Rectangle>().LastOrDefault().Fill = new SolidColorBrush(System.Windows.Media.Colors.CadetBlue);
                return true;
            }
        }

        private bool setSlot3(KeyWords keyWord)
        {
            Console.WriteLine("Setze Slot 3 mit " + keyWord);

            if (slot2 != KeyWords.NO)
            {
                Canvas fromCanvas, toCanvas;

                slot3 = keyWord;

                fromCanvas = _getCanvasFromKeyWord(slot2);
                toCanvas = _getCanvasFromKeyWord(slot3);

                if (fromCanvas != null && toCanvas != null)
                {
                    move(fromCanvas, toCanvas);
                    _resetSlots();
                    return true;
                }
            }

            return false;
        }

        private Canvas _getCanvasFromKeyWord(KeyWords keyWord)
        {
            switch (keyWord)
            {
                case KeyWords.CANVAS_LEFT:
                    {
                        return _canvasLeft;
                    }
                case KeyWords.CANVAS_MIDDLE:
                    {
                        return _canvasMiddle;
                    }
                case KeyWords.CANVAS_RIGHT:
                    {
                        return _canvasRight;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        private KeyWords _getKeyWordFromNumber(int num)
        {
            KeyWords el = KeyWords.NO;

            if (num == 1) el = KeyWords.CANVAS_LEFT;
            else if (num == 2) el = KeyWords.CANVAS_MIDDLE;
            else if (num == 3) el = KeyWords.CANVAS_RIGHT;
            else if (num == 4 && slot1 == KeyWords.PUT && inputType == InputTypes.SPEAK)
            {
                if (_canvasLeft.IsMouseOver) el = KeyWords.CANVAS_LEFT;
                else if (_canvasMiddle.IsMouseOver) el = KeyWords.CANVAS_MIDDLE;
                else if (_canvasRight.IsMouseOver) el = KeyWords.CANVAS_RIGHT;
            }
            else if (num == 4 && slot1 == KeyWords.CLOSE)
            {
                el = KeyWords.CLOSE;
            }

            return el;
        }

    }
}

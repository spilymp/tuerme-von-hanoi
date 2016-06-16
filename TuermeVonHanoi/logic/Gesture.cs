using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TuermeVonHanoi.logic
{
    class Gesture
    {
        double[] gestureOne2Two_1 = new double[] { 0, 0, 179.288287793963, 129.40066066348, 49.9937439858234, 50.2142088078166, 91.3203725369468, 137.834111016307, 0, 0, 0, 125.217592968192, 113.6464424603, 134.776799806461, 157.083097381553, 175.298405075955, 159.573975236909, 118.64342142244, 114.416053258632, 171.219666869036, 100.222168633636, 36.9936981895637, 36.8877820837752, 164.345060167927, 146.346458724188, 149.109598711741, 161.43393978233 };
        double[] gestureOne2Two_2 = new double[] { 0, 0, 158.990987246047, 175.160412423703, 125.154177632147, 48.4666170644407, 65.9598087106058, 0, 0, 0, 0, 153.828359353462, 163.300755766006, 167.799531272619, 169.992020198559, 170.40686573727, 170.472716618548, 144.462322208026, 153.179825172567, 170.125661715338, 64.7655406320243, 68.6402445605992, 59.7519064769408, 124.213654830948, 143.781210837917, 109.185096197026, 154.464664048401, 149.082141278814, 156.336859291806 };
        double[] gestureOne2Two_3 = new double[] { 0, 0, 172.78420615191, 165.565990660831, 153.587941194988, 94.5247684728338, 40.1515539085475, 102.474630165347, 0, 72.5064464000992, 81.8333569750884, 133.696689047479, 159.583676688321, 0, 0, 81.1646267865505, 160.144785630679, 110.033341833319, 63.0324561145977, 50.6381646666665, 26.7892996583017, 150.186418760546, 154.535253977691, 167.487032981491, 161.847294113349 };

        double[] gestureOne2Three_1 = new double[] { 0, 0, 175.6218, 169.3707, 35.3316, 48.9386, 103.3245, 163.1625, 160.1892, 168.2067, 0, 0, 0, 155.5838, 149.9281, 103.3136, 49.5268, 28.596, 158.6401, 173.7126, 67.7447, 47.2206, 33.8515 };
        double[] gestureOne2Three_2 = new double[] { 0, 0, 169.4366, 176.4513, 19.3335, 53.9084, 0, 153.4349, 157.8337, 147.2193, 0, 0, 173.3089, 156.8014, 41.5936, 46.122, 151.8214, 174.6878, 105.1073, 62.0798, 42.8253 };
        double[] gestureOne2Three_3 = new double[] { 0, 0, 178.5354, 173.7822, 171.3414, 170.2295, 24.1714, 25.8099, 50.8696, 59.3227, 0, 0, 144.6209, 159.9081, 168.7812, 0, 169.0459, 166.6799, 109.9686, 53.8508, 44.8605, 129.2669, 155.6245, 75.4693, 62.6113, 71.9506, 85.4329, 95.7302 };

        double[] gestureTwo2Three_1 = new double[] { 0, 0, 172.874983651098, 147.528807709152, 153.766134611337, 63.434948822922, 57.5288077091515, 99.2461127455633, 0, 124.460816271372, 126.689722832476, 143.21160411926, 155.256462174523, 0, 0, 0, 0, 164.91671278459, 157.533250079004, 163.272036079432, 0, 106.225275361179, 0, 0, 163.495638618245, 178.289943021697, 68.8801438465753, 73.0509571318694, 24.9118119278561, 25.0037129447394, 6.66130163977128, 25.2608191641013, 166.235291978371, 166.636455957138, 95.9391328033818, 59.244999246221, 42.3251342973634, 31.6227587442272 };
        double[] gestureTwo2Three_2 = new double[] { 0, 0, 167.115380616899, 85.4338180139538, 54.7066524093397, 53.8792730852639, 53.7461622625552, 121.192097418862, 158.198590513648, 25.2472480756282, 21.5409759185381, 0, 157.750976342788, 161.816346844287, 164.919208688557, 0, 0, 144.806092759897, 159.372364922404, 158.635111322103, 63.7263086052155, 32.1438446531633, 130.142445784857, 173.36602607007, 98.4381702685243, 43.0362474434229, 39.8949095534076, 0 };
        double[] gestureTwo2Three_3 = new double[] { 0, 0, 156.974507991472, 98.6201621508697, 49.828236679721, 85.8106570975194, 139.666858371439, 106.757081182636, 114.665273436928, 145.967872182661, 0, 0, 0, 0, 0, 172.874983651098, 153.86156990269, 161.073445788292, 157.18401334527, 0, 0, 173.566017658499, 165.103722823291, 55.0546424282535, 50.8942490883054, 155.021532286551, 172.870852588017, 68.2932625825724, 59.7600583460591, 0, 47.2835910815672, 48.764398020469 };

        double[] gestureTwo2One_1 = new double[] { 0, 0, 169.286876977209, 55.9787466856048, 83.0240684708612, 135, 169.808498149972, 87.0734617224289, 95.6405494321568, 41.9872124958163, 56.6644012781373, 0, 0, 0, 142.476361106426, 139.304468960508, 150.336911337736, 0, 0, 0, 151.440379521681, 151.59190869168, 113.9907252638, 120.012638207753, 138.802585807071, 104.233361197725, 127.270249837809, 138.245254903888 };
        double[] gestureTwo2One_2 = new double[] { 0, 0, 155.349415277375, 62.2519440101067, 62.73079033924, 97.2271932883133, 0, 95.4875570600912, 114.193208987289, 141.259036305122, 147.901072745215, 0, 0, 148.134022306396, 150.101098161385, 158.481390682608, 165.042837696877, 0, 0, 123.538815662396, 98.6935607322571, 154.763736353096, 88.4182758798186, 104.309925549374, 128.233825177446 };
        double[] gestureTwo2One_3 = new double[] { 0, 0, 170.753887254437, 67.2855876468328, 58.9916899936149, 116.941766355053, 93.7722836093798, 94.3987053549955, 130.557938028601, 0, 0, 0, 122.776458115532, 125.41856799372, 95.4308112967108, 84.3348842054755, 98.3821729455959, 141.598173455059, 146.375115357744, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        double[] gestureThree2One_1 = new double[] { 0, 0, 163.0529, 90.4717, 28.8695, 52.4224, 137.2434, 145.3602, 80.7697, 93.0751, 17.0125, 17.3789, 104.2152, 134.3154, 0, 0, 114.1477, 88.6636, 72.1972, 113.0064, 103.1364, 125.4716 };
        double[] gestureThree2One_2 = new double[] { 0, 0, 152.9873, 156.6912, 101.7882, 71.0954, 53.6955, 139.0924, 149.5717, 168.7478, 96.0725, 94.6896, 94.11, 79.6111, 99.9262, 102.6804, 108.832, 120.5476, 0, 0, 109.5292, 113.6938, 110.6006, 116.0766, 124.8885 };
        double[] gestureThree2One_3 = new double[] { 0, 0, 165.0727, 80.8628, 57.6277, 19.406, 136.8075, 130.3505, 96.6168, 63.2456, 0, 117.7735, 129.4489, 0, 0, 120.6314, 115.7123, 124.1145, 79.9911, 94.8473 };

        double[] gestureThree2Two_1 = new double[] { 0, 0, 164.3276, 74.2545, 51.5387, 35.4484, 154.1032, 73.5676, 84.4217, 95.0058, 90.1242, 104.1361, 109.5047, 145.008, 33.6239, 104.2563, 159.0616, 99.9126, 68.4319, 137.9425, 144.3775, 149.7833, 29.6638 };
        double[] gestureThree2Two_2 = new double[] { 0, 0, 160.8012, 125.8185, 96.2167, 90.5457, 139.7446, 142.9995, 76.2434, 99.4554, 0, 0, 113.0583, 108.8564, 139.6859, 0, 140.3616, 138.0735, 126.6304, 114.5609, 61.8584, 146.7829, 144.481 };
        double[] gestureThree2Two_3 = new double[] { 0, 0, 156.7323, 136.647, 119.5388, 45.2271, 153.2394, 153.7041, 68.3677, 67.2662, 61.6658, 117.4567, 122.7964, 138.9452, 0, 0, 126.9691, 157.0849, 82.8923, 55.3544, 64.9625, 145.7657, 147.9576 };

        double[] gestureSolve_1 = new double[] { 0, 0, 175.3286, 169.679, 144.4736, 108.6094, 118.3274, 127.7197, 134.6661, 130.562, 96.0807, 55.0462, 95.0131 };
        double[] gestureSolve_2 = new double[] { 0, 0, 173.1121, 166.2154, 159.168, 101.2906, 131.8084, 137.0306, 148.7033, 62.4915, 67.9628, 84.2969 };
        double[] gestureSolve_3 = new double[] { 0, 0, 171.5231, 139.8868, 97.6924, 129.2665, 143.005, 75.2297, 60.0474, 81.653 };

        double[] gestureRefresh_1 = new double[] { 0, 0, 143.6271, 112.651, 77.6833, 68.5249, 68.3378, 8.7355, 17.4027 };
        double[] gestureRefresh_2 = new double[] { 0, 0, 150.8411, 150.8569, 102.0694, 57.4466, 57.8883, 23.8655, 0.7045, 173.2902 };
        double[] gestureRefresh_3 = new double[] { 0, 0, 142.9055, 140.0063, 86.189, 62.0542, 117.1916, 15.3933, 0.1061, 0 };

        public Gestures identifyGesture(double[] inputGesture)
        {
            if(inputGesture.Length < 5)
            {
                Console.WriteLine("Not enough input to identify gesture.");
                return Gestures.NONE;
            }

            double dtw_One2Two = Math.Min(DTW(gestureOne2Two_1, inputGesture), Math.Min(DTW(gestureOne2Two_2, inputGesture), DTW(gestureOne2Two_3, inputGesture)));
            double dtw_One2Three = Math.Min(DTW(gestureOne2Three_1, inputGesture), Math.Min(DTW(gestureOne2Three_2, inputGesture), DTW(gestureOne2Three_3, inputGesture)));

            double dtw_Two2Three = Math.Min(DTW(gestureTwo2Three_1, inputGesture), Math.Min(DTW(gestureTwo2Three_2, inputGesture), DTW(gestureTwo2Three_3, inputGesture)));
            double dtw_Two2One = Math.Min(DTW(gestureTwo2One_1, inputGesture), Math.Min(DTW(gestureTwo2One_2, inputGesture), DTW(gestureTwo2One_3, inputGesture)));

            double dtw_Three2One = Math.Min(DTW(gestureThree2One_1, inputGesture), Math.Min(DTW(gestureThree2One_2, inputGesture), DTW(gestureThree2One_3, inputGesture)));
            double dtw_Three2Two = Math.Min(DTW(gestureThree2Two_1, inputGesture), Math.Min(DTW(gestureThree2Two_2, inputGesture), DTW(gestureThree2Two_3, inputGesture)));

            double dtw_Solve = Math.Min(DTW(gestureSolve_1, inputGesture), Math.Min(DTW(gestureSolve_2, inputGesture), DTW(gestureSolve_3, inputGesture)));
            double dtw_Refresh = Math.Min(DTW(gestureRefresh_1, inputGesture), Math.Min(DTW(gestureRefresh_2, inputGesture), DTW(gestureRefresh_3, inputGesture)));

            List<double> values = new List<double>();
            values.Add(dtw_One2Two);
            values.Add(dtw_One2Three);
            values.Add(dtw_Two2Three);
            values.Add(dtw_Two2One);
            values.Add(dtw_Three2One);
            values.Add(dtw_Three2Two);
            values.Add(dtw_Solve);
            values.Add(dtw_Refresh);

            if (values.Min() == dtw_One2Two) return Gestures.ONE2TWO;
            if (values.Min() == dtw_One2Three) return Gestures.ONE2THREE;
            if (values.Min() == dtw_Two2Three) return Gestures.TWO2THREE;
            if (values.Min() == dtw_Two2One) return Gestures.TWO2ONE;
            if (values.Min() == dtw_Three2One) return Gestures.THREE2ONE;
            if (values.Min() == dtw_Three2Two) return Gestures.THREE2TWO;
            if (values.Min() == dtw_Solve) return Gestures.SOLVE;
            if (values.Min() == dtw_Refresh) return Gestures.REFRESH;
            else return Gestures.NONE;
        }

        public double DTW(double[] arr1, double[] arr2)
        {
            int M = arr1.Length;
            int N = arr2.Length;
            var DTW = new double[M + 1, N + 1];
            //Initial matrix  
            DTW[0, 0] = 0;
            for (int j = 1; j <= M; j++)
            {
                DTW[j, 0] = double.PositiveInfinity;
            }
            for (int i = 1; i <= N; i++)
            {
                DTW[0, i] = double.PositiveInfinity;
            }
            //End of Init  
            for (int i = 1; i <= M; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    double cost = Math.Abs(arr1[i - 1] - arr2[j - 1]);
                    //double cost = calculateDistance(arr1[i - 1], arr2[j - 1]);
                    DTW[i, j] = cost + Math.Min(DTW[i - 1, j],          // insertion  
                                  Math.Min(DTW[i, j - 1],               // deletion  
                                       DTW[i - 1, j - 1]));             // match  
                }
            }

            double result = DTW[M, N];
            // normalisieren
            result = (result / (Math.Abs(M) + Math.Abs(N)));
            return result;
        }

        private double calculateDistance(double x, double y)
        {
            if (Math.Abs(x - y) < Math.PI)
            {
                return (1 / Math.PI) * Math.Abs(x - y);
            }
            else
            {
                return (1 / Math.PI) * (2 * Math.PI - Math.Abs(x - y));
            }
        }

        public static double calculateAngle(Point start, Point last, Point current)
        {
            // check if all points are on one line
            if ((start.X == 0 && last.X == 0 && current.X == 0) || (start.Y == 0 && last.Y == 0 && current.Y == 0)) return 0;

            // S = start, L = last, C = current
            // angle at point L is needed

            // calculate distances
            // source: http://stackoverflow.com/questions/1211212/how-to-calculate-an-angle-from-three-points
            double L2C = Math.Sqrt(Math.Pow(last.X - current.X, 2) + Math.Pow(last.Y - current.Y, 2));
            double L2S = Math.Sqrt(Math.Pow(last.X - start.X, 2) + Math.Pow(last.Y - start.Y, 2));
            double C2S = Math.Sqrt(Math.Pow(current.X - start.X, 2) + Math.Pow(current.Y - start.Y, 2));

            // calculate angle
            double result = Math.Acos((Math.Pow(L2C, 2) + Math.Pow(L2S, 2) - Math.Pow(C2S, 2)) / (2 * L2C * L2S));

            return Double.IsNaN(result) ? 0 : Math.Round(result * 180 / Math.PI, 4);
        }

        public static void writeGestureToConsole(List<double> gesture)
        {
            foreach (double angle in gesture)
            {
                Console.Write(angle.ToString().Replace(",", ".") + ", ");
            }
            Console.Write("\n");
        }
    }

    enum Gestures
    {
        ONE2TWO,
        ONE2THREE,
        TWO2THREE,
        TWO2ONE,
        THREE2ONE,
        THREE2TWO,
      
        MOVE,
        SOLVE,
        REFRESH,
        NONE
    }

    public class Worker
    {
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        private int count;
        private List<double> gesture;

        // This method will be called when the thread is started.
        public void DoWork()
        {
            _shouldStop = false;
            count = 0;

            Point start = new Point(-10000, -10000);
            Point last = new Point(-10000, -10000);
            Point current;

            gesture = new List<double>();

            while (!_shouldStop && count < 80)
            {

                current = new Point(Cursor.Position.X, Cursor.Position.Y);

                if (count == 0)
                {
                    start = current;
                    last = current;
                }

                gesture.Add(Gesture.calculateAngle(start, last, current));

                last = current;
                count++;

                Thread.Sleep(100);

            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }

        public List<double> getGesture()
        {
            return gesture;
        }
    }
}

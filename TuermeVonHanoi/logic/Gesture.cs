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
        /*
        double[] gestureOne2Two_1 = new double[] { 0, 0, 0, 129.2894, 177.9627, 178.6196, 176.8365, 171.1063, 175.684, 139.0856, 0, 40.4462, 43.394, 60.0184, 80.1807, 92.406, 108.4349, 116.1139, 0, 0, 0, 0, 150.3763, 147.9895, 155.8871, 166.9307, 167.8729, 168.518, 0, 0, 0, 0, 142.5337, 154.0124, 175.6777, 175.8919, 139.0634, 112.2372, 74.0657, 71.3996, 67.2638, 77.3914, 59.226, 101.4572, 124.8907, 0, 0, 144.6052, 150.5142, 156.1247, 156.2113, 0, 0 };
        double[] gestureOne2Two_2 = new double[] { 0, 0, 114.444, 173.0728, 174.462, 178.2101, 168.4602, 178.6173, 170.7775, 166.7486, 0, 0, 35.0778, 32.2389, 46.5786, 71.9395, 81.3475, 82.5686, 0, 0, 175.0303, 178.6028, 178.9678, 179.6292, 175.8724, 176.2916, 176.3255, 0, 0, 0, 138.6412, 147.7244, 177.3739, 171.2825, 145.5932, 87.3467, 64.3845, 51.3837, 63.4793, 66.9544, 81.9057, 127.5963, 0, 141.6913, 129.5386, 140.8555, 159.0481, 157.9247, 159.0832, 155.6207, 0 };
        double[] gestureOne2Two_3 = new double[] { 0, 0, 0, 173.8482, 179.1575, 174.6203, 177.9691, 173.6974, 178.7826, 166.6287, 158.3396, 0, 38.6598, 34.2824, 47.4744, 82.3356, 105.6422, 120.9638, 124.992, 0, 0, 0, 0, 0, 0, 129.4623, 140.5993, 151.3269, 157.3801, 160.71, 160.887, 161.3999, 0, 162.4991, 163.9926, 0, 0, 0, 0, 0, 0, 0, 0, 0, 119.1161, 115.8004, 139.5784, 167.6226, 122.9998, 126.8616, 67.613, 48.6914, 63.0478, 64.9533, 0, 144.0801, 153.477, 145.2585, 147.6573, 155.6199, 0, 0 };

        double[] gestureOne2Three_1 = new double[] { 0, 0, 0, 146.3099, 170.7355, 163.1378, 170.6764, 0, 0, 0, 0, 0, 0, 36.8699, 37.5041, 41.9815, 57.5288, 86.0548, 90, 99.7824, 95.9315, 105.2551, 113.8243, 0, 0, 0, 0, 92.2906, 130.1116, 145.9887, 159.6601, 141.0542, 168.559, 0, 0, 0, 0, 0, 168.8182, 160.8135, 161.2476, 176.1575, 0, 138.6013, 72.2052, 61.6227, 60.4507, 0, 0, 0, 157.5472, 165.8907, 157.3801, 46.9786, 42.9898, 44.3085, 51.8702, 75.0816, 61.1029, 78.426 };
        double[] gestureOne2Three_2 = new double[] { 0, 0, 0, 178.1524, 179.4271, 173.0157, 134.2361, 0, 0, 50.7106, 45.5774, 70.7841, 80.1975, 0, 87.5737, 127.5686, 0, 0, 0, 141.0725, 143.1301, 146.7251, 163.3008, 169.8245, 0, 0, 0, 9.9262, 0, 140.9374, 169.2986, 177.8168, 147.3887, 107.6106, 87.6048, 78.2195, 29.8673, 25.2211, 25.5451, 45.6882, 133.2053, 156.5014, 174.7214, 123.333, 56.9659, 44.1887, 46.4455, 51.7617, 0, 0, 0 };
        double[] gestureOne2Three_3 = new double[] { 0, 0, 169.6952, 179.736, 168.6313, 176.8753, 0, 151.0399, 118.8108, 36.6638, 33.9436, 42.9132, 69.7174, 93.8141, 0, 0, 0, 172.4054, 160.1188, 176.1859, 178.5498, 0, 0, 0, 0, 0, 178.6678, 178.6831, 174.4163, 177.2737, 160.5959, 146.7402, 90.7902, 66.4603, 44.2989, 35.3442, 18.5793, 0.7441, 0.7639, 0, 160.7802, 178.1374, 160.3123, 89.3364, 49.1338, 43.0329, 40.4125, 53.1104, 48.4566, 0 };

        double[] gestureTwo2Three_1 = new double[] { 0, 0, 172.874983651098, 147.528807709152, 153.766134611337, 63.434948822922, 57.5288077091515, 99.2461127455633, 0, 124.460816271372, 126.689722832476, 143.21160411926, 155.256462174523, 0, 0, 0, 0, 164.91671278459, 157.533250079004, 163.272036079432, 0, 106.225275361179, 0, 0, 163.495638618245, 178.289943021697, 68.8801438465753, 73.0509571318694, 24.9118119278561, 25.0037129447394, 6.66130163977128, 25.2608191641013, 166.235291978371, 166.636455957138, 95.9391328033818, 59.244999246221, 42.3251342973634, 31.6227587442272 };
        double[] gestureTwo2Three_2 = new double[] { 0, 0, 167.115380616899, 85.4338180139538, 54.7066524093397, 53.8792730852639, 53.7461622625552, 121.192097418862, 158.198590513648, 25.2472480756282, 21.5409759185381, 0, 157.750976342788, 161.816346844287, 164.919208688557, 0, 0, 144.806092759897, 159.372364922404, 158.635111322103, 63.7263086052155, 32.1438446531633, 130.142445784857, 173.36602607007, 98.4381702685243, 43.0362474434229, 39.8949095534076, 0 };
        double[] gestureTwo2Three_3 = new double[] { 0, 0, 156.974507991472, 98.6201621508697, 49.828236679721, 85.8106570975194, 139.666858371439, 106.757081182636, 114.665273436928, 145.967872182661, 0, 0, 0, 0, 0, 172.874983651098, 153.86156990269, 161.073445788292, 157.18401334527, 0, 0, 173.566017658499, 165.103722823291, 55.0546424282535, 50.8942490883054, 155.021532286551, 172.870852588017, 68.2932625825724, 59.7600583460591, 0, 47.2835910815672, 48.764398020469 };

        double[] gestureTwo2One_1 = new double[] { 0, 0, 169.286876977209, 55.9787466856048, 83.0240684708612, 135, 169.808498149972, 87.0734617224289, 95.6405494321568, 41.9872124958163, 56.6644012781373, 0, 0, 0, 142.476361106426, 139.304468960508, 150.336911337736, 0, 0, 0, 151.440379521681, 151.59190869168, 113.9907252638, 120.012638207753, 138.802585807071, 104.233361197725, 127.270249837809, 138.245254903888 };
        double[] gestureTwo2One_2 = new double[] { 0, 0, 155.349415277375, 62.2519440101067, 62.73079033924, 97.2271932883133, 0, 95.4875570600912, 114.193208987289, 141.259036305122, 147.901072745215, 0, 0, 148.134022306396, 150.101098161385, 158.481390682608, 165.042837696877, 0, 0, 123.538815662396, 98.6935607322571, 154.763736353096, 88.4182758798186, 104.309925549374, 128.233825177446 };
        double[] gestureTwo2One_3 = new double[] { 0, 0, 170.753887254437, 67.2855876468328, 58.9916899936149, 116.941766355053, 93.7722836093798, 94.3987053549955, 130.557938028601, 0, 0, 0, 122.776458115532, 125.41856799372, 95.4308112967108, 84.3348842054755, 98.3821729455959, 141.598173455059, 146.375115357744, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        double[] gestureThree2One_1 = new double[] { 0, 0, 163.0529, 90.4717, 28.8695, 52.4224, 137.2434, 145.3602, 80.7697, 93.0751, 17.0125, 17.3789, 104.2152, 134.3154, 0, 0, 114.1477, 88.6636, 72.1972, 113.0064, 103.1364, 125.4716 };
        double[] gestureThree2One_2 = new double[] { 0, 0, 152.9873, 156.6912, 101.7882, 71.0954, 53.6955, 139.0924, 149.5717, 168.7478, 96.0725, 94.6896, 94.11, 79.6111, 99.9262, 102.6804, 108.832, 120.5476, 0, 0, 109.5292, 113.6938, 110.6006, 116.0766, 124.8885 };
        double[] gestureThree2One_3 = new double[] { 0, 0, 166.5718, 162.5973, 160.0169, 98.7248, 78.3291, 66.0333, 50.7944, 37.4762, 6.5819, 132.2475, 150.0388, 168.8522, 132.7942, 86.8242, 59.8382, 72.2638, 92.2384, 0, 101.4158, 101.9442, 101.3921, 108.0984, 127.6856, 140.9061, 0, 0, 0, 0, 69.7825, 118.0725, 160.922, 179.4669, 87.908, 66.3687, 73.1846, 82.3988, 0, 133.2322, 135.341, 145.5926, 144.622, 0 };

        double[] gestureThree2Two_1 = new double[] { 0, 0, 0, 165.4655, 148.0991, 148.2922, 116.3202, 83.4537, 58.124, 68.6653, 83.1987, 80.2933, 0, 0, 110.6955, 133.698, 0, 0, 0, 0, 127.3332, 122.5322, 120.75, 114.0406, 82.1688, 83.5986, 88.4234, 0, 0, 0, 90.5256, 91.5766, 92.6264, 94.7201, 96.6473, 114.2015, 136.9001, 0, 0, 0, 0, 137.1611, 122.4203, 128.0362, 150.2266, 150.5861, 94.3125, 85.6653, 70.6996, 95.0006, 77.9838, 78.5353, 127.4762, 132.9373, 135.6161, 167.0386, 0, 0, 0 };
        double[] gestureThree2Two_2 = new double[] { 0, 0, 129.2894, 172.3746, 154.571, 136.9607, 122.7067, 80.9982, 58.2995, 30.8239, 29.2221, 20.7723, 0, 157.4569, 157.8337, 171.0274, 147.7599, 83.3675, 67.4259, 63.3537, 76.6943, 67.2703, 0, 0, 0, 112.0679, 128.0705, 119.835, 134.4662, 149.3493, 0, 0, 0, 0, 0, 111.4181, 139.7862, 178.4234, 163.884, 106.6558, 62.8222, 57.352, 59.3483, 0, 153.1219, 143.0821, 144.5784, 151.1071, 0, 27.6375 };
        double[] gestureThree2Two_3 = new double[] { 0, 0, 156.7323, 136.647, 119.5388, 45.2271, 153.2394, 153.7041, 68.3677, 67.2662, 61.6658, 117.4567, 122.7964, 138.9452, 0, 0, 126.9691, 157.0849, 82.8923, 55.3544, 64.9625, 145.7657, 147.9576 };

        double[] gestureSolve_1 = new double[] { 0, 0, 176.6891, 175.486, 177.1376, 178.4518, 0, 178.6361, 178.6678, 174.0602, 173.3675, 91.7899, 64.389, 68.9326, 91.9028, 114.0448, 135.85, 0, 0, 1.783, 5.1611, 0.4732, 2.1747, 13.1906, 56.3099, 96.3402, 157.3801, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        double[] gestureSolve_2 = new double[] { 0, 0, 177.594, 174.0939, 175.2364, 0, 4.6859, 4.7115, 61.1268, 87.6356, 101.0423, 91.4164, 76.8762, 76.0124, 91.4845, 0, 150.0602, 0, 0, 0, 0, 0, 0, 15.2163, 0.4747, 1.8794, 9.1328, 3.3577, 13.7702, 16.9093, 15.8324, 53.8068, 0, 113.1986 };
        double[] gestureSolve_3 = new double[] { 0, 0, 173.6598, 178.0047, 179.8036, 173.5065, 173.6095, 163.3262, 175.7493, 175.9922, 0, 0, 62.897, 54.5277, 84.13, 113.9828, 120.1008, 117.0506, 0, 55.6492, 37.4962, 9.1739, 5.4383, 5.3015, 7.684, 1.8476, 15.3128, 23.1986, 26.5651, 42.2737, 0 };

        double[] gestureRefresh_1 = new double[] { 0, 0, 143.6271, 112.651, 77.6833, 68.5249, 68.3378, 8.7355, 17.4027 };
        double[] gestureRefresh_2 = new double[] { 0, 0, 150.8411, 150.8569, 102.0694, 57.4466, 57.8883, 23.8655, 0.7045, 173.2902 };
        double[] gestureRefresh_3 = new double[] { 0, 0, 142.9055, 140.0063, 86.189, 62.0542, 117.1916, 15.3933, 0.1061, 0 };

        */
        double[] gestureOne_1 = new double[] { 0, 0, 168.4244, 174.9364, 0, 59.6346, 64.6841, 126.3868, 149.513, 0 };
        double[] gestureOne_2 = new double[] { 0, 0, 160.3462, 168.7379, 177.8789, 159.8637, 52.0284, 60.7086, 102.4998, 148.431, 0 };
        double[] gestureOne_3 = new double[] { 0, 0, 172.4762, 175.8611, 177.8014, 167.7615, 51.4108, 54.3276, 60.116, 80.2292, 103.4732, 114.3573, 124.7891, 0 };

        double[] gestureTwo_1 = new double[] { 0, 0, 170.2324, 138.7577, 95.5027, 70.4444, 88.0585, 126.4052, 148.3768, 0, 82.0734, 118.1625, 138.7782, 175.479 };
        double[] gestureTwo_2 = new double[] { 0, 0, 170.1555, 122.1743, 89.8469, 56.702, 90.6531, 107.6874, 132.5104, 0, 94.4454, 128.2418, 139.6355 };
        double[] gestureTwo_3 = new double[] { 0, 0, 167.7843, 163.2782, 157.0679, 81.8699, 54.2461, 54.951, 91.6555, 136.023, 162.3499, 76.139, 76.8425, 103.3291, 129.0443 };

        double[] gestureThree_1 = new double[] { 0, 0, 173.6869, 142.125, 97.3662, 38.6375, 57.2648, 131.6335, 142.5946, 165.4808, 123.5863, 118.7459, 91.8717, 85.6969, 94.0042, 93.2861, 88.5559, 0 };
        double[] gestureThree_2 = new double[] { 0, 0, 179.5342, 159.6189, 135.31, 84.314, 48.0762, 67.6199, 137.6209, 132.5303, 143.7684, 90.134, 83.1202, 96.3402, 91.6724 };
        double[] gestureThree_3 = new double[] { 0, 0, 170.1975, 151.9598, 112.249, 45.1752, 40.5972, 118.0245, 160.2821, 178.1247, 101.635, 75.0321, 80.6689, 87.3356, 78.9765, 0, };

        double[] gestureClose1_1 = new double[] { 0, 0, 172.875, 173.3156, 174.716, 164.2888, 164.9037, 151.7747 };
        double[] gestureClose1_2 = new double[] { 0, 0, 179.6712, 158.4094, 175.78, 175.0303 };
        double[] gestureClose1_3 = new double[] { 0, 0, 169.356, 175.535, 164.5641, 178.7829 };

        double[] gestureClose2_1 = new double[] { 0, 0, 169.7665, 150.5159, 160.9497, 165.1798, 176.8396 };
        double[] gestureClose2_2 = new double[] { 0, 0, 177.4144, 173.381, 155.6898, 162.5164, 171.3651, 135.1206, 34.9359 };
        double[] gestureClose2_3 = new double[] { 0, 0, 178.6274, 173.7543, 173.9028, 164.1337, 169.9352, 161.5964, 156.7176 };

        public Gestures identifyGesture(double[] inputGesture)
        {
            if (inputGesture.Length < 5)
            {
                Console.WriteLine("Not enough input to identify gesture.");
                return Gestures.NONE;
            }

            /*
            double dtw_One2Two = Math.Min(DTW(gestureOne2Two_1, inputGesture), Math.Min(DTW(gestureOne2Two_2, inputGesture), DTW(gestureOne2Two_3, inputGesture)));
            double dtw_One2Three = Math.Min(DTW(gestureOne2Three_1, inputGesture), Math.Min(DTW(gestureOne2Three_2, inputGesture), DTW(gestureOne2Three_3, inputGesture)));

            double dtw_Two2Three = Math.Min(DTW(gestureTwo2Three_1, inputGesture), Math.Min(DTW(gestureTwo2Three_2, inputGesture), DTW(gestureTwo2Three_3, inputGesture)));
            double dtw_Two2One = Math.Min(DTW(gestureTwo2One_1, inputGesture), Math.Min(DTW(gestureTwo2One_2, inputGesture), DTW(gestureTwo2One_3, inputGesture)));

            double dtw_Three2One = Math.Min(DTW(gestureThree2One_1, inputGesture), Math.Min(DTW(gestureThree2One_2, inputGesture), DTW(gestureThree2One_3, inputGesture)));
            double dtw_Three2Two = Math.Min(DTW(gestureThree2Two_1, inputGesture), Math.Min(DTW(gestureThree2Two_2, inputGesture), DTW(gestureThree2Two_3, inputGesture)));

            double dtw_Solve = Math.Min(DTW(gestureSolve_1, inputGesture), Math.Min(DTW(gestureSolve_2, inputGesture), DTW(gestureSolve_3, inputGesture)));
            double dtw_Refresh = Math.Min(DTW(gestureRefresh_1, inputGesture), Math.Min(DTW(gestureRefresh_2, inputGesture), DTW(gestureRefresh_3, inputGesture)));
            */

            double dtw_One = Math.Min(DTW(gestureOne_1, inputGesture), Math.Min(DTW(gestureOne_2, inputGesture), DTW(gestureOne_3, inputGesture)));
            double dtw_Two = Math.Min(DTW(gestureTwo_1, inputGesture), Math.Min(DTW(gestureTwo_2, inputGesture), DTW(gestureTwo_3, inputGesture)));
            double dtw_Three = Math.Min(DTW(gestureThree_1, inputGesture), Math.Min(DTW(gestureThree_2, inputGesture), DTW(gestureThree_3, inputGesture)));

            double dtw_Close1 = Math.Min(DTW(gestureClose1_1, inputGesture), Math.Min(DTW(gestureClose1_2, inputGesture), DTW(gestureClose1_3, inputGesture)));
            double dtw_Close2 = Math.Min(DTW(gestureClose2_1, inputGesture), Math.Min(DTW(gestureClose2_2, inputGesture), DTW(gestureClose2_3, inputGesture)));

            List<double> values = new List<double>();
            /*
            values.Add(dtw_One2Two);
            values.Add(dtw_One2Three);
            values.Add(dtw_Two2Three);
            values.Add(dtw_Two2One);
            values.Add(dtw_Three2One);
            values.Add(dtw_Three2Two);

            values.Add(dtw_Solve);
            values.Add(dtw_Refresh);
            */

            values.Add(dtw_One);
            values.Add(dtw_Two);
            values.Add(dtw_Three);

            values.Add(dtw_Close1);
            values.Add(dtw_Close2);

            /*
            if (values.Min() == dtw_One2Two) return Gestures.ONE2TWO;
            if (values.Min() == dtw_One2Three) return Gestures.ONE2THREE;
            if (values.Min() == dtw_Two2Three) return Gestures.TWO2THREE;
            if (values.Min() == dtw_Two2One) return Gestures.TWO2ONE;
            if (values.Min() == dtw_Three2One) return Gestures.THREE2ONE;
            if (values.Min() == dtw_Three2Two) return Gestures.THREE2TWO;
            if (values.Min() == dtw_Solve) return Gestures.SOLVE;
            if (values.Min() == dtw_Refresh) return Gestures.REFRESH;
            */
            if (values.Min() == dtw_One) return Gestures.ONE;
            if (values.Min() == dtw_Two) return Gestures.TWO;
            if (values.Min() == dtw_Three) return Gestures.THREE;
            if (values.Min() == dtw_Close1) return Gestures.CLOSE_1;
            if (values.Min() == dtw_Close2) return Gestures.CLOSE_2;
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
        NONE,

        CLOSE_1,
        CLOSE_2,
        ONE,
        TWO,
        THREE
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

                Thread.Sleep(50);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ZedGraph;

namespace Lab_02_EMSE
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        public ApplicationViewModel()
        {

        }

        private GeneratedSamples samplesClass = new GeneratedSamples();

        // команда добавления нового объекта
        private RelayCommand ok_Command;
        public RelayCommand OKCommand
        {
            get
            {
                return ok_Command ??
                  (ok_Command = new RelayCommand(obj =>
                  {
                      Clear();
                      UD = samplesClass.GenerateUniformDistribution(volume, leftBound, rightBound);
                      ED = samplesClass.GenerateExponentialDistribution(volume, lambda);
                      ND = samplesClass.GenerateNormalDistribution(volume, meanValue, standardDeviation);                      

                      //Создание интервальных рядов
                      PointsListUD = CreatePointsList(IntervalList.GetIntervalRange(UD));
                      PointsListED = CreatePointsList(IntervalList.GetIntervalRange(ED));
                      PointsListND = CreatePointsList(IntervalList.GetIntervalRange(ND));

                      string res1, res2 = "";
                      StringResult = "Критерий восходящих и нисходящих серий" + Environment.NewLine;
                      res2 = samplesClass.SearchAscendingDescendingSeriesCriterion(UD, out res1);
                      StringResult += "Равномерный закон:" + Environment.NewLine + res1 + Environment.NewLine + res2 + Environment.NewLine;

                      res2 = samplesClass.SearchAscendingDescendingSeriesCriterion(ED, out res1);
                      StringResult += "Показательный закон:" + Environment.NewLine + res1 + Environment.NewLine + res2 + Environment.NewLine;

                      res2 = samplesClass.SearchAscendingDescendingSeriesCriterion(ND, out res1);
                      StringResult += "Нормальный закон:" + Environment.NewLine + res1 + Environment.NewLine + res2 + Environment.NewLine;
                                                                  
                      SearchEjectionWriteResult();
                  }));
            }
        }

        /// <summary>
        /// Удаление выбросов и вывод на экран диаграмм
        /// </summary>
        private void SearchEjectionWriteResult()
        {            
            int lap = 1;
            double[] ND_copy = new double[ND.Length];
            Array.Copy(ND,ND_copy,ND.Length);

            bool flag = false;

            List<Interval> intervalRangeND;
            do
            {
                
                intervalRangeND = IntervalList.GetIntervalRange(ND_copy);
                ZDotChart dotChartControl = CreateDotChart(ND_copy, lap);             
                double meanValue = IntervalList.GetMeanValue(intervalRangeND, ND_copy.Length),
                 standartDeviation = IntervalList.GetStandardDeviation(intervalRangeND, ND_copy.Length),
                 dispersion = IntervalList.GetDispersion(intervalRangeND, ND_copy.Length);
                string resTemp = "Мат ожидание: " + Math.Round(meanValue,3).ToString() + Environment.NewLine +
                    "σ: " + Math.Round(standartDeviation, 3).ToString() + Environment.NewLine +
                    "Дисперсия: " + Math.Round(dispersion,3).ToString() + Environment.NewLine;

                double max = ND_copy.Max();
                double Tmax = (max - meanValue) / standartDeviation;
                double Ttabl = TableStudent.Instance[ND_copy.Length - 2];
                resTemp += "Tmax(n) = " + Math.Round(Tmax,3).ToString() + ";  Tтабл = " 
                    + Math.Round(Ttabl,3).ToString() + " " + Environment.NewLine;
                if (Tmax >= TableStudent.Instance[ND_copy.Length - 2])
                {
                    resTemp += "Выброс Xmax = " + Math.Round(max,3).ToString() + "." + Environment.NewLine;
                    var tempND = ND_copy.ToList();
                    tempND.Remove(max);
                    ND_copy = tempND.ToArray();
                    flag = true;
                }
                else
                    flag = false;

                intervalRangeND = IntervalList.GetIntervalRange(ND_copy);
                meanValue = IntervalList.GetMeanValue(intervalRangeND, ND_copy.Length);
                standartDeviation = IntervalList.GetStandardDeviation(intervalRangeND, ND_copy.Length);
                dispersion = IntervalList.GetDispersion(intervalRangeND, ND_copy.Length);
                resTemp += Environment.NewLine +
                    "----------------------" + Environment.NewLine + Environment.NewLine +
                    "Мат ожидание: " + Math.Round(meanValue,3).ToString() + Environment.NewLine +
                    "σ: " + Math.Round(standartDeviation,3).ToString() + Environment.NewLine +
                    "Дисперсия: " + Math.Round(dispersion,3).ToString() + Environment.NewLine;

                double min = ND_copy.Min();
                double Tmin = (meanValue - min) / standartDeviation;
                Ttabl = TableStudent.Instance[ND_copy.Length - 2];
                resTemp += "Tmin(n) = " + Math.Round(Tmin,3) + "; Tтабл = " + Ttabl.ToString() + " " + Environment.NewLine;
                if (Tmin >= TableStudent.Instance[ND_copy.Length - 2])
                {
                    resTemp += "Выброс Xmin = " + Math.Round(min,3).ToString() + "." + Environment.NewLine;
                    flag = true;
                    var tempND = ND_copy.ToList();
                    tempND.Remove(min);
                    ND_copy = tempND.ToArray();
                }

                dotChartControl.TextRight = resTemp;
                //Построить график
                CollectionDotCharts.Add(dotChartControl);
                lap++;
            } while (flag);
            
        }

        private void Clear()
        {
            UD = null; ED = null; ND = null;
            if (PointsListUD != null)
                PointsListUD.Clear();
            if (PointsListED != null)
                PointsListED.Clear();
            if (PointsListND != null)
                PointsListND.Clear();

            StringResult = "";
            CollectionDotCharts.Clear();
        }

        /// <summary>
        /// Создание контрола DotChart
        /// </summary>
        /// <param name="distribution">совокупность</param>
        /// <param name="number">порядковый номер диаграммы</param>
        /// <returns></returns>
        private ZDotChart CreateDotChart(double[] distribution, int number)
        {
            PointPairList listResult = new PointPairList();
            foreach (var y in distribution.Select((x, index) => new PointPair(index + 1, x)))
                listResult.Add(y);

            ZDotChart dotChart = new ZDotChart();
            
            dotChart.Header = "Проход: " + number.ToString();            
            dotChart.Width = 1300;
            dotChart.Height = 500;

            Func<double, PointPairList> foo = (double m) =>
                {
                    PointPairList list = new PointPairList();
                    foreach (var p in listResult.Where(t => t.Y == m))
                        list.Add(p);
                    return list;
                };
            dotChart.PointsMaxList = foo(listResult.Max(x => x.Y));
            dotChart.PointsMinList = foo(listResult.Min(x => x.Y));
            dotChart.PointsValues = listResult;

            return dotChart;
        }

        private string stringResult;
        /// <summary>
        /// Результат
        /// </summary>
        public string StringResult
        {
            get { return stringResult; }
            set
            {
                stringResult = value;
                OnPropertyChanged("StringResult");
            }
        }

        //Список графиков (выбросы)
        private ObservableCollection<ZDotChart> collectionDotCharts = new ObservableCollection<ZDotChart>();
        public ObservableCollection<ZDotChart> CollectionDotCharts
        {
            get { return collectionDotCharts; }
            set
            {
                collectionDotCharts = value;
                OnPropertyChanged("CollectionDotCharts");
            }
        }

        private PointPairList CreatePointsList(List<Interval> iList)
        {
            PointPairList p_list = new PointPairList();
            foreach (var xxx in iList)
                p_list.Add(new PointPair(xxx.X_average, xxx.ni));
            return p_list;
        }

        private PointPairList pointsListUD;
        public PointPairList PointsListUD
        {
            get { return pointsListUD; }
            set
            {
                pointsListUD = value;
                OnPropertyChanged("PointsListUD");
            }
        }

        private PointPairList pointsListED;
        public PointPairList PointsListED
        {
            get { return pointsListED; }
            set
            {
                pointsListED = value;
                OnPropertyChanged("PointsListED");
            }
        }
        private PointPairList pointsListND;
        public PointPairList PointsListND
        {
            get { return pointsListND; }
            set
            {
                pointsListND = value;
                OnPropertyChanged("PointsListND");
            }
        }

        private double[] _UD;
        /// <summary>
        /// Совокупность по равномерному закону
        /// </summary>
        public double[] UD
        {
            get { return _UD; }
            set
            {
                _UD = value;
                OnPropertyChanged("UD");
            }
        }

        private double[] _ED;
        /// <summary>
        /// Совокупность по показательному закону
        /// </summary>
        public double[] ED
        {
            get { return _ED; }
            set
            {
                _ED = value;
                OnPropertyChanged("ED");
            }
        }

        private double[] _ND;
        /// <summary>
        /// Совокупность по нормальному закону
        /// </summary>
        public double[] ND
        {
            get { return _ND; }
            set
            {
                _ND = value;
                OnPropertyChanged("ND");
            }
        }

        private Int32 volume = 90;
        public Int32 Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                OnPropertyChanged("Volume");
            }
        }

        private double leftBound = 50;
        public double LeftBound
        {
            get { return leftBound; }
            set
            {
                leftBound = value;
                OnPropertyChanged("LeftBound");
            }
        }

        private double rightBound = 75;
        public double RightBound
        {
            get { return rightBound; }
            set
            {
                rightBound = value;
                OnPropertyChanged("RightBound");
            }
        }

        private double lambda = 0.9;
        public double Lambda
        {
            get { return lambda; }
            set
            {
                lambda = value;
                OnPropertyChanged("Lambda");
            }
        }

        private double meanValue = 0.52;
        public double MeanValue
        {
            get { return meanValue; }
            set
            {
                meanValue = value;
                OnPropertyChanged("MeanValue");
            }
        }

        /// <summary>
        /// sigma
        /// </summary>
        private double standardDeviation = 0.8;
        public double StandardDeviation
        {
            get { return standardDeviation; }
            set
            {
                standardDeviation = value;
                OnPropertyChanged("StandardDeviation");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

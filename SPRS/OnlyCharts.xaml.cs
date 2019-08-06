using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Loadcell
{
    public partial class OnlyCharts : Window
    {
        ObservableCollection<ChartData>[] chartData = new ObservableCollection<ChartData>[4];
        ChartData[] objChartData= new ChartData[4];
        Thread chartThread;
        DateTime dtNow = DateTime.Now;

        public OnlyCharts()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Grid_KeyDown);

            for(int i=0;i<chartData.Length; i++)
            {
                chartData[i] = new ObservableCollection<ChartData>();
                objChartData[i] = new ChartData() { Name = dtNow, Value = 0.0 };

                chartData[i].Add(objChartData[i]);
                chartData[i].Add(new ChartData() { Name = (dtNow + TimeSpan.FromSeconds(5)), Value = Math.Round(SPRS.data1[i], 2) });
            }


            // UL_Chart-----------       
            xAxisUL.Minimum = chartData[0][0].Name;
            UL_Chart.DataContext = chartData[0];


            // UR_Chart-----------         
            xAxisUR.Minimum = chartData[1][0].Name;
            UR_Chart.DataContext = chartData[1];


            // DL_Chart-----------
            xAxisDL.Minimum = chartData[2][0].Name;
            DL_Chart.DataContext = chartData[2];


            // DR_Chart-----------
            xAxisDR.Minimum = chartData[3][0].Name;
            DR_Chart.DataContext = chartData[3];


            // thread---------------------------
            chartThread = new Thread(new ThreadStart(StartChartDataSimulation));
            chartThread.Start();
        }
        
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            Window mainWin = sender as Window;

            if (e.Key == Key.Escape)
            {
                chartThread.Abort();
                mainWin.Close();
            }
        }

        public void StartChartDataSimulation()
        {
            int i1 = 0; int i2 = 0; int i3 = 0; int i4 = 0;

            while (true)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    //UL_Chart
                    var data1 = new ChartData() { Name = DateTime.Now, Value = Math.Round(SPRS.data1[0], 2) };
                    chartData[0].Add(data1);

                    if (chartData[0].Count % 50 == 0 && i1 == 0)
                    {
                        xAxisUL.Minimum = chartData[0][i1 + 1].Name;
                        i1++;
                    }
                    if (i1 >= 1)
                    {
                        xAxisUL.Minimum = chartData[0][i1 + 1].Name;
                        i1++;
                    }


                    //UR_Chart
                    var data2 = new ChartData() { Name = DateTime.Now, Value = Math.Round(SPRS.data1[1], 2) };
                    chartData[1].Add(data2);

                    if (chartData[1].Count % 50 == 0 && i2 == 0)
                    {
                        xAxisUR.Minimum = chartData[1][i2 + 1].Name;
                        i2++;
                    }
                    if (i2 >= 1)
                    {
                        xAxisUR.Minimum = chartData[1][i2 + 1].Name;
                        i2++;
                    }


                    //DL_Chart
                    var data3 = new ChartData() { Name = DateTime.Now, Value = Math.Round(SPRS.data1[2], 2) };
                    chartData[2].Add(data3);

                    if (chartData[2].Count % 50 == 0 && i3 == 0)
                    {
                        xAxisDL.Minimum = chartData[2][i3 + 1].Name;
                        i3++;
                    }
                    if (i3 >= 1)
                    {
                        xAxisDL.Minimum = chartData[2][i3 + 1].Name;
                        i3++;
                    }


                    //UR_Chart
                    var data4 = new ChartData() { Name = DateTime.Now, Value = Math.Round(SPRS.data1[3], 2) };
                    chartData[3].Add(data4);

                    if (chartData[3].Count % 50 == 0 && i4 == 0)
                    {
                        xAxisDR.Minimum = chartData[3][i4 + 1].Name;
                        i4++;
                    }
                    if (i4 >= 1)
                    {
                        xAxisDR.Minimum = chartData[3][i4 + 1].Name;
                        i4++;
                    }

                }));

                Thread.Sleep(100);
            }
        }

    }


    //------------------------------------------------
    public class ChartData : INotifyPropertyChanged
    {
        DateTime _Name;
        double _Value;

        #region properties

        public DateTime Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

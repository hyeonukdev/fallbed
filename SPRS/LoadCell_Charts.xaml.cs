using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Configurations;
using SPRS;

namespace Loadcell
{    
    public partial class LoadCell_Charts : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private double _axisStep;

        private double _trend1;
        private double _trend2;
        private double _trend3;
        private double _trend4;

        public LoadCell_Charts()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the values property will store our values array
            ChartValues_UL = new ChartValues<MeasureModel>();
            ChartValues_DL = new ChartValues<MeasureModel>();
            ChartValues_DR = new ChartValues<MeasureModel>();
            ChartValues_UR = new ChartValues<MeasureModel>();

            //lets set how to display the X Labels
            DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);

            IsReading = true;

            DataContext = this;

            Task.Factory.StartNew(Read);
        }
        public ChartValues<MeasureModel> ChartValues_UL { get; set; }
        public ChartValues<MeasureModel> ChartValues_DL { get; set; }
        public ChartValues<MeasureModel> ChartValues_DR { get; set; }
        public ChartValues<MeasureModel> ChartValues_UR { get; set; }

        public Func<double, string> DateTimeFormatter { get; set; }

        public double AxisStep { get; set; }
        //public double AxisStep
        //{
        //    get { return _axisStep; }
        //    set
        //    {
        //        _axisStep = value;
        //        OnPropertyChanged("AxisStep");
        //    }
        //}

        public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        public bool IsReading { get; set; }

        private void Read()
        {
            while (IsReading)
            {
                Thread.Sleep(50);
                var now = DateTime.Now;

                _trend1 = SPRS.data1[0];
                ChartValues_UL.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend1
                });

                _trend2 = SPRS.data1[2];
                ChartValues_DL.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend2
                });

                _trend3 = SPRS.data1[3];
                ChartValues_DR.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend3
                });

                _trend4 = SPRS.data1[1];
                ChartValues_UR.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend4
                });

                SetAxisLimits(now);


                int lastValue = 200;

                if (ChartValues_UL.Count > lastValue) ChartValues_UL.RemoveAt(0);
                if (ChartValues_DL.Count > lastValue) ChartValues_DL.RemoveAt(0);
                if (ChartValues_DR.Count > lastValue) ChartValues_DR.RemoveAt(0);
                if (ChartValues_UR.Count > lastValue) ChartValues_UR.RemoveAt(0);
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            //AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            //AxisMin = now.Ticks - TimeSpan.FromSeconds(1).Ticks; // and 8 seconds behind
            AxisMin = now.Ticks;
            //if (AxisStep>0)
            //{
            //    AxisStep = now.Ticks + TimeSpan.FromSeconds(1).Ticks;
            //}
        }

        public void InjectStopOnClick(object sender, RoutedEventArgs e)
        {
            IsReading = !IsReading;
            if (IsReading) Task.Factory.StartNew(Read);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
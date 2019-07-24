using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO.Ports;
using System.Threading;
using System.Xml;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace Loadcell
{

    public partial class MulitBeds : Window
    {

        //---------------------------------------
        public MulitBeds()
        {
            InitializeComponent();
            Drawbeds();
        }
        private void Drawbeds()
        {
            canvas1.Children.Clear();

            canvas1.Children.Add(title);

            canvas1.Children.Add(bed1);
            canvas1.Children.Add(bed2);
            canvas1.Children.Add(bed3);
            canvas1.Children.Add(bed4);
            canvas1.Children.Add(bed5);

            canvas1.Children.Add(bed6);
            canvas1.Children.Add(bed7);
            canvas1.Children.Add(bed8);
            canvas1.Children.Add(bed9);
            canvas1.Children.Add(bed10);

        }
        void bed1Btn_Click(object sender, RoutedEventArgs args)
        {

            this.Close();
        }
        void bed2Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
        void bed3Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed4Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed5Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed6Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed7Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed8Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed9Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        void bed10Btn_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }



    }
}
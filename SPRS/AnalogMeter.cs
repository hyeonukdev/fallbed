using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Loadcell
{
    partial class AnalogMeter
    {
        double fromDegree = 0;
        double toDegree = 0;
        double fromRadian = 0;
        double toRadian = 0;
        double fromValue = 0;
        double toValue = 0;
        //double radius = 100;
        bool lowerCoverHide = false;
        double needleLength = 0;

        int[] scaleLines = null;
        Line[] scaleMarks = null;
        string[] scaleTexts = null;
        Label[] scaleTextLabel = null;
        Brush ScaleMarkStroke = Brushes.Blue;
        double ScaleMarkStrokeThickness = 2;
        Brush ScaleTextForeground = Brushes.Black;
        double ScaleTextFontSize = 12;
        double ScaleLabelWidth = 20;
        double ScaleLabelHeight = 30;
        double ScaleValue = 0;
        
        //double needleAngle = 0;
        FontFamily ScaleTextFontFamily = new FontFamily("OCR A Extended");
        FontWeight ScaleTextFontWeight = FontWeights.Normal;

        bool initialized = false;

        //--------------------------------------------------
        public AnalogMeter()
        {
            InitializeComponent();
        }//Constructor()

        //--------------------------------
        public Brush MeterFill
        {
            get { return circlePanel.Fill; }
            set
            {
                circlePanel.Fill = value;
            }
        }

        //--------------------------------
        public Brush MeterStroke
        {
            get { return panelBorder.Stroke; }
            set
            {
                panelBorder.Stroke = value;
            }
        }

        //--------------------------------
        public double MeterStrokeThickness
        {
            get { return panelBorder.StrokeThickness; }
            set
            {
                panelBorder.StrokeThickness = value;
            }
        }

        //--------------------------------
        public Brush CenterRingFill
        {
            get { return centerRing.Fill; }
            set
            {
                centerRing.Fill = value;
            }
        }

        //--------------------------------
        public Brush CenterRingStroke
        {
            get { return centerRing.Stroke; }
            set
            {
                centerRing.Stroke = value;
            }
        }

        //--------------------------------
        public double CenterRingStrokeThickness
        {
            get { return centerRing.StrokeThickness; }
            set
            {
                centerRing.StrokeThickness = value;
            }
        }

        //--------------------------------
        public Brush LowerCoverFill
        {
            get { return lowerCover.Fill; }
            set
            {
                lowerCover.Fill = value;
            }
        }

        //--------------------------------
        public bool LowerCoverHide
        {
            get { return lowerCoverHide; }
            set
            {
                lowerCoverHide = value;
                if (lowerCoverHide) Canvas.SetZIndex(lowerCover, 0);
                else Canvas.SetZIndex(lowerCover, 2);
            }
        }

        //--------------------------------
        public Brush NeedleStroke
        {
            get { return needle.Stroke; }
            set
            {
                needle.Stroke = value;
            }
        }

        //--------------------------------
        public double NeedleStrokeThickness
        {
            get { return needle.StrokeThickness; }
            set
            {
                needle.StrokeThickness = value;
            }
        }

        //--------------------------------
        public double DigitalDisplayWidth
        {
            get { return digitalDisplay.Width; }
            set
            {
                digitalDisplay.Width = value;
            }
        }

        //--------------------------------
        public double DigitalDisplayHeight
        {
            get { return digitalDisplay.Height; }
            set
            {
                digitalDisplay.Height = value;
            }
        }

        //--------------------------------
        public double DigitalDisplayFontSize
        {
            get { return digitalDisplay.FontSize; }
            set
            {
                digitalDisplay.FontSize = value;
            }
        }

        //--------------------------------
        public FontWeight DigitalDisplayFontWeight
        {
            get { return digitalDisplay.FontWeight; }
            set
            {
                digitalDisplay.FontWeight = value;
            }
        }

        //--------------------------------
        public Brush DigitalDisplayBackGround
        {
            get { return digitalDisplay.Background; }
            set
            {
                digitalDisplay.Background = value;
            }
        }

        //--------------------------------
        public Brush DigitalDisplayForeground
        {
            get { return digitalDisplay.Foreground; }
            set
            {
                digitalDisplay.Foreground = value;
            }
        }

        //--------------------------------------------------
        public Brush ScaleStroke
        {
            get
            {
                return ScaleMarkStroke;
            }
            set
            {
                ScaleMarkStroke = value;
                if (scaleMarks == null) return;

                for(int i=0; i<scaleMarks.Length; i++) {
                    scaleMarks[i].Stroke = value;
                }
            }
        }

        //--------------------------------------------------
        public double ScaleStrokeThickness
        {
            get
            {
                return ScaleMarkStrokeThickness;
            }
            set
            {
                ScaleMarkStrokeThickness = value;
                if (scaleMarks == null) return;

                for (int i = 0; i < scaleMarks.Length; i++) {
                    scaleMarks[i].StrokeThickness = value;
                }
            }
        }

        //--------------------------------------------------
        public Brush ScaleForeground
        {
            get
            {
                return ScaleTextForeground;
            }
            set
            {
                ScaleTextForeground = value;
                if (scaleTextLabel == null) return;

                for (int i = 0; i < scaleTextLabel.Length; i++) {
                    scaleTextLabel[i].Foreground = value;
                }

            }
        }

        //--------------------------------------------------
        public double ScaleFontSize
        {
            get
            {
                return ScaleTextFontSize;
            }
            set
            {
                ScaleTextFontSize = value;

                if (scaleTextLabel == null) return;

                for (int i = 0; i < scaleTextLabel.Length; i++) {
                    scaleTextLabel[i].FontSize = value;
                }

            }
        }

        //--------------------------------------------------
        public FontFamily ScaleFontFamily
        {
            get
            {
                return ScaleTextFontFamily;
            }
            set
            {
                ScaleTextFontFamily = value;

                if (scaleTextLabel == null) return;

                for (int i = 0; i < scaleTextLabel.Length; i++)
                {
                    scaleTextLabel[i].FontFamily = value;
                }
            }
        }

        //--------------------------------------------------
        public FontWeight ScaleFontWeight
        {
            get
            {
                return ScaleTextFontWeight;
            }
            set
            {
                ScaleTextFontWeight = value;

                if (scaleTextLabel == null) return;

                for (int i = 0; i < scaleTextLabel.Length; i++)
                {
                    scaleTextLabel[i].FontWeight = value;
                }
            }
        }

        //--------------------------------------------------
        public double ScaleTextBoxWidth
        {
            get
            {
                return ScaleLabelWidth;
            }
            set
            {
                ScaleLabelWidth = value;

                if (scaleTextLabel == null) return;

                drawScaleText();

            }
        }

        //--------------------------------------------------
        public double ScaleTextBoxHeight
        {
            get
            {
                return ScaleLabelHeight;
            }
            set
            {
                ScaleLabelHeight = value;

                if (scaleTextLabel == null) return;

                drawScaleText();

            }
        }

        //--------------------------------------------------
        private void AnalogMeterLoaded(object sender, RoutedEventArgs e)
        {
            initialized = false;

            double x, y;
            double radius = (ActualWidth > ActualHeight) ? (ActualHeight / 2) : (ActualWidth / 2);

            circlePanel.Width = radius * 2;
            circlePanel.Height = radius * 2;
            panelBorder.Width = radius * 2;
            panelBorder.Height = radius * 2;

            needleLength = radius * 0.75;
            needle.X1 = ActualWidth / 2;
            needle.Y1 = ActualHeight / 2;
            setNeedlePosition(ScaleValue);

            //needle.X2 = needleLength * xSinTable.Cos(needleAngle) + ActualWidth / 2;
            //needle.Y2 = ActualHeight - (needleLength * xSinTable.Sin(needleAngle) + ActualHeight / 2);

            Canvas.SetLeft(circlePanel, ActualWidth / 2 - radius);
            Canvas.SetTop(circlePanel, ActualHeight / 2 - radius);
            Canvas.SetLeft(panelBorder, ActualWidth / 2 - radius);
            Canvas.SetTop(panelBorder, ActualHeight / 2 - radius);

            double numPoints = 50;
            double delta = Math.PI * 2 / numPoints;

            double arcStart = 200.0 / 180.0 * Math.PI;
            double arcEnd = 340.0 / 180.0 * Math.PI;
            PointCollection points = new PointCollection();

            Vector startPoint = new Vector(radius * Math.Cos(arcStart), radius * Math.Sin(arcStart));
            Vector endPoint = new Vector(radius * Math.Cos(arcEnd), radius * Math.Sin(arcEnd));

            for (double angle = arcStart; angle < arcEnd; angle += delta)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);
                points.Add(new Point(x + ActualWidth / 2, ActualHeight - (y + ActualHeight / 2)));
            }
            points.Add(new Point(endPoint.X + ActualWidth / 2, ActualHeight - (endPoint.Y + ActualHeight / 2)));


            double yMove = radius * 2.5;
            Vector newOrigin = new Vector(0, -yMove);
            Vector newRefLine = new Vector(radius, -yMove);

            Vector startVector = startPoint - newOrigin;
            Vector endVector = endPoint - newOrigin;
            Vector refVector = newRefLine - newOrigin;

            double newArcStart = Vector.AngleBetween(refVector, endVector);
            double newArcEnd = Vector.AngleBetween(refVector, startVector);
            double newRadius = startVector.Length;
            double newDelta = delta = Math.PI * 2 / 80;

            newArcStart = newArcStart / 180.0 * Math.PI;
            newArcEnd = newArcEnd / 180.0 * Math.PI;

            for (double angle = newArcStart; angle < newArcEnd; angle += newDelta)
            {
                x = newRadius * Math.Cos(angle);
                y = newRadius * Math.Sin(angle);
                points.Add(new Point(x + ActualWidth / 2, ActualHeight - (y - yMove + ActualHeight / 2)));
            }
            points.Add(new Point(startPoint.X + ActualWidth / 2, ActualHeight - (startPoint.Y + ActualHeight / 2)));

            lowerCover.Points.Clear();
            lowerCover.Points = points;

            centerRing.Width = radius * 0.5;
            centerRing.Height = radius * 0.5;
            Canvas.SetLeft(centerRing, ActualWidth / 2 - centerRing.Width / 2);
            Canvas.SetTop(centerRing, ActualHeight / 2 - centerRing.Height / 2);

            Canvas.SetLeft(digitalDisplay, ActualWidth / 2 - digitalDisplay.Width / 2);
            Canvas.SetTop(digitalDisplay, ActualHeight / 2 - digitalDisplay.Height / 2 + radius * 0.5);
            digitalDisplay.Content = ScaleValue.ToString();

            initialized = true;

            drawScale();

        }

        //--------------------------------------------------
        private void drawScale()
        {
            if (!initialized) return;
            if (scaleLines == null) return;
            if (scaleLines.Length == 0) return;

            double radius = circlePanel.Width / 2;
            double maxScaleMarkLen = radius * 0.2;

            if (scaleMarks != null)
            {
                for (int i = 0; i < scaleMarks.Length; i++)
                {
                    AnalogMeterCanvas.Children.Remove(scaleMarks[i]);
                }
            }

            int num = scaleLines.Length;

            scaleMarks = new Line[num];

            double delta = (toRadian - fromRadian) / (num - 1);

            for (int i = 0; i < num; i++)
            {
                scaleMarks[i] = new Line();
                double angle = fromRadian + delta * i;
                double lineLen = (scaleLines[i] < maxScaleMarkLen) ? scaleLines[i] : maxScaleMarkLen;

                scaleMarks[i].Stroke = ScaleMarkStroke;
                scaleMarks[i].StrokeThickness = ScaleMarkStrokeThickness;

                Point pt1 = new Point(radius * Math.Cos(angle), radius * Math.Sin(angle));
                Point pt2 = new Point((radius - lineLen) * Math.Cos(angle), (radius - lineLen) * Math.Sin(angle));
                scaleMarks[i].X1 = pt1.X + ActualWidth / 2;
                scaleMarks[i].Y1 = ActualHeight - (pt1.Y + ActualHeight / 2);
                scaleMarks[i].X2 = pt2.X + ActualWidth / 2;
                scaleMarks[i].Y2 = ActualHeight - (pt2.Y + ActualHeight / 2);
                Canvas.SetZIndex(scaleMarks[i], 3);

                AnalogMeterCanvas.Children.Add(scaleMarks[i]);

            }

            if (scaleTexts != null) drawScaleText();

        }

        //----------------------------------------------------
        private void drawScaleText()
        {
            if (!initialized) return;
            if (scaleTexts == null) return;
            if (scaleTexts.Length == 0) return;

            if (scaleTextLabel != null)
            {
                for (int i = 0; i < scaleTextLabel.Length; i++)
                {
                    AnalogMeterCanvas.Children.Remove(scaleTextLabel[i]);
                }
            }

            int num = scaleTexts.Length;

            scaleTextLabel = new Label[num];

            double radius = circlePanel.Width / 2;
            double delta = (toRadian - fromRadian) / (num - 1);
            double textRadius = radius * 0.8;

            for (int i = 0; i < num; i++)
            {
                double angle = fromRadian + delta * i;

                double x, y;

                scaleTextLabel[i] = new Label();
                scaleTextLabel[i].Padding = new Thickness(0);

                scaleTextLabel[i].Width = ScaleLabelWidth;
                scaleTextLabel[i].Height = ScaleLabelHeight;
                scaleTextLabel[i].Content = scaleTexts[i];
                scaleTextLabel[i].FontSize = ScaleTextFontSize;
                scaleTextLabel[i].Foreground = ScaleTextForeground;
                scaleTextLabel[i].HorizontalContentAlignment = HorizontalAlignment.Center;
                scaleTextLabel[i].VerticalContentAlignment = VerticalAlignment.Center;
                scaleTextLabel[i].FontFamily = ScaleTextFontFamily;
                scaleTextLabel[i].FontWeight = ScaleTextFontWeight;

                x = textRadius * Math.Cos(angle) + ActualWidth / 2 - scaleTextLabel[i].Width / 2;
                y = ActualHeight - (textRadius * Math.Sin(angle) + ActualHeight / 2 + scaleTextLabel[i].Height / 2);

                Canvas.SetZIndex(scaleTextLabel[i], 5);
                Canvas.SetLeft(scaleTextLabel[i], x);
                Canvas.SetTop(scaleTextLabel[i], y);

                AnalogMeterCanvas.Children.Add(scaleTextLabel[i]);

            }
        }

        //--------------------------------------------------
        public void setScale(double fromDegree, double toDegree, int[] lines, string[] texts,
                             double fromValue = 0, double toValue = 0)
        {

            this.fromDegree = fromDegree;
            this.toDegree = toDegree;
            fromRadian = fromDegree / 180.0 * Math.PI;
            toRadian = toDegree / 180.0 * Math.PI;
            scaleLines = lines;
            scaleTexts = texts;
            this.fromValue = fromValue;
            this.toValue = toValue;
            drawScale();
        }

        //--------------------------------------------------
        public void setNeedlePosition(double targetValue)
        {
            double ratio;
            double needleAngle = 0;

            ScaleValue = targetValue;

            if (fromValue == toValue) ratio = 0.5;
            else ratio = (targetValue - fromValue) / (toValue - fromValue);

            if (ratio < 0) needleAngle = fromDegree;
            else if (ratio > 1) needleAngle = toDegree;
            else needleAngle = fromDegree + (toDegree - fromDegree) * ratio;

            needle.X2 = needleLength * xSinTable.Cos(needleAngle) + ActualWidth / 2;
            needle.Y2 = ActualHeight - (needleLength * xSinTable.Sin(needleAngle) + ActualHeight / 2);

            //digitalDisplay.Content = roundUpDouble1(targetValue).ToString();
            digitalDisplay.Content = targetValue.ToString("N1");


        }

        private double roundUpDouble1(double x)
        {
            return ((int)(((x > 0.0) ? (x + 0.05) : (x - 0.05)) * 10.0)) * 0.1;
        }

        public static implicit operator double(AnalogMeter v)
        {
            throw new NotImplementedException();
        }
    }//class AnalogMeter
  
}//ns
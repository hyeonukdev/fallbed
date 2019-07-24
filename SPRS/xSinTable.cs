using System;

namespace Loadcell
{
    public static class xSinTable
    {
        static double[] sinTable;

        static xSinTable()
        {
            sinTable = new double[91];
            for(int i=0; i<=90; i++)
            {
                sinTable[i] = Math.Sin(i / 180.0 * Math.PI);
            }
        }

        public static double Sin(double degree)
        {
            int x =  (degree > 0) ? (int)(degree + 0.5) : (int)(degree - 0.5);

            if ((x > 360) || (x < 0)) x %= 360;
            if (x < 0) x += 360;

            if (x < 90) return sinTable[x];
            else if (x < 180) return sinTable[180 - x];
            else if (x < 270) return -(sinTable[x - 180]);
            else return -(sinTable[360 - x]);
         }

        public static double Cos(double degree)
        {
            int x = (degree > 0) ? (int)(degree + 0.5) : (int)(degree - 0.5);

            if ((x > 360) || (x < 0)) x %= 360;
            if (x < 0) x += 360;

            if (x < 90) return sinTable[90-x];
            else if (x < 180) return -(sinTable[90-(180 - x)]);
            else if (x < 270) return -(sinTable[90-(x - 180)]);
            else return sinTable[90-(360 - x)];
  
        }

        public static double Tan(double degree)
        {
            double cos = Cos(degree);
            if (cos == 0) throw new Exception("Zero Divided Error");
            return Sin(degree) / cos;
        }
    }//class
}//ns
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingBookManager.Physics
{
    public class MovmentCanon
    {
        public static double g = 9.8;
        public double AngelAlpha, AngelGamma;
        public double InitalizingVelocity;
        public double InitalizingHeight;
        public double CononLength;

        private double cosX, cosY, cosZ;

        public MovmentCanon
            (double angelAlpha,double angelGamma,double cononLength,double initalizingVelocity,double initalizingHeight)
        {
            AngelAlpha = angelAlpha;
            AngelGamma = angelGamma;
            CononLength = cononLength;
            InitalizingHeight = initalizingHeight;
            InitalizingVelocity = initalizingVelocity;
            cosX = - Math.Sin(angelAlpha) * Math.Cos(angelGamma);
            cosY=Math.Cos(angelAlpha);
            cosZ = Math.Sin(angelAlpha) * Math.Sin(angelGamma);
        }

        public MovmentCanon
    (string angelAlpha, string angelGamma, string cononLength, string initalizingVelocity, string initalizingHeight)
            :this(Convert.ToDouble(angelAlpha), Convert.ToDouble(angelGamma), Convert.ToDouble(cononLength), Convert.ToDouble(initalizingVelocity), Convert.ToDouble(initalizingHeight))
        { }
        public Position DisplacementEquation(double timeSpan)
        {
            double v0 = InitalizingVelocity;
            double y0 = InitalizingHeight;
            return new Position(
                v0 * cosX * timeSpan,
                y0 + CononLength * cosY + v0 * cosY * timeSpan - 0.5 * g * timeSpan * timeSpan,
                v0 * cosZ * timeSpan);
        }
    }

    public class Position : Tuple<double, double, double>
    {
        public Position(double x,double y,double z) : base(x, y, z) { }
    }
}

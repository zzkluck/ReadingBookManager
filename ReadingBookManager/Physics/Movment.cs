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

        #region field
        private double angleAlpha;
        private double angleGamma;
        private double initalizingVelocity;
        private double initalizingHeight;
        private double cononLength;
        public double cosX, cosY, cosZ;
        #endregion
        #region Property
        public double AngleAlpha
        {
            get
            {
                return angleAlpha;
            }

            set
            {
                if (value < 0 || value > 180)
                    throw new ArgumentOutOfRangeException("AngleAlpha", "角度数应在0~180之间");
                angleAlpha = value;
            }
        }

        public double AngleGamma
        {
            get
            {
                return angleGamma;
            }

            set
            {
                if (value < -90 || value > 90)
                    throw new ArgumentOutOfRangeException("AngleGamma", "角度数应在-90~0之间");
                angleGamma = value;
            }
        }

        public double InitalizingVelocity
        {
            get
            {
                return initalizingVelocity;
            }

            set
            {
                if (value < 0 || value > 76000)
                    throw new ArgumentOutOfRangeException("InitalizingVelocity", "速度值应当大于0且不应过大");
                initalizingVelocity = value;
            }
        }

        public double InitalizingHeight
        {
            get
            {
                return initalizingHeight;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("InitalizingHeight");
                initalizingHeight = value;
            }
        }

        public double CononLength
        {
            get
            {
                return cononLength;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("CononLength");
                cononLength = value;
            }
        }
        #endregion
        public MovmentCanon
            (double angleAlpha, double angleGamma, double cononLength, double initalizingVelocity, double initalizingHeight)
        {
            AngleAlpha = angleAlpha;
            AngleGamma = angleGamma;
            CononLength = cononLength;
            InitalizingHeight = initalizingHeight;
            InitalizingVelocity = initalizingVelocity;
            cosX = Math.Sin(angleToRadin(angleAlpha)) * Math.Cos(angleToRadin(angleGamma));
            cosY = Math.Cos(angleToRadin(angleAlpha));
            cosZ = Math.Sin(angleToRadin(angleAlpha)) * Math.Sin(angleToRadin(angleGamma));
        }
        private static double angleToRadin(double angleValue)
        {
            return angleValue * Math.PI / 180;
        }

        public MovmentCanon
    (string angelAlpha, string angelGamma, string cononLength, string initalizingVelocity, string initalizingHeight)
            : this(Convert.ToDouble(angelAlpha), Convert.ToDouble(angelGamma), Convert.ToDouble(cononLength), Convert.ToDouble(initalizingVelocity), Convert.ToDouble(initalizingHeight))
        { }
        public Position DisplacementEquation(double timeSpan)
        {
            double v0 = InitalizingVelocity;
            double x0 = CononLength * cosX;
            double y0 = InitalizingHeight + CononLength * cosY;
            double z0 = CononLength * cosZ;
            return new Position(
                x0 + v0 * cosX * timeSpan,
                y0 + v0 * cosY * timeSpan - 0.5 * g * timeSpan * timeSpan,
                z0 + v0 * cosZ * timeSpan);
        }
    }

    public class Position : Tuple<double, double, double>
    {
        public Position(double x, double y, double z) : base(x, y, z) { }
    }
}

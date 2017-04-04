using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingBookManager.Physics
{
    public class MovmentCanon
    {
        static double g = 9.8;
        double angelAlpha, angelGamma;
        double InitalizingVelocity;
        double InitalizingHeight;
        double CononLength;
        private Position DisplacementEquation(double timeSpan)
        {
            double v0 = InitalizingVelocity;
            double y0 = InitalizingHeight;
            throw new NotImplementedException();
        }
    }

    public class Position : Tuple<double, double, double>
    {
        public Position(double x,double y,double z) : base(x, y, z) { }
    }
}

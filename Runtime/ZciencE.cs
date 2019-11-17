using System.Collections;
using System.Collections.Generic;
using System;


namespace Excessives.ZciencE
{

    public static class ZciencE
    {
        #region Special Relativity

        public static double GammaMod(double velocity, double speedOfLight = 3 * (10 ^ 8))
        {
            return (Math.Sqrt(1 - ((velocity * velocity) / (speedOfLight * speedOfLight))));
        }

        public static double LengthContraction(
            double originalLength,
            double velocity,
            double speedOfLight = 3 * (10 ^ 8))
        {
            return (originalLength * GammaMod(velocity, speedOfLight));
        }

        public static double MassDilation(
            double originalMass,
            double velocity,
            double speedOfLight = 3 * (10 ^ 8))
        {
            return (originalMass / GammaMod(velocity, speedOfLight));
        }

        public static double TimeDilation(
            double originalTime,
            double velocity,
            double speedOfLight = 3 * (10 ^ 8))
        {
            return (originalTime / GammaMod(velocity, speedOfLight));
        }

        #endregion
    }
}
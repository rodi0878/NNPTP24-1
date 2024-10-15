using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    internal class ArgumentParser
    {
        public static int[] ParseIntArguments(string[] args)
        {
            int[] intArgs = new int[2];

            for (int i = 0; i < intArgs.Length; i++)
            {
                intArgs[i] = int.Parse(args[i]);
            }
            return intArgs;
        }

        public static double[] ParseDoubleArguments(string[] args)
        {
            double[] doubleArgs = new double[4];

            for (int i = 0; i < doubleArgs.Length; i++)
            {
                doubleArgs[i] = double.Parse(args[i + 2]);
            }
            return doubleArgs;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int[] intArgs = ArgumentParser.ParseIntArguments(args);
            double[] doubleArgs = ArgumentParser.ParseDoubleArguments(args);

            int imageWidth = intArgs[0];
            int imageHeight = intArgs[1];
            double minX = doubleArgs[0];
            double maxX = doubleArgs[1];
            double minY = doubleArgs[2];
            double maxY = doubleArgs[3];
            string output = args[6];

            FractalGenerator fractalGenerator = new FractalGenerator(imageWidth, imageHeight, minX, maxX, minY, maxY, output);
            fractalGenerator.GenerateFractal(); 
        }
    }
}

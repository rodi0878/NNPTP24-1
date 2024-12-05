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
using NNPTPZ1.Mathematics;
using System.Runtime.CompilerServices;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {

        static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };


        static void Main(string[] args)
        {
            int[] intargs = ParseIntegerArguments(args);
            double[] doubleargs = ParseDoubleArguments(args);
            string output = args[6];

            Bitmap bmp = new Bitmap(intargs[0], intargs[1]);
            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];

            double xstep = (xmax - xmin) / intargs[0];
            double ystep = (ymax - ymin) / intargs[1];

            Polynomial p = CreatePolynomial();
            Polynomial pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            GenerateFractalImage(intargs[0], intargs[1], xmin, ymin, xstep, ystep, p, pd, bmp);

            bmp.Save(output ?? "../../../out.png");
        }




        static int[] ParseIntegerArguments(string[] args)
        {
            int[] intargs = new int[2];
            for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }
            return intargs;
        }

        static double[] ParseDoubleArguments(string[] args)
        {
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            return doubleargs;
        }

        static Polynomial CreatePolynomial()
        {
            Polynomial p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber() { Real = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber() { Real = 1 });
            return p;
        }
        static void GenerateFractalImage(int width, int height, double xmin, double ymin, double xstep, double ystep, Polynomial p, Polynomial pd, Bitmap bmp)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    processPixel(i, j, xmin, ymin, xstep, ystep, p, pd, bmp, roots);
                }
            }
        }

        static void processPixel(int i, int j, double xmin, double ymin, double xstep, double ystep, Polynomial p, Polynomial pd, Bitmap bmp, List<ComplexNumber> roots)
        {
            ComplexNumber currentComplex = CalculateInitialComplexNumber(xmin, ymin, xstep, ystep, i, j);
            int iterations = PerformNewtonIteration(p, pd, ref currentComplex);
            int rootIndex = GetOrAddRootIndex(roots, currentComplex);
            Color pixelColor = CalculateColor(rootIndex, currentComplex, iterations);
            SetPixelColor(bmp, i, j, pixelColor);
        }

        static ComplexNumber CalculateInitialComplexNumber(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            ComplexNumber ox = new ComplexNumber()
            {
                Real = x,
                Imaginary = (float)(y)
            };

            if (ox.Real == 0)
                ox.Real = 0.0001;
            if (ox.Imaginary == 0)
                ox.Imaginary = 0.0001f;

            return ox;
        }

        static int PerformNewtonIteration(Polynomial p, Polynomial pd, ref ComplexNumber ox)
        {
            int iterations = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                {
                    i--;
                }
                iterations++;
            }
            return iterations;
        }

        static int GetOrAddRootIndex(List<ComplexNumber> roots, ComplexNumber currentComplex)
        {
            for (int w = 0; w < roots.Count; w++)
            {
                if (Math.Pow(currentComplex.Real - roots[w].Real, 2) + Math.Pow(currentComplex.Imaginary - roots[w].Imaginary, 2) <= 0.01)
                {
                    return w;
                }
            }
            roots.Add(currentComplex);
            return roots.Count - 1;
        }

        static Color CalculateColor(int rootIndex, ComplexNumber ox, int iterations)
        {
            Color baseColor = colors[rootIndex % colors.Length];
            return Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - iterations * 2), 255),
                Math.Min(Math.Max(0, baseColor.G - iterations * 2), 255),
                Math.Min(Math.Max(0, baseColor.B - iterations * 2), 255)
                );
        }

        static void SetPixelColor(Bitmap bmp, int i, int j, Color color)
        {
            bmp.SetPixel(j, i, color);
        }

    }

}

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
            List<ComplexNumber> koreny = new List<ComplexNumber>();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ComplexNumber currentComplex = CalculateInitialComplexNumber(xmin, ymin, xstep, ystep, i, j);
                    int iterations = PerformNewtonIteration(p, pd, ref currentComplex);
                    int rootIndex = GetRootIndex(koreny, currentComplex);
                    if (rootIndex == -1)
                    {
                        koreny.Add(currentComplex);
                        rootIndex = koreny.Count;
                    }
                    Color pixelColor = CalculateColor(rootIndex, currentComplex, iterations);
                    bmp.SetPixel(j, i, pixelColor);
                }
            }
        }

        static ComplexNumber CalculateInitialComplexNumber(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            ComplexNumber ox = new ComplexNumber()
            {
                Real = x,
                Imaginari = (float)(y)
            };

            if (ox.Real == 0)
                ox.Real = 0.0001;
            if (ox.Imaginari == 0)
                ox.Imaginari = 0.0001f;

            return ox;
        }

        static int PerformNewtonIteration(Polynomial p, Polynomial pd, ref ComplexNumber ox)
        {
            int iterations = 0;
            for (int q = 0; q < 30; q++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);

                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
                {
                    q--;
                }
                iterations++;
            }
            return iterations;
        }

        static int GetRootIndex(List<ComplexNumber> koreny, ComplexNumber ox)
        {
            for (int w = 0; w < koreny.Count; w++)
            {
                if (Math.Pow(ox.Real - koreny[w].Real, 2) + Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= 0.01)
                {
                    return w;
                }
            }
            return -1;
        }

        static Color CalculateColor(int rootIndex, ComplexNumber ox, int iterations)
        {
            Color vv = colors[rootIndex % colors.Length];
            vv = Color.FromArgb(vv.R, vv.G, vv.B);
            vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - iterations * 2), 255), Math.Min(Math.Max(0, vv.G - iterations * 2), 255), Math.Min(Math.Max(0, vv.B - iterations * 2), 255));
            return vv;
        }

    }

}

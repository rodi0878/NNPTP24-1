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
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;
using System.Diagnostics;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int MaxIterations = 30;
        private const double ConvergenceThreshold = 0.5;
        private const double RootTolerance = 0.01;

        private static Color[] colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };


        private static int width;
        private static int height;
        private static Bitmap image;
        private static double xmin;
        private static double xmax;
        private static double ymin;
        private static double ymax;

        private static double xstep;
        private static double ystep;

        private static string output;


        private static void ParseArguments(string[] args)
        {
            width = int.Parse(args[0]);
            height = int.Parse(args[1]);
            image = new Bitmap(width, height);
            xmin = double.Parse(args[2]);
            xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            ymax = double.Parse(args[5]);

            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;    
        }

        private static Polynomial InitializePolynomial()
        {
            Polynomial p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber { Real = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber { Real = 1 });
            return p;
        }

        private static void ProcessImage()
        {
            Polynomial p = InitializePolynomial();
            Polynomial pd = p.Derive();
            List<ComplexNumber> roots = new List<ComplexNumber>();


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ProcessPixel(x, y, p, pd, roots); 
                }
            }
        }

        private static void ProcessPixel(int x, int y, Polynomial p, Polynomial pd, List<ComplexNumber> roots)
        {
            // find "world" coordinates of pixel
            double worldY = ymin + x * ystep;
            double worldX = xmin + y * xstep;

            ComplexNumber ox = new ComplexNumber()
            {
                Real = worldX,
                Imaginary = worldY
            };

            if (ox.Real == 0)
                ox.Real = 0.0001;
            if (ox.Imaginary == 0)
                ox.Imaginary = 0.0001f;

            //Console.WriteLine(ox);

            // find solution of equation using newton's iteration


            //Console.ReadKey();
            float iterations = EquationSolution(ref ox, ref p, ref pd);

            // find solution root number
            int rootId = FindOrAddRoot(ref ox, ref roots);

            ColorizePixel(rootId, iterations, y, x);

        }

        private static float EquationSolution(ref ComplexNumber ox, ref Polynomial p, ref Polynomial pd)
        {
            float it = 0;
            for (int q = 0; q < MaxIterations; q++)
            {
                var diff = p.Evalueate(ox).Divide(pd.Evalueate(ox));
                ox = ox.Subtract(diff);

                //Console.WriteLine($"{q} {ox} -({diff})");
                if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= ConvergenceThreshold)
                {
                    q--;
                }
                it++;
            }

            return it;
        }

        private static int FindOrAddRoot(ref ComplexNumber ox, ref List<ComplexNumber> roots)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(ox.Real - roots[i].Real, 2) + Math.Pow(ox.Imaginary - roots[i].Imaginary, 2) <= RootTolerance)
                {
                    return i;
                }
            }
            roots.Add(ox);
            return roots.Count - 1;
        }

        private static void ColorizePixel(int id, float it, int y, int x)
        {
            // colorize pixel according to root number
            //int vv = id;
            //int vv = id * 50 + (int)it*5;
            var vv = colors[id % colors.Length];
            vv = Color.FromArgb(vv.R, vv.G, vv.B);
            vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255), Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));
            //vv = Math.Min(Math.Max(0, vv), 255);
            image.SetPixel(y, x, vv);
        }


        static void Main(string[] args)
        {
            ParseArguments(args);

            // for every pixel in image...
            ProcessImage();

            image.Save(output ?? "../../../out.png");
        }
    }
}

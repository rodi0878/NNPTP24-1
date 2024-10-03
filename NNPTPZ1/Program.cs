using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static readonly Color[] colours = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            int[] resolution;
            string output;
            Bitmap bitmapImage;
            double xmin, ymin, xstep, ystep;

            LoadConfiguration(args, out resolution, out output, out bitmapImage, out xmin, out ymin, out xstep, out ystep);

            Polynome p, pd;
            InitializePolynome(out p, out pd);

            //Console.WriteLine(p);
            //Console.WriteLine(pd);

            NewtonFractalAlgorithm(resolution, bitmapImage, xmin, ymin, xstep, ystep, p, pd);

            SaveImage(output, bitmapImage);
        }

        private static void SaveImage(string output, Bitmap bitmapImage)
        {
            bitmapImage.Save(output ?? "../../../out.png");
        }

        private static void NewtonFractalAlgorithm(int[] resolution, Bitmap bitmapImage, double xmin, double ymin, double xstep, double ystep, Polynome p, Polynome pd)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            int maxid = 0;

            for (int i = 0; i < resolution[0]; i++)
            {
                for (int j = 0; j < resolution[1]; j++)
                {
                    ProcessPixelOfImage(bitmapImage, xmin, ymin, xstep, ystep, p, pd, roots, ref maxid, i, j);
                }
            }
        }

        private static void ProcessPixelOfImage(Bitmap bitmapImage, double xmin, double ymin, double xstep, double ystep, Polynome p, Polynome pd, List<ComplexNumber> roots, ref int maxid, int i, int j)
        {
            ComplexNumber ox = CreateCoordinate(xmin, ymin, xstep, ystep, i, j);

            int iterations = FindSolutionOfNewtonIteration(p, pd, ref ox);

            int id = FindSolutionRootNumber(roots, ref maxid, ox);

            Color pixelColor = CreatePixelColor(iterations, id);

            ColorifyPixel(bitmapImage, i, j, pixelColor);
        }

        private static void ColorifyPixel(Bitmap bitmapImage, int i, int j, Color pixelColor)
        {
            bitmapImage.SetPixel(j, i, pixelColor);
        }

        private static Color CreatePixelColor(int iterations, int id)
        {
            const int colorAdjust = 2;
            Color pixelColor = colours[id % colours.Length];
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iterations * colorAdjust), 255),
                Math.Min(Math.Max(0, pixelColor.G - iterations * colorAdjust), 255),
                Math.Min(Math.Max(0, pixelColor.B - iterations * colorAdjust), 255));
            return pixelColor;
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> roots, ref int maxid, ComplexNumber ox)
        {
            const double tolerance = 0.01;
            bool knownRoot = false;
            int id = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(ox.RealPart - roots[i].RealPart, 2) + Math.Pow(ox.ImaginaryPart - roots[i].ImaginaryPart, 2) <= tolerance)
                {
                    knownRoot = true;
                    id = i;
                }
            }

            if (!knownRoot)
            {
                roots.Add(ox);
                id = roots.Count;
                maxid = id + 1;
            }

            return id;
        }

        private static int FindSolutionOfNewtonIteration(Polynome p, Polynome pd, ref ComplexNumber ox)
        {
            const int maxIterations = 30;
            const double tolerance = 0.5;
            int iteration = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                ComplexNumber difference = p.Evaluate(ox).Divide(pd.Evaluate(ox));
                ox = ox.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= tolerance)
                {
                    i--;
                }
                iteration++;
            }

            return iteration;
        }

        private static ComplexNumber CreateCoordinate(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            const double minValue = 0.0001;
            double x = xmin + j * xstep;
            double y = ymin + i * ystep;


            ComplexNumber ox = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (ox.RealPart == 0)
            {
                ox.RealPart = minValue;
            }

            if (ox.ImaginaryPart == 0)
            {
                ox.ImaginaryPart = minValue;
            }
            return ox;
        }

        private static void InitializePolynome(out Polynome p, out Polynome pd)
        {
            p = new Polynome(
                new List<ComplexNumber>
                {
                    new ComplexNumber() { RealPart = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() { RealPart = 1 }
                });

            pd = p.Derive();
        }

        private static void LoadConfiguration(string[] args, out int[] resolution, out string output, out Bitmap bitmapImage, out double xmin, out double ymin, out double xstep, out double ystep)
        {
            resolution = new int[2];
            for (int i = 0; i < resolution.Length; i++)
            {
                resolution[i] = int.Parse(args[i]);
            }
            double[] coordinates = new double[4];
            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = double.Parse(args[i + 2]);
            }
            output = args[6];
            bitmapImage = new Bitmap(resolution[0], resolution[1]);
            xmin = coordinates[0];
            double xmax = coordinates[1];
            ymin = coordinates[2];
            double ymax = coordinates[3];

            xstep = (xmax - xmin) / bitmapImage.Width;
            ystep = (ymax - ymin) / bitmapImage.Height;
        }
    }
}
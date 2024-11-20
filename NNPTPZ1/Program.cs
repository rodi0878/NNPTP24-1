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
        static readonly Color[] Colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            if (args.Length < 7)
            {
                return;
            }
            int[] resolution;
            string output;
            Bitmap bitmapImage;
            double xmin, ymin, xstep, ystep;

            LoadConfiguration(args, out resolution, out output, out bitmapImage, out xmin, out ymin, out xstep, out ystep);

            Polynome polynome, derivedPolynome;
            InitializePolynome(out polynome, out derivedPolynome);
            NewtonFractalAlgorithm(resolution, bitmapImage, xmin, ymin, xstep, ystep, polynome, derivedPolynome);
            SaveImage(output, bitmapImage);
        }

        private static void SaveImage(string output, Bitmap bitmapImage)
        {
            bitmapImage.Save(output ?? "../../../out.png");
        }

        private static void NewtonFractalAlgorithm(int[] resolution, Bitmap bitmapImage, double xmin, double ymin, double xstep, double ystep, Polynome polynome, Polynome derivedPolynome)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            int maxid = 0;
            for (int i = 0; i < resolution[0]; i++)
            {
                for (int j = 0; j < resolution[1]; j++)
                {
                    ProcessPixel(bitmapImage, xmin, ymin, xstep, ystep, polynome, derivedPolynome, roots, ref maxid, i, j);
                }
            }
        }

        private static void ProcessPixel(Bitmap bitmapImage, double xmin, double ymin, double xstep, double ystep, Polynome polynome, Polynome derivedPolynome, List<ComplexNumber> roots, ref int maxid, int i, int j)
        {
            ComplexNumber coordinate = CreateCoordinate(xmin, ymin, xstep, ystep, i, j);

            int iterations = PerformNewtonIterations(polynome, derivedPolynome, ref coordinate);

            int rootIndex = FindSolutionRootNumber(roots, ref maxid, coordinate);

            Color pixelColor = CreatePixelColor(Colors[rootIndex % Colors.Length], iterations);

            ColorifyPixel(bitmapImage, i, j, pixelColor);
        }

        private static void ColorifyPixel(Bitmap bitmapImage, int i, int j, Color pixelColor)
        {
            bitmapImage.SetPixel(j, i, pixelColor);
        }

        private static Color CreatePixelColor(Color baseColor, int iterations)
        {
            const int colorDecay = 2;

            return Color.FromArgb(
                Math.Min(Math.Max(0, baseColor.R - iterations * colorDecay), 255),
                Math.Min(Math.Max(0, baseColor.G - iterations * colorDecay), 255),
                Math.Min(Math.Max(0, baseColor.B - iterations * colorDecay), 255)
            );
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> roots, ref int maxid, ComplexNumber coordinate)
        {
            const double tolerance = 0.01;
            bool knownRoot = false;
            int id = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(coordinate.RealPart - roots[i].RealPart, 2) + Math.Pow(coordinate.ImaginaryPart - roots[i].ImaginaryPart, 2) <= tolerance)
                {
                    knownRoot = true;
                    id = i;
                }
            }

            if (!knownRoot)
            {
                roots.Add(coordinate);
                id = roots.Count;
                maxid = id + 1;
            }

            return id;
        }

        private static int PerformNewtonIterations(Polynome polynome, Polynome derivedPolynome, ref ComplexNumber coordinate)
        {
            const int maxIterations = 30;
            const double tolerance = 0.5;
            int iteration = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                ComplexNumber difference = polynome.Evaluate(coordinate).Divide(derivedPolynome.Evaluate(coordinate));
                coordinate = coordinate.Subtract(difference);

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

            ComplexNumber coordinate = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = y
            };

            if (coordinate.RealPart == 0)
            {
                coordinate.RealPart = minValue;
            }

            if (coordinate.ImaginaryPart == 0)
            {
                coordinate.ImaginaryPart = minValue;
            }
            return coordinate;
        }

        private static void InitializePolynome(out Polynome polynome, out Polynome derivedPolynome)
        {
            polynome = new Polynome(
                new List<ComplexNumber>
                {
                    new ComplexNumber() { RealPart = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() { RealPart = 1 }
                });

            derivedPolynome = polynome.Derive();
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
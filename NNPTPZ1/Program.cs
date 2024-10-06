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
        private const int MAX_ITERATIONS = 30;
        private const double COORDINATES_ZERO = 0.0001;
        private const double ROOT_PROXIMITY_THRESHOLD = 0.01;
        static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            var (width, height, xmin, xmax, ymin, ymax, filename) = ParseCommandLineArguments(args);

            Bitmap bitmap = GenerateNewtonFractal(width, height, xmin, xmax, ymin, ymax);

            SaveImage(bitmap, filename);
        }

        private static Bitmap GenerateNewtonFractal(int width, int height, double xmin, double xmax, double ymin, double ymax)
        {
            Bitmap bitmap = new Bitmap(width, height);
            List<ComplexNumber> roots = new List<ComplexNumber>();

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            // TODO: poly should be parameterised?
            Polynomial polynomial = new Polynomial(new ComplexNumber() { Re = 1 },
                                          ComplexNumber.Zero,
                                          ComplexNumber.Zero,
                                          new ComplexNumber() { Re = 1 });
            Polynomial derivedPolynomial = polynomial.Derive();

            var maxId = 0;
            // for every pixel in image...
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // find "world" coordinates of pixel
                    ComplexNumber coordinates = FindCoordinates(xmin, ymin, xstep, ystep, i, j);

                    // find solution of equation using newton's iteration
                    int iterationCount = FindRootNumber(polynomial, derivedPolynomial, ref coordinates);

                    // find solution root number
                    int rootId = FindSolutionForRootNumber(roots, ref maxId, coordinates);

                    // colorize pixel according to root number
                    ColorizePixel(bitmap, i, j, iterationCount, rootId);
                }
            }

            return bitmap;
        }

        private static (int width, int height, double xmin, double xmax, double ymin, double ymax, string filename) ParseCommandLineArguments(string[] args)
        {
            if (args.Length < 7)
            {
                throw new ArgumentException("Insufficient arguments provided. Expected: width height xmin xmax ymin ymax filename");
            }

            return (
                int.Parse(args[0]),
                int.Parse(args[1]),
                double.Parse(args[2]),
                double.Parse(args[3]),
                double.Parse(args[4]),
                double.Parse(args[5]),
                args[6]
            );
        }

        private static void SaveImage(Bitmap bmp, string filename)
        {
            bmp.Save(filename ?? "../../../out.png");
        }

        private static ComplexNumber FindCoordinates(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            ComplexNumber coordinates = new ComplexNumber()
            {
                Re = x,
                Im = y
            };

            if (coordinates.Re == 0)
                coordinates.Re = COORDINATES_ZERO;
            if (coordinates.Im == 0)
                coordinates.Im = COORDINATES_ZERO;
            return coordinates;
        }

        private static void ColorizePixel(Bitmap bitmap, int i, int j, int iterationCount, int rootId)
        {
            Color selectedColor = colors[rootId % colors.Length];
            selectedColor = Color.FromArgb(selectedColor.R, selectedColor.G, selectedColor.B);
            selectedColor = Color.FromArgb(Math.Min(Math.Max(0, selectedColor.R - iterationCount * 2), 255),
                                           Math.Min(Math.Max(0, selectedColor.G - iterationCount * 2), 255),
                                           Math.Min(Math.Max(0, selectedColor.B - iterationCount * 2), 255));

            bitmap.SetPixel(j, i, selectedColor);
        }

        private static int FindRootNumber(Polynomial polynomial, Polynomial derivedPolynomial, ref ComplexNumber coordinates)
        {
            int iterationCount = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                ComplexNumber diff = polynomial.Eval(coordinates).Divide(derivedPolynomial.Eval(coordinates));
                coordinates = coordinates.Subtract(diff);

                if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Im, 2) >= 0.5)
                {
                    i--;
                }
                iterationCount++;
            }

            return iterationCount;
        }

        private static int FindSolutionForRootNumber(List<ComplexNumber> roots, ref int maxid, ComplexNumber coordinates)
        {
            Boolean known = false;
            int rootId = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(coordinates.Re - roots[i].Re, 2) + Math.Pow(coordinates.Im - roots[i].Im, 2) <= ROOT_PROXIMITY_THRESHOLD)
                {
                    known = true;
                    rootId = i;
                }
            }
            if (!known)
            {
                roots.Add(coordinates);
                rootId = roots.Count;
                maxid = rootId + 1;
            }

            return rootId;
        }
    }
}

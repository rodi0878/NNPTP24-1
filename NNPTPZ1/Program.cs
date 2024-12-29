using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int MaxIterations = 30;
        private const double Tolerance = 0.5;
        private const double ZeroThreshold = 0.0001;

        private static readonly Color[] Colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Usage: width height xMin xMax yMin yMax outputFile");
                return;
            }

            Bitmap image = CreateBitmap(args);
            double[] worldCoordinates = ParseWorldCoordinates(args);
            string outputFilePath = args[6];

            double xStep = (worldCoordinates[1] - worldCoordinates[0]) / image.Width;
            double yStep = (worldCoordinates[3] - worldCoordinates[2]) / image.Height;

            Polynome polynomial = CreateDefaultPolynomial();
            Polynome derivedPolynomial = polynomial.Derive();

            Console.WriteLine("Polynomial: " + polynomial);
            Console.WriteLine("Derived: " + derivedPolynomial);

            List<ComplexNumber> roots = new List<ComplexNumber>();

            GenerateFractal(image, worldCoordinates, xStep, yStep, polynomial, derivedPolynomial, roots);

            image.Save(outputFilePath);
        }

        private static Bitmap CreateBitmap(string[] args)
        {
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            return new Bitmap(width, height);
        }

        private static double[] ParseWorldCoordinates(string[] args)
        {
            return new[]
            {
                double.Parse(args[2]),
                double.Parse(args[3]),
                double.Parse(args[4]),
                double.Parse(args[5])
            };
        }

        private static Polynome CreateDefaultPolynomial()
        {
            Polynome polynomial = new Polynome();
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber { Real = 1 });
            return polynomial;
        }

        private static void GenerateFractal(Bitmap image, double[] worldCoords, double xStep, double yStep, Polynome polynomial, Polynome derivedPolynomial, List<ComplexNumber> roots)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    ComplexNumber point = GetWorldCoordinate(worldCoords, xStep, yStep, x, y);
                    int iterations = ApplyNewtonMethod(polynomial, derivedPolynomial, ref point);
                    int rootId = IdentifyRoot(roots, point);
                    ColorizePixel(image, x, y, rootId, iterations);
                }
            }
        }

        private static ComplexNumber GetWorldCoordinate(double[] worldCoords, double xStep, double yStep, int pixelX, int pixelY)
        {
            double real = worldCoords[0] + pixelX * xStep;
            double imaginary = worldCoords[2] + pixelY * yStep;

            return new ComplexNumber
            {
                Real = Math.Abs(real) < ZeroThreshold ? ZeroThreshold : real,
                Imaginary = Math.Abs(imaginary) < ZeroThreshold ? ZeroThreshold : imaginary
            };
        }

        private static int ApplyNewtonMethod(Polynome polynomial, Polynome derivedPolynomial, ref ComplexNumber point)
        {
            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                ComplexNumber delta = polynomial.Evaluate(point).Divide(derivedPolynomial.Evaluate(point));
                point = point.Subtract(delta);

                if (delta.GetMagnitudeSquared() < Tolerance)
                {
                    return iteration;
                }
            }
            return MaxIterations;
        }

        private static int IdentifyRoot(List<ComplexNumber> roots, ComplexNumber point)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (roots[i].IsCloseTo(point))
                {
                    return i;
                }
            }

            roots.Add(point);
            return roots.Count - 1;
        }

        private static void ColorizePixel(Bitmap image, int x, int y, int rootId, int iterations)
        {
            Color baseColor = Colors[rootId % Colors.Length];
            int attenuation = Math.Min(255, iterations * 8);
            Color pixelColor = Color.FromArgb(
                Math.Max(0, baseColor.R - attenuation),
                Math.Max(0, baseColor.G - attenuation),
                Math.Max(0, baseColor.B - attenuation)
            );

            image.SetPixel(x, y, pixelColor);
        }
    }
}

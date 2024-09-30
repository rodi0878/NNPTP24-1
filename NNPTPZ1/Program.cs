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
        private const int MaxIterations = 30;
        private const double Epsilon = 0.01;
        private const double Threshold = 0.5;

        private static readonly Color[] Colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow,
            Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            int[] dimensions = ParseDimensions(args);
            double[] bounds = ParseBounds(args);

            string outputPath = args.Length > 6 ? args[6] : "../../../out.png";

            Bitmap bitmap = GenerateFractal(dimensions, bounds);

            bitmap.Save(outputPath);
        }
        private static int[] ParseDimensions(string[] args)
        {
            return new int[]
            {
                int.Parse(args[0]),
                int.Parse(args[1])
            };
        }

        private static double[] ParseBounds(string[] args)
        {
            return new double[]
            {
                double.Parse(args[2]),
                double.Parse(args[3]),
                double.Parse(args[4]),
                double.Parse(args[5])
            };
        }
        private static Bitmap GenerateFractal(int[] dimensions, double[] bounds)
        {
            Bitmap bitmap = new Bitmap(dimensions[0], dimensions[1]);
            double xMin = bounds[0], xMax = bounds[1], yMin = bounds[2], yMax = bounds[3];

            double xStep = (xMax - xMin) / dimensions[0];
            double yStep = (yMax - yMin) / dimensions[1];

            Polynomial polynomial = CreatePolynomial();
            Polynomial polynomialDerivative = polynomial.Derive();

            List<ComplexNumber> roots = new List<ComplexNumber>();

            for (int i = 0; i < dimensions[0]; i++)
            {
                for (int j = 0; j < dimensions[1]; j++)
                {
                    double x = xMin + j * xStep;
                    double y = yMin + i * yStep;

                    ComplexNumber currentGuess = new ComplexNumber()
                    {
                        RealPart = x != 0 ? x : 0.0001,
                        ImaginaryPart = y != 0 ? y : 0.0001
                    };

                    int rootIndex = FindRootIndex(currentGuess, polynomial, polynomialDerivative, roots, out int iterations);
                    Color pixelColor = CalculateColor(rootIndex, iterations);
                    bitmap.SetPixel(j, i, pixelColor);
                }
            }
            return bitmap;
        }
        private static Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            return polynomial;
        }
        private static int FindRootIndex(ComplexNumber currentGuess, Polynomial polynomial, Polynomial polynomialDerivative, List<ComplexNumber> roots, out int iterations)
        {
            iterations = 0;
            for (int i = 0; i < MaxIterations; i++)
            {
                ComplexNumber difference = polynomial.Eval(currentGuess).Divide(polynomialDerivative.Eval(currentGuess));
                currentGuess = currentGuess.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= Threshold)
                {
                    i--;
                }
                iterations++;
            }

            for (int i = 0; i < roots.Count; i++)
            {
                if (DistanceSquared(currentGuess, roots[i]) <= Epsilon)
                {
                    return i;
                }
            }
            roots.Add(currentGuess);
            return roots.Count;
        }

        private static double DistanceSquared(ComplexNumber a, ComplexNumber b)
        {
            return Math.Pow(a.RealPart - b.RealPart, 2) + Math.Pow(a.ImaginaryPart - b.ImaginaryPart, 2);
        }

        private static Color CalculateColor(int rootIndex, int iterations)
        {
            Color pixelColor = Colors[rootIndex % Colors.Length];
            pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
            pixelColor = Color.FromArgb(Math.Min(Math.Max(0, pixelColor.R - iterations * 2), 255), Math.Min(Math.Max(0, pixelColor.G - iterations * 2), 255), Math.Min(Math.Max(0, pixelColor.B - iterations * 2), 255));
            return pixelColor;
        }
    }
}

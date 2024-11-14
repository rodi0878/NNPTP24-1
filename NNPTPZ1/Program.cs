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
        static void Main(string[] args)
        {
            var imageSize = ParseSize(args);
            var range = ParseRange(args);
            string outputFile = args[6];

            Bitmap fractalImage = GenerateImageWithNewtonFractal(imageSize, range);
            fractalImage.Save(outputFile);
        }
        static (int width, int height) ParseSize(string[] args)
        {
            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            return (width, height);
        }
        static (double xmin, double xmax, double ymin, double ymax) ParseRange(string[] args)
        {
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            return (xmin, xmax, ymin, ymax);
        }
        static Bitmap GenerateImageWithNewtonFractal((int width, int height) size, (double xmin, double xmax, double ymin, double ymax) range)
        {
            var (width, height) = size;
            var (xmin, xmax, ymin, ymax) = range;
            Bitmap bmp = new Bitmap(width, height);
            double xStep = (xmax - xmin) / width;
            double yStep = (ymax - ymin) / height;
            Color[] colors = new Color[]
            {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };
            Polynomial polynomial = DefinePolynomial();
            Polynomial polynomialDerivative = polynomial.Derive();
            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivative);
            List<ComplexNumber> roots = new List<ComplexNumber>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ComplexNumber initialGuess = MapPixelToComplexPlane(x, y, xmin, ymin, xStep, yStep);
                    (int rootIndex, int iterations) = FindRootIndex(initialGuess, polynomial, polynomialDerivative, roots);
                    Color pixelColor = CalculatePixelColor(colors, rootIndex, iterations);
                    bmp.SetPixel(x, y, pixelColor);
                }
            }

            return bmp;

        }
        static Polynomial DefinePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coeficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coeficients.Add(ComplexNumber.Zero);
            polynomial.Coeficients.Add(ComplexNumber.Zero);
            polynomial.Coeficients.Add(new ComplexNumber() { RealPart = 1 });
            return polynomial;
        }
        static ComplexNumber MapPixelToComplexPlane(int x, int y, double xmin, double ymin, double xStep, double yStep)
        {
            double real = xmin + x * xStep;
            double imaginary = ymin + y * yStep;
            return new ComplexNumber()
            {
                RealPart = real == 0 ? 0.0001 : real,
                Imaginari = imaginary == 0 ? 0.0001 : imaginary
            };
        }
        static (int rootIndex, int iterationCount) FindRootIndex(ComplexNumber initialGuess, Polynomial polynomial, Polynomial polynomialDerivative, List<ComplexNumber> roots)
        {
            const double tolerance = 0.5;
            const int maxIterations = 30;
            ComplexNumber currentGuess = initialGuess;
            int iterationCount = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                ComplexNumber diff = polynomial.Eval(currentGuess).Divide(polynomialDerivative.Eval(currentGuess));
                currentGuess = currentGuess.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.Imaginari, 2) >= tolerance)
                {
                    i--;
                }
                iterationCount++;
            }

            int rootIndex = GetRootIndexOrAddNew(roots, currentGuess);
            return (rootIndex, iterationCount);
        }
        static int GetRootIndexOrAddNew(List<ComplexNumber> roots, ComplexNumber guess)
        {
            const double tolerance = 0.01;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(guess.RealPart - roots[i].RealPart, 2) + Math.Pow(guess.Imaginari - roots[i].Imaginari, 2) <= tolerance)
                {
                    return i;
                }
            }

            roots.Add(guess);
            return roots.Count - 1;
        }
        static Color CalculatePixelColor(Color[] colors, int rootIndex, float iterations)
        {
            Color color = colors[rootIndex % colors.Length];
            return Color.FromArgb(
                Math.Min(Math.Max(0, color.R - (int)iterations * 2), 255),
                Math.Min(Math.Max(0, color.G - (int)iterations * 2), 255),
                Math.Min(Math.Max(0, color.B - (int)iterations * 2), 255)
            );
        }
    }
}

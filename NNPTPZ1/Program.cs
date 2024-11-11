using NNPTPZ1.Mathematics;
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
        private const int MAX_ITERATIONS = 30;
        private const double TOLERANCE = 0.5;
        private const double ZERO_COORDINATES = 0.0001;

        private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            int[] imagineDimensions = ReadImagineDimensions(args);
            double[] worldCoordinates = ReadWorldCoordinates(args);
            string outputFile = args[6];

            Bitmap bitmap = new Bitmap(imagineDimensions[0], imagineDimensions[1]);
            double xStep = (worldCoordinates[1] - worldCoordinates[0]) / imagineDimensions[0];
            double yStep = (worldCoordinates[3] - worldCoordinates[2]) / imagineDimensions[1];

            Polynomial polynomial = createPolynomial();
            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivedPolynomial);

            List<ComplexNumber> roots = new List<ComplexNumber>();
            int maxid = 0;
            maxid = GenerateFractal(imagineDimensions, worldCoordinates, bitmap, xStep, yStep, polynomial, derivedPolynomial, roots, maxid);

            bitmap.Save(outputFile ?? "../../../out.png");
        }

        private static int GenerateFractal(int[] imagineDimensions, double[] worldCoordinates, Bitmap bitmap, double xStep, double yStep, Polynomial polynomial, Polynomial derivedPolynomial, List<ComplexNumber> roots, int maxid)
        {
            for (int i = 0; i < imagineDimensions[0]; i++)
            {
                for (int j = 0; j < imagineDimensions[1]; j++)
                {
                    ComplexNumber worldCoordinateForPixel = GetWorldCoordinatesForPixel(worldCoordinates, xStep, yStep, i, j);
                    float iterationCount = IterateNewton(polynomial, derivedPolynomial, ref worldCoordinateForPixel);

                    int rootId = FindRootId(roots, ref maxid, worldCoordinateForPixel);

                    var color = colors[rootId % colors.Length];
                    color = Color.FromArgb(color.R, color.G, color.B);
                    color = Color.FromArgb(
                        Math.Min(Math.Max(0, color.R - (int)iterationCount * 2), 255),
                        Math.Min(Math.Max(0, color.G - (int)iterationCount * 2), 255),
                        Math.Min(Math.Max(0, color.B - (int)iterationCount * 2), 255)
                        );
                    bitmap.SetPixel(j, i, color);
                }
            }

            return maxid;
        }

        private static int FindRootId(List<ComplexNumber> roots, ref int maxid, ComplexNumber worldCoordinateForPixel)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(worldCoordinateForPixel.Real - roots[i].Real, 2) + Math.Pow(worldCoordinateForPixel.Imaginary - roots[i].Imaginary, 2) <= 0.01)
                {
                    return i;
                }
            }
            roots.Add(worldCoordinateForPixel);
            int rootId = roots.Count;
            maxid = rootId + 1;


            return rootId;
        }

        private static float IterateNewton(Polynomial polynomial, Polynomial derivedPolynomial, ref ComplexNumber worldCoordinateForPixel)
        {
            float iterationCount = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                var differential = polynomial.Evaluate(worldCoordinateForPixel).Divide(derivedPolynomial.Evaluate(worldCoordinateForPixel));
                worldCoordinateForPixel = worldCoordinateForPixel.Subtract(differential);
                if (Math.Pow(differential.Real, 2) + Math.Pow(differential.Imaginary, 2) >= TOLERANCE)
                {
                    i--;
                }
                iterationCount++;
            }

            return iterationCount;
        }

        private static ComplexNumber GetWorldCoordinatesForPixel(double[] worldCoordinates, double xStep, double yStep, int i, int j)
        {
            double y = worldCoordinates[2] + i * yStep;
            double x = worldCoordinates[0] + j * xStep;
            ComplexNumber complexNumber = new ComplexNumber()
            {
                Real = x,
                Imaginary = y
            };

            if (complexNumber.Real == 0)
                complexNumber.Real = ZERO_COORDINATES;
            if (complexNumber.Imaginary == 0)
                complexNumber.Imaginary = ZERO_COORDINATES;
            return complexNumber;
        }

        private static Polynomial createPolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            return polynomial;
        }

        private static double[] ReadWorldCoordinates(string[] args)
        {
            double[] coordinates = {
                double.Parse(args[2]),
                double.Parse(args[3]),
                double.Parse(args[4]),
                double.Parse(args[5])
            };

            return coordinates;
        }

        private static int[] ReadImagineDimensions(string[] args)
        {
            int[] dimensions = { int.Parse(args[0]), int.Parse(args[1]) };

            return dimensions;
        }
    }
}

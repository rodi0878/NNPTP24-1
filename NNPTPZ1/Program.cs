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
    /// 
    class Program
    {
        private const double COORDINATE_MIN_VALUE = 0.0001;
        private const int MAX_ITERATION = 30;
        private const double ITERATION_TOLERANCE = 0.5;
        private const double ROOT_TOLERANCE = 0.01;
        private static readonly Color[] colors = 
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, 
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private static int width;
        private static int height;

        private static double xMin;
        private static double xMax;
        private static double yMin;
        private static double yMax;

        private static string outputPath;

        private static double xStep;
        private static double yStep;

        private static Bitmap bitmap;

        static void Main(string[] args)
        {
            LoadConfiguration(args);

            GenerateNewtonFractal();

            SaveImage();
        }

        private static void GenerateNewtonFractal()
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynomial polynomial = CreatePolynomial();

            Polynomial polynomialDerivative = polynomial.Derive();

            int maxId = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ProcessPixelOfImage(roots, polynomial, polynomialDerivative, ref maxId, i, j);
                }
            }
        }

        private static void ProcessPixelOfImage(List<ComplexNumber> roots, Polynomial polynomial, Polynomial polynomialDerivative, ref int maxId, int i, int j)
        {
            // find "world" coordinates of pixel
            ComplexNumber worldCoordinate = CreateWorldCoordinate(i, j);

            // find solution of equation using newton's iteration
            int iteration = FindSolutionOfNewtonIteration(polynomial, polynomialDerivative, ref worldCoordinate);

            // find solution root number
            int rootNumber = FindSolutionRootNumber(roots, ref maxId, worldCoordinate);

            Color pixelColor = CalculateColor(iteration, rootNumber);

            // colorize pixel according to root number
            SetBitmapPixel(i, j, pixelColor);
        }

        private static int FindSolutionOfNewtonIteration(Polynomial polynomial, Polynomial polynomialDerivative, ref ComplexNumber worldCoordinate)
        {
            int iteration = 0;
            for (int i = 0; i < MAX_ITERATION; i++)
            {
                ComplexNumber difference = polynomial.Evaluate(worldCoordinate).Divide(polynomialDerivative.Evaluate(worldCoordinate));

                worldCoordinate = worldCoordinate.Subtract(difference);

                if (Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginariPart, 2) >= ITERATION_TOLERANCE)
                {
                    i--;
                }
                iteration++;
            }

            return iteration;
        }

        private static ComplexNumber CreateWorldCoordinate(int i, int j)
        {
            double worldY = yMin + i * yStep;
            double worldX = xMin + j * xStep;

            ComplexNumber worldCoordinate = new ComplexNumber()
            {
                RealPart = worldX,
                ImaginariPart = worldY
            };

            if (worldCoordinate.RealPart == 0)
            {
                worldCoordinate.RealPart = COORDINATE_MIN_VALUE;
            }

            if (worldCoordinate.ImaginariPart == 0)
            {
                worldCoordinate.ImaginariPart = COORDINATE_MIN_VALUE;
            }

            return worldCoordinate;
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> roots, ref int maxId, ComplexNumber worldCoordinate)
        {
            bool known = false;
            int rootCount = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(worldCoordinate.RealPart - roots[i].RealPart, 2) +
                    Math.Pow(worldCoordinate.ImaginariPart - roots[i].ImaginariPart, 2) <= ROOT_TOLERANCE)
                {
                    known = true;
                    rootCount = i;
                }
            }

            if (!known)
            {
                roots.Add(worldCoordinate);
                rootCount = roots.Count;
                maxId = rootCount + 1;
            }

            return rootCount;
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

        private static void LoadConfiguration(string[] args)
        {
            width = int.Parse(args[0]);
            height = int.Parse(args[1]);

            xMin = double.Parse(args[2]);
            xMax = double.Parse(args[3]);
            yMin = double.Parse(args[4]);
            yMax = double.Parse(args[5]);

            outputPath = args[6];

            xStep = (xMax - xMin) / width;
            yStep = (yMax - yMin) / height;

            bitmap = new Bitmap(width, height);
        }

        private static void SaveImage()
        {
            bitmap.Save(outputPath ?? "../../../out.png");
        }

        private static Color CalculateColor(int iteration, int rootNumber)
        {
            Color pixelColor = colors[rootNumber % colors.Length];
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iteration * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - iteration * 2), 255), 
                Math.Min(Math.Max(0, pixelColor.B - iteration * 2), 255));

            return pixelColor;
        }

        private static void SetBitmapPixel(int i, int j, Color pixelColor)
        {
            bitmap.SetPixel(j, i, pixelColor);
        }
    }
}

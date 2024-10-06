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
    static class Program
    {
        private static readonly Color[] Colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        private static int canvasWidth;
        private static int canvasHeight;
        private static double worldMinWidth;
        private static double worldMaxWidth;
        private static double worldMinHeight;
        private static double worldMaxHeight;

        private static string outputPath;

        private static Polynomial polynomial;
        private static Polynomial polynomialDerivation;
        private static double xStep;
        private static double yStep;
        private static List<ComplexNumber> polynomialRoots;

        static void Main(string[] args)
        {
            ParseArgs(args);
            Init();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivation);

            
            Bitmap outputBmp = GenerateNewtonFractalBitmap();
            outputBmp.Save(outputPath ?? "../../../out.png");
            outputBmp.Dispose();
        }

        private static void ParseArgs(string[] args)
        {
            canvasWidth = int.Parse(args[0]);
            canvasHeight = int.Parse(args[1]);

            worldMinWidth = double.Parse(args[2]);
            worldMaxWidth = double.Parse(args[3]);
            worldMinHeight = double.Parse(args[4]);
            worldMaxHeight = double.Parse(args[5]);

            outputPath = args[6];
        }

        private static void Init()
        {
            polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber { RealPart = 1 });
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber { RealPart = 1 });
            polynomialDerivation = polynomial.Derive();

            polynomialRoots = new List<ComplexNumber>();
            xStep = (worldMaxWidth - worldMinWidth) / canvasWidth;
            yStep = (worldMaxHeight - worldMinHeight) / canvasHeight;
        }

        private static Bitmap GenerateNewtonFractalBitmap()
        {
            Bitmap outputBmp = new Bitmap(canvasWidth, canvasHeight);
            for (int i = 0; i < canvasWidth; i++)
            {
                for (int j = 0; j < canvasHeight; j++)
                {
                    ComputeWorldPixel(outputBmp, i, j);
                }
            }

            return outputBmp;
        }

        private static void ComputeWorldPixel(Bitmap outputBmp, int i, int j)
        {
            var worldCoordinate = GetWorldCoordinate(i, j);

            int newtonsIterationCount = NewtonsIteration(polynomial, polynomialDerivation, ref worldCoordinate);

            int rootIndex = GetKnowPolynomialRootIndex(worldCoordinate);
            if (rootIndex == -1)
            {
                rootIndex = polynomialRoots.Count;
                polynomialRoots.Add(worldCoordinate);
            }

            SetBitmapPixel(outputBmp, i, j, rootIndex, newtonsIterationCount);
        }

        private static ComplexNumber GetWorldCoordinate(int i, int j)
        {
            double worldY = worldMinHeight + i * yStep;
            double worldX = worldMinWidth + j * xStep;

            ComplexNumber worldCoordinate = new ComplexNumber
            {
                RealPart = worldX,
                ImaginaryPart = worldY
            };

            const double coordinateMinValue = 0.0001;
            if (worldCoordinate.RealPart == 0)
                worldCoordinate.RealPart = coordinateMinValue;
            if (worldCoordinate.ImaginaryPart == 0)
                worldCoordinate.ImaginaryPart = coordinateMinValue;

            return worldCoordinate;
        }

        private static int NewtonsIteration(Polynomial polynomial, Polynomial polynomialDerivation, ref ComplexNumber worldCoordinate)
        {
            const int newtonsIterationMaximum = 30;
            const double newtonsIterationTolerance = 0.5;

            int newtonsIterationCount = 0;

            for (int i = 0; i < newtonsIterationMaximum; i++)
            {
                ComplexNumber diff = polynomial
                    .Eval(worldCoordinate)
                    .Divide(polynomialDerivation.Eval(worldCoordinate));

                worldCoordinate = worldCoordinate.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= newtonsIterationTolerance)
                {
                    i--;
                }

                newtonsIterationCount++;
            }

            return newtonsIterationCount;
        }

        private static int GetKnowPolynomialRootIndex(ComplexNumber worldCoordinate)
        {
            // find solution root number
            int rootIndex = -1;
            double rootTolerance = 0.01;
            for (int i = 0; i < polynomialRoots.Count; i++)
            {
                if (Math.Pow(worldCoordinate.RealPart - polynomialRoots[i].RealPart, 2) +
                    Math.Pow(worldCoordinate.ImaginaryPart - polynomialRoots[i].ImaginaryPart, 2) <= rootTolerance)
                {
                    rootIndex = i;
                }
            }

            return rootIndex;
        }

        private static void SetBitmapPixel(Bitmap outputBmp, int i, int j, int rootIndex, int newtonsIterationCount)
        {
            Color pixelColor = Colors[rootIndex % Colors.Length];
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - newtonsIterationCount * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - newtonsIterationCount * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - newtonsIterationCount * 2), 255)
            );

            outputBmp.SetPixel(j, i, pixelColor);
        }
    }
}
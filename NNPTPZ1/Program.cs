using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
	class Program
	{
		/// <summary>
		/// This program should produce Newton fractals.
		/// See more at: https://en.wikipedia.org/wiki/Newton_fractal
		/// </summary>

		private const int MaxIterations = 30;
		private const double ConvergenceThreshold = 0.5;
		private const double RootTolerance = 0.01;

		static void Main(string[] args)
		{
			var imageDimensions = ParseImageDimensions(args);
			var worldCoordinates = ParseWorldCoordinates(args);
			var outputFilePath = args[6];

			var bitmap = new Bitmap(imageDimensions[0], imageDimensions[1]);
			double xStep = CalculateStep(worldCoordinates[0], worldCoordinates[1], imageDimensions[0]);
			double yStep = CalculateStep(worldCoordinates[2], worldCoordinates[3], imageDimensions[1]);

			var polynomial = CreateDefaultPolynomial();
			var derivedPolynomial = polynomial.Derive();

			Console.WriteLine(polynomial);
			Console.WriteLine(derivedPolynomial);

			var colors = GetColorPalette();
			var roots = new List<ComplexNumber>();
			var maxRootId = 0;

			GenerateFractal(bitmap, imageDimensions, worldCoordinates, xStep, yStep, polynomial, derivedPolynomial, colors, roots, ref maxRootId);

			SaveBitmap(bitmap, outputFilePath);
		}

		private static int[] ParseImageDimensions(string[] args)
		{
			return new[] { int.Parse(args[0]), int.Parse(args[1]) };
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

		private static double CalculateStep(double min, double max, int dimension)
		{
			return (max - min) / dimension;
		}

		private static Polynomial CreateDefaultPolynomial()
		{
			var polynomial = new Polynomial();
			polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
			polynomial.Coefficients.Add(ComplexNumber.Zero);
			polynomial.Coefficients.Add(ComplexNumber.Zero);
			polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
			return polynomial;
		}

		private static Color[] GetColorPalette()
		{
			return new[]
			{
				Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
			};
		}

		private static void GenerateFractal(Bitmap bitmap, int[] imageDimensions, double[] worldCoordinates, double xStep, double yStep, Polynomial polynomial, Polynomial derivedPolynomial, Color[] colors, List<ComplexNumber> roots, ref int maxRootId)
		{
			for (int i = 0; i < imageDimensions[0]; i++)
			{
				for (int j = 0; j < imageDimensions[1]; j++)
				{
					var worldCoordinatesForPixel = GetWorldCoordinatesForPixel(i, j, worldCoordinates, xStep, yStep);
					var iterationCount = IterateNewtonMethod(polynomial, derivedPolynomial, ref worldCoordinatesForPixel);

					int rootId = FindOrAddRoot(roots, worldCoordinatesForPixel, ref maxRootId);
					var color = CalculatePixelColor(colors, rootId, iterationCount);

					bitmap.SetPixel(j, i, color);
				}
			}
		}

		private static ComplexNumber GetWorldCoordinatesForPixel(int i, int j, double[] worldCoordinates, double xStep, double yStep)
		{
			double x = worldCoordinates[0] + j * xStep;
			double y = worldCoordinates[2] + i * yStep;
			var complex = new ComplexNumber { Real = x, Imaginary = y };

			if (complex.Real == 0)
				complex.Real = 0.0001;
			if (complex.Imaginary == 0)
				complex.Imaginary = 0.0001f;

			return complex;
		}

		private static float IterateNewtonMethod(Polynomial polynomial, Polynomial derivedPolynomial, ref ComplexNumber complex)
		{
			float iterationCount = 0;

			for (int iteration = 0; iteration < MaxIterations; iteration++)
			{
				var diff = polynomial.Evaluate(complex).Divide(derivedPolynomial.Evaluate(complex));
				complex = complex.Subtract(diff);

				if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= ConvergenceThreshold)
				{
					iteration--;
				}
				iterationCount++;
			}

			return iterationCount;
		}

		private static int FindOrAddRoot(List<ComplexNumber> roots, ComplexNumber complex, ref int maxRootId)
		{
			for (int i = 0; i < roots.Count; i++)
			{
				if (IsRootCloseEnough(roots[i], complex))
				{
					return i;
				}
			}

			roots.Add(complex);
			maxRootId = roots.Count;
			return maxRootId - 1;
		}

		private static bool IsRootCloseEnough(ComplexNumber knownRoot, ComplexNumber newRoot)
		{
			return Math.Pow(newRoot.Real - knownRoot.Real, 2) + Math.Pow(newRoot.Imaginary - knownRoot.Imaginary, 2) <= RootTolerance;
		}

		private static Color CalculatePixelColor(Color[] colors, int rootId, float iterationCount)
		{
			var color = colors[rootId % colors.Length];
			return Color.FromArgb(
				Math.Min(Math.Max(0, color.R - (int)iterationCount * 2), 255),
				Math.Min(Math.Max(0, color.G - (int)iterationCount * 2), 255),
				Math.Min(Math.Max(0, color.B - (int)iterationCount * 2), 255)
			);
		}

		private static void SaveBitmap(Bitmap bitmap, string outputFilePath)
		{
			bitmap.Save(outputFilePath ?? "../../../out.png");
		}
	}
}

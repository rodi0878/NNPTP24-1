using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NNPTPZ1
{
    public class NewtonFractal
    {
        private const string _defaultFilePath = "../../../out.png";

        private const float _minimumCoordinate = 0.0001f;

        private const int _maxNewtonIterations = 30;
        private const double _convergenceThreshold = 0.5;
        private const double _rootEqualityThreshold = 0.01;

        private readonly double _minX;
        private readonly double _minY;

        private readonly double _stepX;
        private readonly double _stepY;

        private readonly Bitmap _newtonFractalBitmap;

        private readonly Color[] _colors = new Color[]
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
        };

        public NewtonFractal(int bitmapWidth, int bitmapHeight,
            double minX, double maxX, double minY, double maxY)
        {
            _minX = minX;
            _minY = minY;

            _stepX = (maxX - minX) / bitmapWidth;
            _stepY = (maxY - minY) / bitmapHeight;

            _newtonFractalBitmap = new Bitmap(bitmapWidth, bitmapHeight);
        }

        /// <summary>
        /// Generates Newton Fractal and saves it to file.
        /// </summary>
        /// <param name="outputFilePath">Path to file for output.</param>
        public void GenerateNewtonFractal(string outputFilePath)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();

            // Initial polynomes.
            Polynome polynome = GetInitialPolynome();
            Polynome polynomeDerived = polynome.Derive();

            Console.WriteLine(polynome);
            Console.WriteLine(polynomeDerived);

            // Traverse Bitmap to calculate and set color of each pixel.
            for (int i = 0; i < _newtonFractalBitmap.Width; i++)
            {
                for (int j = 0; j < _newtonFractalBitmap.Height; j++)
                {
                    // Calculate complex coordinates of current pixel.
                    ComplexNumber complexCoordinates = GetComplexCoordinates(i, j);

                    // Set possible zero coordinates to minimum value.
                    CorrectPossibleZeroCoordinates(complexCoordinates);

                    // Find solution of equation using Newton's iteration.
                    int iterationCount = NewtonIteration(polynome, polynomeDerived, ref complexCoordinates);

                    // Find solution root number.
                    int solutionRootNumber = FindSolution(roots, complexCoordinates);

                    // Get pixel color from root number and set it in the Bitmap.
                    Color pixelColor = GetPixelColor(solutionRootNumber, iterationCount);
                    _newtonFractalBitmap.SetPixel(j, i, pixelColor);
                }
            }

            // Save final bitmap to a file.
            _newtonFractalBitmap.Save(outputFilePath ?? _defaultFilePath);
        }

        private Polynome GetInitialPolynome()
        {
            Polynome initialPolynome = new Polynome();

            initialPolynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            initialPolynome.Coefficients.Add(ComplexNumber.Zero);
            initialPolynome.Coefficients.Add(ComplexNumber.Zero);
            initialPolynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });

            return initialPolynome;
        }

        private ComplexNumber GetComplexCoordinates(int row, int column)
        {
            return new ComplexNumber()
            {
                RealPart = _minX + column * _stepX,
                ImaginaryPart = (float)(_minY + row * _stepY)
            };
        }

        private void CorrectPossibleZeroCoordinates(ComplexNumber complexCoordinates)
        {
            if (complexCoordinates.RealPart == 0)
                complexCoordinates.RealPart = _minimumCoordinate;
            if (complexCoordinates.ImaginaryPart == 0)
                complexCoordinates.ImaginaryPart = _minimumCoordinate;
        }

        private int NewtonIteration(Polynome polynome, Polynome polynomeDerived, ref ComplexNumber complexCoordinates)
        {
            int iterationCount = 0;

            for (int i = 0; i < _maxNewtonIterations; i++)
            {
                ComplexNumber difference = polynome.Evaluate(complexCoordinates)
                    .Divide(polynomeDerived.Evaluate(complexCoordinates));

                complexCoordinates = complexCoordinates.Subtract(difference);

                if (difference.GetMagnitudeSquared() >= _convergenceThreshold)
                    i--;

                iterationCount++;
            }

            return iterationCount;
        }

        private int FindSolution(List<ComplexNumber> roots, ComplexNumber complexCoordinates)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (complexCoordinates.IsWithinDistance(roots[i], _rootEqualityThreshold))
                {
                    return i;
                }
            }

            roots.Add(complexCoordinates);
            return roots.Count;
        }

        private Color GetPixelColor(int solutionRootNumber, int iterationCount)
        {
            Color pixelColor = _colors[solutionRootNumber % _colors.Length];
            pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iterationCount * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - iterationCount * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - iterationCount * 2), 255));

            return pixelColor;
        }

    }
}

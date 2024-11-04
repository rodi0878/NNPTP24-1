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
        private readonly double _maxX;
        private readonly double _minY;
        private readonly double _maxY;

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
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;

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
                    ComplexNumber complexCoordinates = new ComplexNumber()
                    {
                        RealPart = _minX + j * _stepX,
                        ImaginaryPart = (float)(_minY + i * _stepY)
                    };

                    // Set possible zero coordinates to minimum value.
                    CorrectPossibleZeroCoordinates(complexCoordinates);

                    // Find solution of equation using Newton's iteration.
                    int iterationCount = NewtonIteration(polynome, polynomeDerived, ref complexCoordinates);

                    // Find solution root number.
                    var solutionFound = false;
                    var solutionRootNumber = 0;
                    for (int k = 0; k < roots.Count; k++)
                    {
                        if (Math.Pow(complexCoordinates.RealPart - roots[k].RealPart, 2)
                            + Math.Pow(complexCoordinates.ImaginaryPart - roots[k].ImaginaryPart, 2) <= _rootEqualityThreshold)
                        {
                            solutionFound = true;
                            solutionRootNumber = k;
                        }
                    }

                    if (!solutionFound)
                    {
                        roots.Add(complexCoordinates);
                        solutionRootNumber = roots.Count;
                    }

                    // Get pixel color from root number and set it in the Bitmap.
                    var pixelColor = _colors[solutionRootNumber % _colors.Length];
                    pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    pixelColor = Color.FromArgb(
                        Math.Min(Math.Max(0, pixelColor.R - iterationCount * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.G - iterationCount * 2), 255),
                        Math.Min(Math.Max(0, pixelColor.B - iterationCount * 2), 255));

                    _newtonFractalBitmap.SetPixel(j, i, pixelColor);
                }
            }

            // Save final bitmap to file.
            _newtonFractalBitmap.Save(outputFilePath ?? _defaultFilePath);
        }

        private static int NewtonIteration(Polynome polynome, Polynome polynomeDerived, ref ComplexNumber complexCoordinates)
        {
            int iterationCount = 0;

            for (int k = 0; k < _maxNewtonIterations; k++)
            {
                var difference = polynome.Evaluate(complexCoordinates)
                    .Divide(polynomeDerived.Evaluate(complexCoordinates));

                complexCoordinates = complexCoordinates.Subtract(difference);

                var exponentialDifference = Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2);
                if (exponentialDifference >= _convergenceThreshold)
                    k--;

                iterationCount++;
            }

            return iterationCount;
        }

        private static Polynome GetInitialPolynome()
        {
            Polynome initialPolynome = new Polynome();

            initialPolynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            initialPolynome.Coefficients.Add(ComplexNumber.Zero);
            initialPolynome.Coefficients.Add(ComplexNumber.Zero);
            initialPolynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });

            return initialPolynome;
        }

        private static void CorrectPossibleZeroCoordinates(ComplexNumber complexCoordinates)
        {
            if (complexCoordinates.RealPart == 0)
                complexCoordinates.RealPart = _minimumCoordinate;
            if (complexCoordinates.ImaginaryPart == 0)
                complexCoordinates.ImaginaryPart = _minimumCoordinate;
        }
    }
}

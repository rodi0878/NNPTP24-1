using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class FractalGenerator
    {
        private Bitmap bitmap;
        private string outputFileName;
        private int imageWidth;
        private int imageHeight;
        private double minX;
        private double maxX;
        private double minY;
        private double maxY;
        private double xStep;
        private double yStep;
        private Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta };

        public FractalGenerator(int imageWidth, int imageHeight, double minX, double maxX, double minY, double maxY, string outputFileName)
        {
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.outputFileName = outputFileName;
            xStep = (maxX - minX) / imageWidth;
            yStep = (maxY - minY) / imageHeight;
            bitmap = new Bitmap(imageWidth, imageHeight);
        }

        private Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            return polynomial;
        }
        private ComplexNumber CalculateWorldCoordinates(int i, int j)
        {
            double x = minX + j * xStep;
            double y = minY + i * yStep;
            return new ComplexNumber(x, (float)y);
        }

        private ComplexNumber AdjustZeroComplex(ComplexNumber complexNumber)
        {
            if (complexNumber.RealPart == 0)
                complexNumber.RealPart = 0.0001;
            if (complexNumber.ImaginaryPart == 0)
                complexNumber.ImaginaryPart = 0.0001f;

            return complexNumber;
        }

        private bool IsDiverging(ComplexNumber difference) =>
            Math.Pow(difference.RealPart, 2) + Math.Pow(difference.ImaginaryPart, 2) >= 0.5;

        private int NewtonIteration(Polynomial polynomial, Polynomial derivedPolynomial, ref ComplexNumber currentComplexNumber)
        {
            int iterations = 0;

            for (int k = 0; k < 30; k++)
            {
                var difference = polynomial.Eval(currentComplexNumber).Divide(derivedPolynomial.Eval(currentComplexNumber));
                currentComplexNumber = currentComplexNumber.Subtract(difference);

                if (IsDiverging(difference))
                {
                    k--;
                }
                iterations++;
            }

            return iterations;
        }

        private int GetRootId(List<ComplexNumber> roots, ComplexNumber currentComplexNumber, ref int maxRootId)
        {
            bool knownRoot = false;
            int rootId = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(currentComplexNumber.RealPart - roots[i].RealPart, 2) + Math.Pow(currentComplexNumber.ImaginaryPart - roots[i].ImaginaryPart, 2) <= 0.01)
                {
                    knownRoot = true;
                    rootId = i;
                }
            }
            if (!knownRoot)
            {
                roots.Add(currentComplexNumber);
                rootId = roots.Count;
                maxRootId = rootId + 1;
            }

            return rootId;
        }

        private Color GetPixelColor(int rootId, int iterations)
        {
            Color color = colors[rootId % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - iterations * 2), 255), Math.Min(Math.Max(0, color.G - iterations * 2), 255), Math.Min(Math.Max(0, color.B - iterations * 2), 255));
            return color;
        }

        private void ColorizePixel(int i, int j, int rootId, int iterations)
        {
            Color pixelColor = GetPixelColor(rootId, iterations);
            bitmap.SetPixel(j, i, pixelColor);
        }

        private void ProcessImage(Polynomial polynomial, Polynomial derivedPolynomial)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            int maxRootId = 0;

            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    ComplexNumber currentComplexNumber = CalculateWorldCoordinates(i, j);
                    currentComplexNumber = AdjustZeroComplex(currentComplexNumber);
                    int iterations = NewtonIteration(polynomial, derivedPolynomial, ref currentComplexNumber);
                    int rootId = GetRootId(roots, currentComplexNumber, ref maxRootId);
                    ColorizePixel(i, j, rootId, iterations);
                }
            }
        }

        public void GenerateFractal()
        {
            Polynomial polynomial = CreatePolynomial();
            Polynomial derivedPolynomial = polynomial.Derive();
            ProcessImage(polynomial, derivedPolynomial);
            Console.WriteLine(outputFileName);
            bitmap.Save(outputFileName ?? "../../../out.png");
        }
    }
}

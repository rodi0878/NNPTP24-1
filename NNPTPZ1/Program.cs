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
        private const double COORDINATES_ZERO = 0.0001;

        private static readonly Color[] colors =
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
            Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            int width, height;
            double xmin, ymin, xstep, ystep;
            string output;
            Bitmap bitmap;

            LoadConfiguration(out width, out height, out xmin, out ymin, out xstep, out ystep, out output, out bitmap);

            Polynomial p, pd;
            InitializePolynome(out p, out pd);
            GenerateNewtonFractal(width, height, xmin, ymin, xstep, ystep, bitmap, p, pd);
            SaveImage(bitmap, output);
        }

        private static void LoadConfiguration(out int width, out int height, out double xmin, out double ymin, out double xstep, out double ystep, out string output, out Bitmap bitmap)
        {
            width = 300;
            height = 300;
            xmin = -2.0;
            double xmax = 2.0;
            ymin = -2.0;
            double ymax = 2.0;

            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;

            output = "output.png";
            bitmap = new Bitmap(width, height);
        }

        private static void InitializePolynome(out Polynomial p, out Polynomial pd)
        {
            p = new Polynomial(
                new ComplexNumber() { RealNumber = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                new ComplexNumber() { RealNumber = 1 }
            );
            pd = p.Derive();
        }

        private static void GenerateNewtonFractal(int width, int height, double xmin, double ymin, double xstep, double ystep, Bitmap bmp, Polynomial p, Polynomial pd)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();
            int maxid = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ProcessPixelOfImage(xmin, ymin, xstep, ystep, bmp, p, pd, roots, ref maxid, j, i);
                }
            }
        }

        private static int ProcessPixelOfImage(double xmin, double ymin, double xstep, double ystep, Bitmap bmp, Polynomial p, Polynomial pd, List<ComplexNumber> roots, ref int maxid, int i, int j)
        {
            ComplexNumber complexNumber = CreateCoordinate(xmin, ymin, xstep, ystep, i, j);
            int iteration = FindSolutionNewtonIteration(p, pd, ref complexNumber);
            int id = FindSolutionRootNumber(roots, ref maxid, complexNumber);
            Color pixelColor = CreatePixelColor(iteration, id);
            PaintPixel(bmp, i, j, pixelColor);

            return maxid;
        }

        private static ComplexNumber CreateCoordinate(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            double x = xmin + i * xstep;
            double y = ymin + j * ystep;

            ComplexNumber coordinates = new ComplexNumber()
            {
                RealNumber = x,
                ImaginaryNumber = y
            };

            if (coordinates.RealNumber == 0)
                coordinates.RealNumber = COORDINATES_ZERO;
            if (coordinates.ImaginaryNumber == 0)
                coordinates.ImaginaryNumber = COORDINATES_ZERO;
            return coordinates;
        }

        private static int FindSolutionNewtonIteration(Polynomial p, Polynomial pd, ref ComplexNumber coordinates)
        {
            int iteration = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                var diff = p.Evaluate(coordinates).Divide(pd.Evaluate(coordinates));
                coordinates = coordinates.Subtract(diff);

                if (Math.Pow(diff.RealNumber, 2) + Math.Pow(diff.ImaginaryNumber, 2) >= TOLERANCE)
                {
                    i--;
                }
                iteration++;
            }

            return iteration;
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> roots, ref int maxid, ComplexNumber coordinates)
        {
            Boolean known = false;
            int id = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(coordinates.RealNumber - roots[i].RealNumber, 2) + Math.Pow(coordinates.ImaginaryNumber - roots[i].ImaginaryNumber, 2) <= TOLERANCE)
                {
                    known = true;
                    id = i;
                }
            }
            if (!known)
            {
                roots.Add(coordinates);
                id = roots.Count;
                maxid = id + 1;
            }

            return id;
        }

        private static Color CreatePixelColor(int iterations, int id)
        {
            const int colorAdjust = 2;
            Color pixelColor = colors[id % colors.Length];
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - iterations * colorAdjust), 255),
                Math.Min(Math.Max(0, pixelColor.G - iterations * colorAdjust), 255),
                Math.Min(Math.Max(0, pixelColor.B - iterations * colorAdjust), 255));
            return pixelColor;
        }

        private static void PaintPixel(Bitmap bitmap, int i, int j, Color selectedColor)
        {
            bitmap.SetPixel(i, j, selectedColor);
        }

        private static void SaveImage(Bitmap bitmap, string output)
        {
            bitmap.Save(output ?? "../../../out.png");
            Console.WriteLine("Success!");
        }
    }
}

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
        private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        private static readonly List<ComplexNumber> roots = new List<ComplexNumber>();

        private static int width;
        private static int height;
        private static double xmin;
        private static double xmax;
        private static double ymin;
        private static double ymax;
        private static string output;
        private static double xstep;
        private static double ystep;

        private static Bitmap bmp;

        static void Main(string[] args)
        {
            ParseArguments(args);

            ProcessImage();

            SaveImage();
        }

        private static void ParseArguments(string[] args)
        {
            width = int.Parse(args[0]);
            height = int.Parse(args[1]);
            xmin = double.Parse(args[2]);
            xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            ymax = double.Parse(args[5]);
            output = args[6];
            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;
            bmp = new Bitmap(width, height);
        }

        private static void ProcessImage()
        {
            PreparePolynomes(out Polynome polynome, out Polynome derivedPolynome);

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    ProcessPixel(polynome, derivedPolynome, x, y);
                }
            }
        }

        private static void PreparePolynomes(out Polynome polynome, out Polynome derivedPolynome)
        {
            polynome = new Polynome();
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(ComplexNumber.Zero);
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            derivedPolynome = polynome.Derive();
        }

        private static void ProcessPixel(Polynome polynome, Polynome derivedPolynome, int x, int y)
        {
            // find "world" coordinates of pixel
            double worldX = xmin + x * xstep;
            double worldY = ymin + y * ystep;

            ComplexNumber initialGuess = new ComplexNumber()
            {
                RealPart = worldX,
                ImaginaryPart = worldY
            };

            if (initialGuess.RealPart == 0)
                initialGuess.RealPart = 0.0001;
            if (initialGuess.ImaginaryPart == 0)
                initialGuess.ImaginaryPart = 0.0001;

            // find solution of equation using newton's iteration
            int equationSolution = FindEquationSolution(polynome, derivedPolynome, ref initialGuess);

            int rootNumber = FindSolutionRootNumber(roots, initialGuess);

            // colorize pixel according to root number
            Color color = GetPixelColor(equationSolution, rootNumber);
            ColorizePixel(x, y, color);
        }

        private static int FindEquationSolution(Polynome polynome, Polynome derivedPolynome, ref ComplexNumber initialGuess)
        {
            const int maxIterations = 30;
            const double tolerance = 0.5;

            int iterationCount = 0;

            for (int i = 0; i < maxIterations; i++)
            {
                ComplexNumber correction = polynome.Eval(initialGuess).Divide(derivedPolynome.Eval(initialGuess));
                initialGuess = initialGuess.Subtract(correction);

                if (!IsConverged(correction, tolerance))
                {
                    i--;
                }

                iterationCount++;
            }

            return iterationCount;
        }

        private static bool IsConverged(ComplexNumber correction, double tolerance)
        {
            return Math.Pow(correction.RealPart, 2) + Math.Pow(correction.ImaginaryPart, 2) < tolerance;
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> roots, ComplexNumber solution)
        {
            const double threshold = 0.01;

            int rootNumber = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (IsCloseEnough(solution, roots[i], threshold))
                {
                    rootNumber = i;
                    return rootNumber;
                }
            }
            roots.Add(solution);
            rootNumber = roots.Count;
            return rootNumber;
        }

        private static bool IsCloseEnough(ComplexNumber solution, ComplexNumber root, double threshold)
        {
            return Math.Pow(solution.RealPart - root.RealPart, 2) + Math.Pow(solution.ImaginaryPart - root.ImaginaryPart, 2) <= threshold;
        }

        private static void ColorizePixel(int x, int y, Color color)
        {
            bmp.SetPixel(x, y, color);
        }

        private static Color GetPixelColor(int equationSolution, int rootNumber)
        {
            var color = colors[rootNumber % colors.Length];
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - equationSolution * 2), 255), Math.Min(Math.Max(0, color.G - equationSolution * 2), 255), Math.Min(Math.Max(0, color.B - equationSolution * 2), 255));
            return color;
        }

        private static void SaveImage()
        {
            bmp.Save(output ?? "../../../out.png");
        }
    }
}

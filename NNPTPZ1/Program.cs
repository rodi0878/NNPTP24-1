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
        static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

        static void Main(string[] args)
        {
            int width, height;
            double xmin, ymin, xstep, ystep;
            string output;
            Bitmap bmp;

            LoadConfiguration(out width, out height, out xmin, out ymin, out xstep, out ystep, out output, out bmp);

            Polynome p, pd;
            InitializePolynome(out p, out pd);

            NewtonFractalAlgorithm(width, height, xmin, ymin, xstep, ystep, bmp, p, pd);

            Saveimage(output, bmp);
        }

        private static void LoadConfiguration(out int width, out int height, out double xmin, out double ymin, out double xstep, out double ystep, out string output, out Bitmap bmp)
        {
            width = 800;
            height = 800;
            xmin = -2.0;
            double xmax = 2.0;
            ymin = -2.0;
            double ymax = 2.0;

            xstep = (xmax - xmin) / width;
            ystep = (ymax - ymin) / height;
            output = "output.png";
            bmp = new Bitmap(width, height);
        }

        private static void InitializePolynome(out Polynome p, out Polynome pd)
        {
            p = new Polynome(
                new ComplexNumber() { RealNumber = 1 },
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                new ComplexNumber() { RealNumber = 1 }
            );
            Polynome ptmp = p;
            pd = p.Derive();
        }

        private static void NewtonFractalAlgorithm(int width, int height, double xmin, double ymin, double xstep, double ystep, Bitmap bmp, Polynome p, Polynome pd)
        {
            List<ComplexNumber> roots = new List<ComplexNumber>();

            int maxid = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    ProcessPixelOfImage(xmin, ymin, xstep, ystep, bmp, p, pd, roots, ref maxid, i, j);
                }
            }
        }

        private static int ProcessPixelOfImage(double xmin, double ymin, double xstep, double ystep, Bitmap bmp, Polynome p, Polynome pd, List<ComplexNumber> roots, ref int maxid, int i, int j)
        {
            ComplexNumber ox = CreateCoordinate(xmin, ymin, xstep, ystep, i, j);

            float it = FindSolutionNewtonIteration(p, pd, ref ox);

            int id = FindSolutionRootNumber(roots, ref maxid, ox);

            Color vv = CreatePixelColor(it, id);

            ColorifyPixel(bmp, i, j, vv);

            return maxid;
        }

        private static ComplexNumber CreateCoordinate(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            ComplexNumber ox = new ComplexNumber()
            {
                RealNumber = x,
                ImaginaryNumber = (float)(y)
            };

            if (ox.RealNumber == 0)
                ox.RealNumber = 0.0001;
            if (ox.ImaginaryNumber == 0)
                ox.ImaginaryNumber = 0.0001f;
            return ox;
        }

        private static float FindSolutionNewtonIteration(Polynome p, Polynome pd, ref ComplexNumber ox)
        {
            float it = 0;
            int maxIterations = 30;
            for (int i = 0; i < maxIterations; i++)
            {
                var diff = p.Evaluate(ox).Divide(pd.Evaluate(ox));
                ox = ox.Subtract(diff);

                double tolerance = 0.5;
                if (Math.Pow(diff.RealNumber, 2) + Math.Pow(diff.ImaginaryNumber, 2) >= tolerance)
                {
                    i--;
                }
                it++;
            }

            return it;
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> roots, ref int maxid, ComplexNumber ox)
        {
            var known = false;
            var id = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                double tolerance = 0.01;
                if (Math.Pow(ox.RealNumber - roots[i].RealNumber, 2) + Math.Pow(ox.ImaginaryNumber - roots[i].ImaginaryNumber, 2) <= tolerance)
                {
                    known = true;
                    id = i;
                }
            }
            if (!known)
            {
                roots.Add(ox);
                id = roots.Count;
                maxid = id + 1;
            }

            return id;
        }

        private static Color CreatePixelColor(float it, int id)
        {
            var vv = colors[id % colors.Length];
            var adjustedColor = Color.FromArgb(vv.R, vv.G, vv.B);
            adjustedColor = Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255), Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));
            return adjustedColor;
        }

        private static void ColorifyPixel(Bitmap bmp, int i, int j, Color vv)
        {
            bmp.SetPixel(j, i, vv);
        }

        private static void Saveimage(string output, Bitmap bmp)
        {
            bmp.Save(output ?? "../../../out.png");
            Console.WriteLine($"Image saved successfully to {output ?? "../../../out.png"}");

        }

    }
}

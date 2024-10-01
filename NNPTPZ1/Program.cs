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
    class Program
    {
        static readonly Color[] clrs = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        static void Main(string[] args)
        {
            int[] intargs;
            string output;
            Bitmap bmp;
            double xmin, ymin, xstep, ystep;

            LoadConfiguration(args, out intargs, out output, out bmp, out xmin, out ymin, out xstep, out ystep);

            Polynome p, pd;
            InitializePolynome(out p, out pd);

            // Console.WriteLine(p);
            // Console.WriteLine(pd);

            NewtonFractalAlgorithm(intargs, bmp, xmin, ymin, xstep, ystep, p, pd);

            SaveImage(output, bmp);
        }

        private static void SaveImage(string output, Bitmap bmp)
        {
            bmp.Save(output ?? "../../../out.png");
        }

        private static void NewtonFractalAlgorithm(int[] intargs, Bitmap bmp, double xmin, double ymin, double xstep, double ystep, Polynome p, Polynome pd)
        {
            List<ComplexNumber> koreny = new List<ComplexNumber>();
            int maxid = 0;

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < intargs[0]; i++)
            {
                for (int j = 0; j < intargs[1]; j++)
                {
                    ProcessPixelOfImage(bmp, xmin, ymin, xstep, ystep, p, pd, koreny, ref maxid, i, j);
                }
            }
        }

        private static void ProcessPixelOfImage(Bitmap bmp, double xmin, double ymin, double xstep, double ystep, Polynome p, Polynome pd, List<ComplexNumber> koreny, ref int maxid, int i, int j)
        {
            ComplexNumber ox = CreateCoordinate(xmin, ymin, xstep, ystep, i, j);

            float it = FindSolutionOfNewtonIteration(p, pd, ref ox);

            int id = FindSolutionRootNumber(koreny, ref maxid, ox);

            Color vv = CreatePixelColor(it, id);

            ColorifyPixel(bmp, i, j, vv);
        }

        private static void ColorifyPixel(Bitmap bmp, int i, int j, Color vv)
        {
            bmp.SetPixel(j, i, vv);
        }

        private static Color CreatePixelColor(float it, int id)
        {
            var vv = clrs[id % clrs.Length];
            return Color.FromArgb(Math.Min(Math.Max(0, vv.R - (int)it * 2), 255), Math.Min(Math.Max(0, vv.G - (int)it * 2), 255), Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));
        }

        private static int FindSolutionRootNumber(List<ComplexNumber> koreny, ref int maxid, ComplexNumber ox)
        {
            bool known = false;
            int id = 0;
            for (int w = 0; w < koreny.Count; w++)
            {
                if (Math.Pow(ox.RealPart - koreny[w].RealPart, 2) + Math.Pow(ox.ImaginaryPart - koreny[w].ImaginaryPart, 2) <= 0.01)
                {
                    known = true;
                    id = w;
                }
            }
            if (!known)
            {
                koreny.Add(ox);
                id = koreny.Count;
                maxid = id + 1;
            }

            return id;
        }

        private static float FindSolutionOfNewtonIteration(Polynome p, Polynome pd, ref ComplexNumber ox)
        {
            float it = 0;
            for (int q = 0; q < 30; q++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);

                if (Math.Pow(diff.RealPart, 2) + Math.Pow(diff.ImaginaryPart, 2) >= 0.5)
                {
                    q--;
                }
                it++;
            }

            return it;
        }

        private static ComplexNumber CreateCoordinate(double xmin, double ymin, double xstep, double ystep, int i, int j)
        {
            // find "world" coordinates of pixel
            double y = ymin + i * ystep;
            double x = xmin + j * xstep;

            ComplexNumber ox = new ComplexNumber()
            {
                RealPart = x,
                ImaginaryPart = (float)(y)
            };

            if (ox.RealPart == 0)
                ox.RealPart = 0.0001;
            if (ox.ImaginaryPart == 0)
                ox.ImaginaryPart = 0.0001f;
            return ox;
        }

        private static void InitializePolynome(out Polynome p, out Polynome pd)
        {
            // TODO: poly should be parameterised?
            p = new Polynome();
            p.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            //p.Coe.Add(Cplx.Zero);
            p.Coefficients.Add(new ComplexNumber() { RealPart = 1 });
            Polynome ptmp = p;
            pd = p.Derive();
        }

        private static void LoadConfiguration(string[] args, out int[] intargs, out string output, out Bitmap bmp, out double xmin, out double ymin, out double xstep, out double ystep)
        {
            intargs = new int[2];
            for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            output = args[6];
            // TODO: add parameters from args?
            bmp = new Bitmap(intargs[0], intargs[1]);
            xmin = doubleargs[0];
            double xmax = doubleargs[1];
            ymin = doubleargs[2];
            double ymax = doubleargs[3];

            xstep = (xmax - xmin) / intargs[0];
            ystep = (ymax - ymin) / intargs[1];
        }
    }
}
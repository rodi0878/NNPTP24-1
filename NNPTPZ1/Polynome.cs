using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    public class Polynome
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynome()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynome Derive()
        {
            Polynome derivedPolynome = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                var derivedCoefficient = Coefficients[i].Multiply(new ComplexNumber() { RealPart = i });

                derivedPolynome.Coefficients.Add(derivedCoefficient);
            }

            return derivedPolynome;
        }

        /// <summary>
        /// Evaluates polynomial at given point: y = polynome(x)
        /// </summary>
        /// <param name="x">point of evaluation (x)</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber s = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coef = Coefficients[i];
                ComplexNumber bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        bx = bx.Multiply(x);

                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
            }

            return s;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < Coefficients.Count; i++)
            {
                s += Coefficients[i];

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        s += "x";
                    }
                }

                if (i + 1 < Coefficients.Count)
                {
                    s += " + ";
                }
            }

            return s;
        }
    }
}

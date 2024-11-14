using System.Collections.Generic;

namespace NNPTPZ1
{
    public class Polynomial
    {
        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coeficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial()
        {
            Coeficients = new List<ComplexNumber>();
        }

        public void Add(ComplexNumber coe)
        {
            Coeficients.Add(coe);
        }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coeficients.Count; i++)
            {
                polynomial.Coeficients.Add(Coeficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>Evaluated polynomial at given point</returns>
        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { RealPart = x, Imaginari = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>result</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coeficients.Count; i++)
            {
                ComplexNumber coeficient = Coeficients[i];
                ComplexNumber term = x;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        term = term.Multiply(x);
                    }
                    coeficient = coeficient.Multiply(term);
                }

                result = result.Add(coeficient);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string resultString = "";
            for (int i = 0; i < Coeficients.Count; i++)
            {
                resultString += Coeficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        resultString += "x";
                    }
                }
                if (i + 1 < Coeficients.Count)
                {
                    resultString += " + ";
                }          
            }
            return resultString;
        }
    }
}

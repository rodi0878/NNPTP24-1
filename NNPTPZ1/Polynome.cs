using System.Collections.Generic;

namespace NNPTPZ1
{
    public class Polynome
    {
        public List<ComplexNumber> Coefficients { get; private set; } = new List<ComplexNumber>();

        public Polynome Derive()
        {
            Polynome derivative = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivative.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber { Real = i }));
            }
            return derivative;
        }

        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;
            ComplexNumber power = ComplexNumber.One;

            foreach (ComplexNumber coefficient in Coefficients)
            {
                result = result.Add(coefficient.Multiply(power));
                power = power.Multiply(point);
            }

            return result;
        }

        public override string ToString()
        {
            var terms = new List<string>();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (!Coefficients[i].IsZero())
                {
                    terms.Add(Coefficients[i] + (i > 0 ? $"x^{i}" : ""));
                }
            }

            return string.Join(" + ", terms);
        }
    }
}

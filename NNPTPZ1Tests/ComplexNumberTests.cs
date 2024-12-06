using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber
            {
                Re = 10,
                Im = 20
            };
            ComplexNumber b = new ComplexNumber
            {
                Re = 1,
                Im = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber expected = new ComplexNumber
            {
                Re = 11,
                Im = 22
            };

            Assert.AreEqual(expected, actual);

            string expectedString = "(10 + 20i)";
            string actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(1 + 2i)";
            actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);

            a = new ComplexNumber
            {
                Re = 1,
                Im = -1
            };
            b = new ComplexNumber
            {
                Re = 0,
                Im = 0
            };
            expected = new ComplexNumber
            {
                Re = 1,
                Im = -1
            };
            actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            expectedString = "(1 + -1i)";
            actualString = a.ToString();
            Assert.AreEqual(expectedString, actualString);

            expectedString = "(0 + 0i)";
            actualString = b.ToString();
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void AddTestPolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber { Re = 1, Im = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Re = 0, Im = 0 });
            polynomial.Coefficients.Add(new ComplexNumber { Re = 1, Im = 0 });

            ComplexNumber result = polynomial.Evaluate(new ComplexNumber { Re = 0, Im = 0 });
            ComplexNumber expected = new ComplexNumber { Re = 1, Im = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber { Re = 1, Im = 0 });
            expected = new ComplexNumber { Re = 2, Im = 0 };
            Assert.AreEqual(expected, result);

            result = polynomial.Evaluate(new ComplexNumber { Re = 2, Im = 0 });
            expected = new ComplexNumber { Re = 5.0000000000, Im = 0 };
            Assert.AreEqual(expected, result);

            string expectedPolynomialString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            string actualPolynomialString = polynomial.ToString();
            Assert.AreEqual(expectedPolynomialString, actualPolynomialString);
        }
    }
}



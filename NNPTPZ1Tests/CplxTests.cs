using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class CplxTests
    {
        [TestMethod]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber { RealPart = 10, ImaginaryPart = 20 };
            ComplexNumber b = new ComplexNumber { RealPart = 1, ImaginaryPart = 2 };

            ComplexNumber expected = new ComplexNumber { RealPart = 11, ImaginaryPart = 22 };
            ComplexNumber actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            string expectedToString = "(10 + 20i)";
            string actualToString = a.ToString();
            Assert.AreEqual(expectedToString, actualToString);

            expectedToString = "(1 + 2i)";
            actualToString = b.ToString();
            Assert.AreEqual(expectedToString, actualToString);

            a = new ComplexNumber { RealPart = 1, ImaginaryPart = -1 };
            b = new ComplexNumber { RealPart = 0, ImaginaryPart = 0 };

            expected = new ComplexNumber { RealPart = 1, ImaginaryPart = -1 };
            actual = a.Add(b);
            Assert.AreEqual(expected, actual);

            expectedToString = "(1 + -1i)";
            actualToString = a.ToString();
            Assert.AreEqual(expectedToString, actualToString);

            expectedToString = "(0 + 0i)";
            actualToString = b.ToString();
            Assert.AreEqual(expectedToString, actualToString);
        }

        [TestMethod]
        public void AddTestPolynome()
        {
            Polynomial poly = new Polynomial();

            poly.Coefficients.Add(new ComplexNumber { RealPart = 1, ImaginaryPart = 0 });
            poly.Coefficients.Add(new ComplexNumber { RealPart = 0, ImaginaryPart = 0 });
            poly.Coefficients.Add(new ComplexNumber { RealPart = 1, ImaginaryPart = 0 });

            ComplexNumber expected = new ComplexNumber { RealPart = 1, ImaginaryPart = 0 };
            ComplexNumber actual = poly.Eval(new ComplexNumber { RealPart = 0, ImaginaryPart = 0 });
            Assert.AreEqual(expected, actual);

            expected = new ComplexNumber { RealPart = 2, ImaginaryPart = 0 };
            actual = poly.Eval(new ComplexNumber { RealPart = 1, ImaginaryPart = 0 });
            Assert.AreEqual(expected, actual);

            expected = new ComplexNumber { RealPart = 5.0000000000, ImaginaryPart = 0 };
            actual = poly.Eval(new ComplexNumber { RealPart = 2, ImaginaryPart = 0 });
            Assert.AreEqual(expected, actual);

            string expectedToString = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            string actualToString = poly.ToString();
            Assert.AreEqual(expectedToString, actualToString);
        }
    }
}
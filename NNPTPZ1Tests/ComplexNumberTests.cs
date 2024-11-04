using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddComplexNumbersTest1()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };

            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            ComplexNumber result = a.Add(b);
            ComplexNumber expected = new ComplexNumber()
            {
                RealPart = 11,
                ImaginaryPart = 22
            };

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void AddComplexNumbersTest2()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };

            ComplexNumber b = new ComplexNumber()
            {
                RealPart = 0,
                ImaginaryPart = 0
            };

            ComplexNumber result = a.Add(b);
            ComplexNumber expected = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringComplexNumberTest1()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 10,
                ImaginaryPart = 20
            };

            string expected = "(10 + 20i)";
            string result = a.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringComplexNumberTest2()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = 2
            };

            string expected = "(1 + 2i)";
            string result = a.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringComplexNumberTest3()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 1,
                ImaginaryPart = -1
            };

            string expected = "(1 + -1i)";
            string result = a.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringComplexNumberTest4()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealPart = 0,
                ImaginaryPart = 0
            };

            string expected = "(0 + 0i)";
            string result = a.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluatePolynomeTest1()
        {
            Polynome polynome = new Polynome();

            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });

            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            var expected = new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 };

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluatePolynomeTest2()
        {
            Polynome polynome = new Polynome();

            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });

            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            ComplexNumber expected = new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 };

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EvaluatePolynomeTest3()
        {
            Polynome polynome = new Polynome();

            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });

            ComplexNumber result = polynome.Evaluate(new ComplexNumber() { RealPart = 2, ImaginaryPart = 0 });
            ComplexNumber expected = new ComplexNumber() { RealPart = 5, ImaginaryPart = 0 };

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ToStringPolynomeTest()
        {
            Polynome polynome = new Polynome();

            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 0, ImaginaryPart = 0 });
            polynome.Coefficients.Add(new ComplexNumber() { RealPart = 1, ImaginaryPart = 0 });

            string result = polynome.ToString();
            string expected = "(1 + 0i) + (0 + 0i)x^1 + (1 + 0i)x^2";

            Assert.AreEqual(expected, result);
        }
    }
}

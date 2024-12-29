using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void AddTest()
        {
            var a = new ComplexNumber { Real = 10, Imaginary = 20 };
            var b = new ComplexNumber { Real = 1, Imaginary = 2 };

            var actual = a.Add(b);
            var expected = new ComplexNumber { Real = 11, Imaginary = 22 };

            Assert.AreEqual(expected.ToString(), actual.ToString());

            Assert.AreEqual("(10 + 20i)", a.ToString());
            Assert.AreEqual("(1 + 2i)", b.ToString());

            a = new ComplexNumber { Real = 1, Imaginary = -1 };
            b = ComplexNumber.Zero;
            expected = new ComplexNumber { Real = 1, Imaginary = -1 };
            actual = a.Add(b);

            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.AreEqual("(1 + -1i)", a.ToString());
            Assert.AreEqual("(0 + 0i)", b.ToString());
        }

        [TestMethod]
        public void MultiplyTest()
        {
            var a = new ComplexNumber { Real = 2, Imaginary = 3 };
            var b = new ComplexNumber { Real = 1, Imaginary = 4 };

            var actual = a.Multiply(b);
            var expected = new ComplexNumber { Real = -10, Imaginary = 11 };

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void DivideTest()
        {
            var a = new ComplexNumber { Real = 10, Imaginary = 20 };
            var b = new ComplexNumber { Real = 1, Imaginary = 2 };

            var actual = a.Divide(b);
            var expected = new ComplexNumber { Real = 10, Imaginary = 0 }; 

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }


        [TestMethod]
        public void MagnitudeTest()
        {
            var a = new ComplexNumber { Real = 3, Imaginary = 4 };

            var magnitudeSquared = a.GetMagnitudeSquared();
            Assert.AreEqual(25, magnitudeSquared);

            Assert.IsTrue(a.IsCloseTo(new ComplexNumber { Real = 3, Imaginary = 4 }));
            Assert.IsFalse(a.IsCloseTo(new ComplexNumber { Real = 5, Imaginary = 5 }));
        }
    }
}



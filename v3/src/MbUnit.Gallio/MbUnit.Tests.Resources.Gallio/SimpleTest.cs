using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;

namespace MbUnit.TestResources.Gallio
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
        public void Pass()
        {
        }

        [Test]
        public void Fail()
        {
            Assert.Fail("Boom");
        }
    }
}

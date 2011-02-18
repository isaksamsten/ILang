using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILang.Frontend.Tokens;

namespace ILang.Test
{
    [TestClass]
    public class ILangTokenTypeTest
    {
        [TestMethod]
        public void Equal()
        {
            ILangTokenType t1 = "end";
            ILangTokenType t2 = "end";


            Assert.IsTrue(t1 == t2);
        }

        [TestMethod]
        public void NotEqual()
        {
            ILangTokenType t1 = "end";
            ILangTokenType t2 = "error";


            Assert.IsTrue(t1 != t2);
        }

        [TestMethod]
        public void CreateValid()
        {
            try
            {
                ILangTokenType t = "error";
                t = "method";
                t = "class";
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CreateInvalid()
        {
            try
            {
                ILangTokenType t = "dsa";
                t = "dsa";
                t = "clasdsadss";

                Assert.Fail("could be created");
            }
            catch
            {
                
            }
        }
    }
}

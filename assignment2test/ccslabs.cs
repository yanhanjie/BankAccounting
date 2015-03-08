using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ccslabsLogIn.forms;


namespace assignment2test
{
    [TestClass]
    public class ccslabs
    {
        [TestMethod]
        public void frmLogin_UserAuthenticated_WithErrorUsernameAndPassword()
        {   
            //arrange
            string p = "abc";
            string p_2 = "abc";
            frmLogin test = new frmLogin();

            //act
            test.UserAuthenticated(p, p_2);
           

            //assert
            bool result = test.Authenticated;
            Assert.AreEqual(false,result,"Login system has error");
        }

    }
}

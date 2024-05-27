using FireFightRedux;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class DBTests
    {
        [TestMethod]
        public void DataTableConnectionTest()
        {
            DBFunctions dBFunctions = new DBFunctions();
            try
            {
                dBFunctions.DataTableConnection.Open();
                dBFunctions.DataTableConnection.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ArmouryConnectionTest()
        {
            DBFunctions dBFunctions = new DBFunctions();
            try
            {
                dBFunctions.ArmouryConnection.Open();
                dBFunctions.ArmouryConnection.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CharacterConnectionTest()
        {
            DBFunctions dBFunctions = new DBFunctions();
            try
            {
                dBFunctions.CharactersConnection.Open();
                dBFunctions.CharactersConnection.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
using PhoenixFireFight.Functions;

namespace PhoenixFireFightUnitTests
{
    [TestClass]
    public class ArcTestForwardFacing
    {
        private float sourcefacing = 0;
        private float arcwidth = 180;

        [TestMethod]
        public void SightArcTestBearing90()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 90, arcwidth);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SightArcTestBearing315()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 315, arcwidth);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SightArcTestBearing270()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 270, arcwidth);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SightArcTestBearingFail()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 180, arcwidth);

                Assert.IsFalse(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }

    [TestClass]
    public class ArcTestBackwardFacing
    {
        private float sourcefacing = 180;
        private float arcwidth = 180;

        [TestMethod]
        public void SightArcTestBearing90()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 90, arcwidth);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SightArcTestBearing215()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 215, arcwidth);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SightArcTestBearing270()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 270, arcwidth);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SightArcTestBearingFail()
        {
            try
            {
                bool result = GameFunctions.IsTargetWithinArc(sourcefacing, 0, arcwidth);

                Assert.IsFalse(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
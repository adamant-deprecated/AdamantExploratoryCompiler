using NUnit.Framework;

namespace Tests.Framework.System
{
	[TestFixture]
	public class DoubleTests
	{
		[Test]
		public void CheckedUnderflow()
		{
			var value = double.MinValue;
			Assert.AreEqual(0, checked(1 / value / value));
		}
	}
}

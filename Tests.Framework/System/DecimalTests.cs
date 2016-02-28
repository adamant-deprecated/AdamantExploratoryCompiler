using System;
using NUnit.Framework;

namespace Adamant.Exploratory.Tests.Framework.System
{
	[TestFixture]
	public class DecimalTests
	{
		[Test]
		public void UncheckedOverflow()
		{
			Assert.Throws<OverflowException>(() =>
			{
				var value = decimal.MaxValue;
				var result = unchecked(value + 1);
			});
		}

		[Test]
		public void UncheckedNegativeOverflow()
		{
			Assert.Throws<OverflowException>(() =>
			{
				var value = decimal.MinValue;
				var result = unchecked(value - 1);
			});
		}

		[Test]
		public void CheckedUnderflow()
		{
			var value = decimal.MinValue;
			Assert.AreEqual(0, checked(1 / value));
		}
	}
}

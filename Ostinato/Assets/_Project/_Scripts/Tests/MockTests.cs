using System.Text;
using NUnit.Framework;
namespace Tests.MockTests{
	public class MockTests {
		StringBuilder sb;
		[SetUp]
		public void Setup() {
			sb = new();
		}

		[Test]
		public void Verify_AllNotesPlayed_ReturnsTrue() {
			sb.Append("a");
			sb.Append("b");
			sb.Append("c");
			Assert.AreEqual("abc", sb.ToString());
		}
	}
}

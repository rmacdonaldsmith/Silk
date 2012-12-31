using System;
using NUnit.Framework;
using MacdonaldSmith.Silk.Transport; 

namespace MacdonaldSmith.Silk.Transport.Tests
{
	[TestFixture]
	public class TcpServerTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void BadPortInConstructorTest()
		{
			new TcpServer(-1, null);
		}
		
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullTcpListenerInConstructorTest()
		{
			new TcpServer(2, null);
		}
	}
}


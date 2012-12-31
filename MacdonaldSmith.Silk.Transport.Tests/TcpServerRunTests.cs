using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NUnit.Framework;
using MacdonaldSmith.Silk.Transport; 

namespace MacdonaldSmith.Silk.Transport.Tests
{
	[TestFixture]
	public class TcpServerRunTests : ITcpServerListener
	{
		private readonly object _lockObject = new object();
		private TcpServer _tcpServer;
		private int _port = 13000;
		private string _host = "localhost";
		private bool _client1Connected;
		private bool _client2Connected;
		private bool _message1Received;
		private bool _message2Received;
		
		[SetUp]
		public void SetUp()
		{
			_tcpServer = new TcpServer(_port, this);
			_tcpServer.Start();
		}
		
		[TearDown]
		public void TearDown()
		{
			_tcpServer.Stop();
		}
		
		[Test]
		public void TcpServerTest()
		{
			TcpClient client1 = new TcpClient(_host, _port);
			string message1 = "Hello World!";
			
			TcpClient client2 = new TcpClient(_host, _port);
			string message2 = "Hello Again.";
			
			try
			{
				lock(_lockObject)
				{
					//wait here until we are connected
					if(!_client1Connected)
					{
						if(Monitor.Wait(_lockObject, 500) == false)
						{
							Assert.Fail("Could not connect to the server within 500ms.");
						}
					}
				}
				
				Assert.IsTrue(client1.Connected);
				
				SendMessage(client1, message1);
				
				//wait here until the server receives the message
				lock(_lockObject)
				{
					if(!_message1Received)
					{
						if(Monitor.Wait(_lockObject, 1000) == false)
						{
							Console.WriteLine("Did not receive a message from the client within a reasonable time.");
							Assert.Fail("Did not receive a message from the client within 500ms.");
						}
						else
						{
							Console.WriteLine("Pulsed, message recieved.");
						}
					}
				}
				
				lock(_lockObject)
				{
					//wait here until we are connected
					if(!_client2Connected)
					{
						if(Monitor.Wait(_lockObject, 500) == false)
						{
							Assert.Fail("Could not connect to the server within 500ms.");
						}
					}
				}
				
				Assert.IsTrue(client2.Connected);
				
				SendMessage(client2, message2);
				
				//wait here until the server receives the message
				lock(_lockObject)
				{
					if(!_message2Received)
					{
						if(Monitor.Wait(_lockObject, 1000) == false)
						{
							Console.WriteLine("Did not receive a message from the client within a reasonable time.");
							Assert.Fail("Did not receive a message from the client within 500ms.");
						}
						else
						{
							Console.WriteLine("Pulsed, message recieved.");
						}
					}
				}
			}
			finally
			{
				client1.Close();
				client2.Close();
				
				//check that we have removed the TcpClient connections from the server.
				
			}
		}
		
		private void SendMessage(TcpClient tcpClient, string message)
		{
			byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
			
			NetworkStream stream = tcpClient.GetStream();
			stream.Write(sendBuffer, 0, sendBuffer.Length);
			stream.Close();			
		}
		
		
		public void OnNewClientConnected (ushort connectionId, System.Net.EndPoint endPoint)
		{
			lock(_lockObject)
			{
				if(connectionId == 1)
				{
					_client1Connected = true;
					Monitor.Pulse(_lockObject);
				}
				
				if(connectionId == 2)
				{
					_client2Connected = true;
					Monitor.Pulse(_lockObject);
				}
			}
		}

		public void OnReceiveMessage (ushort connectionId, byte[] messageBuffer)
		{
			lock(_lockObject)
			{
				if(connectionId == 1)
				{
					Assert.AreEqual(12, messageBuffer.Length);
					string message = Encoding.ASCII.GetString(messageBuffer);
					Console.WriteLine("Message received on server: {0}", message);
					Assert.AreEqual("Hello World!", message);
					_message1Received = true;
				}
				
				if(connectionId == 2)
				{
					Assert.AreEqual(12, messageBuffer.Length);
					string message = Encoding.ASCII.GetString(messageBuffer);
					Console.WriteLine("Message received on server: {0}", message);
					Assert.AreEqual("Hello Again.", message);
					_message2Received = true;
				}

				Monitor.Pulse(_lockObject);
			}
		}

		public void OnClientDisconnect (ushort connectionId)
		{
			throw new NotImplementedException ();
		}
		
	}
}


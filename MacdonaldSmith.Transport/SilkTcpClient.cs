using System;
using System.Net.Sockets;

namespace MacdonaldSmith.Silk.Transport
{
	public class SilkTcpClient
	{
		System.Net.Sockets.TcpClient _tcpClient;
		
		public SilkTcpClient (string host, int port)
		{
			_tcpClient = new TcpClient(host, port);
			_tcpClient.Connect(host, port);
		}
		
		public void Send(byte[] buffer)
		{
			_tcpClient.Client.Send(buffer);
		}
	}
}


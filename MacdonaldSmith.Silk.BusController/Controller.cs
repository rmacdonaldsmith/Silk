using System;
using System.Collections.Generic;
using MacdonaldSmith.Silk.Transport;

namespace MacdonaldSmith.Silk.BusController
{
	public class Controller : ITcpServerListener
	{
		private TcpServer _tcpServer;
		private List<ushort> _connections = new List<ushort>();
		
		public Controller (int listeningPort, int heartbeatPeriod)
		{
			_tcpServer = new TcpServer(listeningPort, this);
		}
		
		public void OnNewClientConnected(ushort connectionId, System.Net.EndPoint endPoint)
		{
			_connections.Add(connectionId);
		}
		
		public void OnReceiveMessage(ushort connectionId, byte[] messageBuffer)
		{
			//first message should be a replay request, throw if it is anything else
			//decode the header to find the message type
		}
		
		public void OnClientDisconnect(ushort connectionId)
		{
			//remove the client from the list
			if(_connections.Contains(connectionId))
			{
				_connections.Remove(connectionId);
			}
		}
	}
}


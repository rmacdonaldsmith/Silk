using System;
namespace MacdonaldSmith.Silk.Transport
{
	public interface ITcpServerListener
	{
		void OnNewClientConnected(ushort connectionId, System.Net.EndPoint endPoint);
		
		void OnReceiveMessage(ushort connectionId, byte[] messageBuffer);
		
		void OnClientDisconnect(ushort connectionId);
	}
}


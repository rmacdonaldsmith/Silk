using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace MacdonaldSmith.Silk.Transport
{	
	public sealed class TcpServer
	{
		private const int INITIAL_TCP_BUFFER_SIZE = 32768; //32k
		private readonly TcpListener _tcpListener;
		private readonly ITcpServerListener _tcpServerListener;
		private readonly int _port;
		private bool _running = true;
		private readonly Dictionary<ushort, TcpClient> _connectionMap = new Dictionary<ushort, TcpClient>();
		private ushort _connectionId = 0;
		
		public TcpServer (int port, ITcpServerListener tcpServerListener)
		{
			if(port < 0)
			{
				throw new ArgumentException("Port must be a positive integer.");
			}
			
			if(tcpServerListener == null)
			{
				throw new ArgumentNullException("tcpServerListener");
			}
			
			_port = port;
			_tcpListener = new TcpListener(port);
			_tcpServerListener = tcpServerListener;
			_tcpListener.Start();
			Console.WriteLine("Tcp Server mounted, listening on port {0}", _port);
		}
		
		public void Start()
		{
			Thread serviceThread = new Thread(new ThreadStart(Run));
			serviceThread.Name = "TcpServerThread";
			serviceThread.Start();
		}
		
		public void Stop()
		{
			//need some thread synchronisation here?
			if(_running)
			{
				_running = false;
				
				//close all the active client connections
				foreach(TcpClient client in _connectionMap.Values)
				{
					client.Close();
				}
			}
		}
		
		public void Send(byte[] encodedBuffer, ushort connectionId)
		{
			if(_connectionMap.ContainsKey(connectionId))
			{
				if(_connectionMap[connectionId].Connected)
				{
					_connectionMap[connectionId].Client.Send(encodedBuffer);
				}
			}
		}
		
		private void Run()
		{
			//enter loop to wait for clients to connect.
			while(_running)
			{
				TcpClient client = _tcpListener.AcceptTcpClient();
				
				Console.WriteLine("Connecting client: {0}", client.Client.RemoteEndPoint);
				
				if(client.Connected)
				{
					Console.WriteLine("Client connected");
					
					//need to make sure that we do not already have an active connection from this client
					RegisterClient(client);
					
					//callback that we have a new connected client
					_tcpServerListener.OnNewClientConnected(_connectionId, client.Client.RemoteEndPoint);
					
					NetworkStream stream = client.GetStream();
					
					if(stream.CanRead)
					{
						Console.WriteLine("Reading message buffer...");
						byte[] fullMessage = ReadFully(stream, INITIAL_TCP_BUFFER_SIZE);
						Console.WriteLine("Finished reading message buffer.");
						//callback with the complete message buffer
						_tcpServerListener.OnReceiveMessage(_connectionId, fullMessage);
					}
					else
					{
						throw new InvalidOperationException("Can not read from the network stream.");
					}
				}
				

			}
		}
		
		private ushort RegisterClient(TcpClient client)
		{
			ushort connectionId = 0;
			
			if(_connectionMap.Count > 0)
			{
				foreach(KeyValuePair<ushort, TcpClient> element in _connectionMap)
				{
					if(element.Value.Client.RemoteEndPoint != client.Client.RemoteEndPoint)
					{
						_connectionMap.Add(_connectionId++, client);
						connectionId = _connectionId;
					}
					else
					{
						connectionId = element.Key;
					}
				}
			}
			else
			{
				connectionId = _connectionId++;
			}
			
			return connectionId;
		}
		
		private static byte[] ReadFully (Stream stream, int initialLength)
		{
		    // If we've been passed an unhelpful initial length, just
		    // use the default.
		    if (initialLength < 1)
		    {
		        initialLength = INITIAL_TCP_BUFFER_SIZE;
		    }
		    
		    byte[] readBuffer = new byte[initialLength];
		    int numberOfBytesRead=0;
		    
		    int chunk;
		    while ( (chunk = stream.Read(readBuffer, numberOfBytesRead, readBuffer.Length-numberOfBytesRead)) > 0)
		    {
		        numberOfBytesRead += chunk;
		        
		        // If we've reached the end of our buffer, check to see if there's
		        // any more information
		        if (numberOfBytesRead == readBuffer.Length)
		        {
		            int nextByte = stream.ReadByte();
		            
		            // End of stream? If so, we're done
		            if (nextByte==-1)
		            {
		                return readBuffer;
		            }
		            
		            // Nope. Resize the buffer, put in the byte we've just
		            // read, and continue
		            byte[] newBuffer = new byte[readBuffer.Length*2];
		            Array.Copy(readBuffer, newBuffer, readBuffer.Length);
		            newBuffer[numberOfBytesRead]=(byte)nextByte;
		            readBuffer = newBuffer;
		            numberOfBytesRead++;
		        }
		    }
		    // Buffer is now too big. Shrink it.
		    byte[] ret = new byte[numberOfBytesRead];
		    Array.Copy(readBuffer, ret, numberOfBytesRead);
		    return ret;
		}
	}
}


using System;
using System.Collections.Generic;
using System.ServiceModel;
using MacdonaldSmith.Silk.Messaging;

using MacdonaldSmith.Silk.SharedInterfaces;

namespace MacdonaldSmith.Silk.Transport
{
	public class BusConnectionManager : IConnectionManager
	{
		//private readonly Dictionary<> _subscribers = new Dictionary<>();
		
		public BusConnectionManager ()
		{
		}
		
		public void Subscribe()
		{
			IConnectionManagerCallBack callBackProxy = OperationContext.Current.GetCallbackChannel<IConnectionManagerCallBack>();
			
			MessageProperties prop = OperationContext.Current.IncomingMessageProperties;
			
			RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
			string ip = endpoint.Address;
		}
		
		public void UnSubscribe()
		{}
		
		public void Command(SilkMessage message)
		{}
		
		public void Hearbeat(SilkMessage heartbeat)
		{}
	}
}


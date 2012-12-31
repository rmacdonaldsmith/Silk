using System;
using MacdonaldSmith.Silk.Messaging;
using System.ServiceModel;

namespace MacdonaldSmith.Silk.SharedInterfaces
{
	[ServiceContract]
	public interface IConnectionManagerCallBack
	{
		[OperationContract(IsOneWay=true)]
		void Disconnected();
		
		[OperationContract]
		void Response(SilkMessage message);
	}
}


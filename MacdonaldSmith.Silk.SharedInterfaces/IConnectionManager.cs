using System;
using System.ServiceModel;
using MacdonaldSmith.Silk.Messaging;

namespace MacdonaldSmith.Silk.SharedInterfaces
{
	[ServiceContract(CallbackContract=typeof(SharedInterfaces.IConnectionManagerCallBack))]
	public interface IConnectionManager
	{
		[OperationContract]
		void Subscribe();
		
		[OperationContract]
		void UnSubscribe();
	
		[OperationContract]
		void Command(SilkMessage message);
		
		[OperationContract]
		void Hearbeat(SilkMessage heartbeat);
	}
}


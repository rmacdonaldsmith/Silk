using System;
namespace MacdonaldSmith.Silk.Messaging
{
	public abstract class SilkMessage
	{
		//Header: contains SilkType (as an int) and payload size as a long.
		//Therefore the header will always be a fixed size of 12 bytes
		
		public SilkTypeEnum SilkType {get; protected set;}
		
		public SilkMessage ()
		{
		}
		
		public abstract byte[] Encode();
		
		public abstract SilkMessage Decode(byte[] byteStream);
	}
}


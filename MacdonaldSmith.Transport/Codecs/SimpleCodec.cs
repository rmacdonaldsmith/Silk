using System;
using MacdonaldSmith.Silk.Messaging; 

namespace MacdonaldSmith.Silk.Transport.Codecs
{
	public class SimpleCodec : ICodec
	{
		public SimpleCodec ()
		{
		}
		
		public byte[] Encode(SilkMessage message)
		{
			//1. create the header with a size of 12 bytes
			//		- silk enum type as an int32 = 4 bytes
			//		- payload size as int64 = 8 bytes
			
			//2. each message knows how to encode itself
			
			throw new NotImplementedException();
		}
		
		public SilkMessage Decode(byte[] byteStream)
		{
			throw new NotImplementedException();
		}
	}
}


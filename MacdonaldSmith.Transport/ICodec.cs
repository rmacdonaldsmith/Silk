using System;
using MacdonaldSmith.Silk.Messaging;

namespace MacdonaldSmith.Silk.Transport
{
	public interface ICodec
	{
		byte[] Encode(SilkMessage message);
		
		SilkMessage Decode(byte[] byteStream);
	}
}


using System;
using System.Text;
namespace MacdonaldSmith.Silk.Messaging
{
	public class Replay : SilkMessage
	{
		public uint SequenceNumber {get; set;}
		
		public Replay()
		{
			this.SilkType = SilkTypeEnum.Replay;
		}
		
		public override SilkMessage Decode (byte[] byteStream)
		{
			string msg = Encoding.ASCII.GetString(byteStream);
			
			return new Replay();
		}
		
		public override byte[] Encode ()
		{
			return Encoding.ASCII.GetBytes("messge");
		}
	}
}


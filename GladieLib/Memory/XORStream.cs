using System;
using System.IO;
using System.Runtime.InteropServices;


namespace GladieLib.memory
{
	public class XORStream:Stream {
				
		private Stream inner;
		private byte[] pw;
		private int pwReadIndex;
		private int pwWriteIndex;
		
		
		public XORStream(Stream inner, byte[] secret) {
			this.inner = inner;
			this.pw = secret;
			this.pwReadIndex = 0;
			this.pwWriteIndex = 0;
		}
		
		public override bool CanRead
		{
		    get { return inner.CanRead; }
		}
		
		public override bool CanWrite
		{
		    get { return inner.CanWrite; }
		}
		
		public override bool CanSeek
		{
		    get { return inner.CanSeek; }
		}
		
		public override long Length
		{
		    get { return inner.Length; }
		}
		
		public override long Position
		{
		    get { return inner.Position; }
			set { inner.Position = value; }
		}
		
		public override void Flush() {
			inner.Flush();
		}
		
		public override long Seek (long offset, SeekOrigin origin) {
			return inner.Seek(offset, origin);
		}
		
		public override void SetLength (long value) {
			inner.SetLength(value);
		}
		
		public override int Read ([In] [Out] byte[] buffer, int offset, int count) {			
			var result = inner.Read(buffer, offset, count);
			
			if(buffer != null && buffer.Length > 0) {				
				for (var i = 0+offset; i < count; i++) {
					buffer[i] = (byte) (buffer[i] ^ pw[pwReadIndex]);
					pwReadIndex++;
					if(pwReadIndex >= pw.Length) {
						pwReadIndex = 0;
					}
				}
			}
			
			return result;
		}
		
		public override void Write (byte[] buffer, int offset, int count) {
			if(buffer != null && buffer.Length > 0) {
				for (var i = 0+offset; i < count; i++) {
					buffer[i] = (byte) (buffer[i] ^ pw[pwWriteIndex]);
					pwWriteIndex++;
					if(pwWriteIndex >= pw.Length) {
						pwWriteIndex = 0;
					}
				}
			}
			
			inner.Write(buffer, offset, count);
		}
	}
}


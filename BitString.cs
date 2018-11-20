using System;

namespace Excessives.BitString {
	//{TODO} Test this properly
	struct BitString {
		public byte[] stream;

		public long Length { get { return stream.LongLength * 8; } }

		public BitString(byte[] stream) { this.stream = stream; }

		public bool this[ulong index] {
			get {//This - 'appears to work'...
				return
					(stream[(ulong)Math.Floor((double)index / 8.0)]
					& (1 << (int)(index % 8))
					) > 0;
			}
			//...and so does this
			set {
				if (value) {
					stream[(ulong)Math.Floor((double)index / 8.0)]
					|=
				  (byte)(1 << (int)(index % 8));//Turn the bit on

				} else {//{TODO} Make this turn the bit off.... I think
					stream[(ulong)Math.Floor((double)index / 8.0)]
				   |=
				 (byte)(1 << (int)(index % 8));//Turn the bit on

					stream[(ulong)Math.Floor((double)index / 8.0)]
				 ^=
			   (byte)(1 << (int)(index % 8));//Toggle the bit (Toggle from on -> off)
				}
			}
		}

	}
}

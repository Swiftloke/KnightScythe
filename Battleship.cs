using System;
using System.Collections.Generic;
using System.Text;

namespace KnightScythe
{
    class Battleship
    {
		// y and x are inverted for natural array behavior. Input values like you normally would (x, y)
		public Battleship(int length, int angle, int x, int y)
		{
			this.length = length;
			this.hitsLeft = this.length;
			this.angle = angle;
			this.x = x;
			this.y = y;
		}
		public bool Hit()
		{
			hitsLeft -= 1;
			if (hitsLeft == 0)
				return true;
			return false;
		}
		private int hitsLeft;
		public int length;
		public int angle;
		public int x;
		public int y;
	}
}

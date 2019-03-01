using System;
using System.Collections.Generic;
using System.Text;

namespace KnightScythe
{
    class Utils
    {
		static public Tuple<int, int> AngleToAdvance(int angle)
		{
			int advancex = 0, advancey = 0;
			switch (angle)
			{
				case 0: advancey = 1; break;
				case 90: advancex = -1; break;
				case 180: advancey = -1; break;
				case 270: advancex = 1; break;
				default: break;
			}
			return new Tuple<int, int>(advancex, advancey);
		}
		static public int StrToAngle(string angle)
		{
			angle = angle.ToLower();
			if (angle.Contains("left"))
				return 180;
			else if (angle.Contains("right"))
				return 0;
			else if (angle.Contains("up"))
				return 90;
			else if (angle.Contains("down"))
				return 270;
			else return -1;
		}
		static public bool AddBattleship(Field field, int shipSize, bool isPlayer)
		{
			int x, y, angle;
			string[] res;
			if (isPlayer)
			{
				Console.WriteLine("Enter ship sizes and orientations, for example: A-1, (left/right/up/down)");
				Console.Write($"Enter a position and orientation for a ship of size {shipSize}: ");
				try
				{
					res = Console.ReadLine().Split(',', '-');
					y = char.Parse(res[0].ToUpper()) - 'A';
					x = int.Parse(res[1]) - 1; //Arrays versus humans- who would win?
					angle = Utils.StrToAngle(res[2]);
				}
				catch (Exception)
				{ return false; }
			}
			else
			{
				Random rnd = new Random();
				x = rnd.Next(1, 10);
				y = rnd.Next(1, 10);
				angle = Utils.Angles[rnd.Next(Utils.Angles.Length)];
			}
			if (!field.AddShip(new Battleship(shipSize, angle, x, y)))
			{
				if(isPlayer)
					Console.WriteLine("Can't place a battleship there. Try again.");
				return false;
			}
			return true;
		}
		public static int[] Angles = { 0, 90, 180, 270 };
		public static int[] ShipSizes = { 2, 3, 3, 4, 5 };
	}
}

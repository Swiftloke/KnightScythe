using System;
using System.Collections.Generic;
using System.Text;

namespace KnightScythe
{
    class Field
    {
		public Field(string name, bool isPlayerField)
		{
			this.isPlayerField = isPlayerField;
			this.name = name;
			fieldArray = new Tuple<bool, Battleship>[10, 10];
			lastHit = new Tuple<int, int>(-1, -1);
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					fieldArray[i, j] = new Tuple<bool, Battleship>(false, null);
				}
			}
		}
		public bool AddShip(Battleship ship)
		{
			int runx = ship.y; //No, this isn't a typo.
			int runy = ship.x;
			var advance = Utils.AngleToAdvance(ship.angle);
			int advancex = advance.Item1, advancey = advance.Item2;

			if (runx + ship.length * advancex >= 10 ||
				runx + ship.length * advancex < 0 ||
				runy + ship.length * advancey >= 10 ||
				runy + ship.length * advancey < 0)
			{
				return false; //Battleship goes out of bounds
			}
			for (int i = 0; i < ship.length; i++)
			{
				if (fieldArray[runx, runy].Item2 != null)
				{
					return false; //Already a battleship here
				}
				runx += advancex;
				runy += advancey;
			}
			runx = ship.y; //Reset
			runy = ship.x;
			for (int i = 0; i < ship.length; i++)
			{
				//All checks passed, assign
				fieldArray[runx, runy] = new Tuple<bool, Battleship>(false, ship);
				runx += advancex;
				runy += advancey;
			}
			shipsLeft++;
			return true;
		}
		// Possible return values: "Already hit this spot!",
		// "Miss!", "You sunk my Battleship! Length: {len}",
		// "You sunk my last Battleship! Game Over!", and "Hit!"
		// Callee is meant to print this result and act based on it.
		// y and x are inverted for natural array behavior. Input values like you normally would (x, y)
		public string Sink(int y, int x)
		{
			Tuple<bool, Battleship> op;
			try
			{ op = fieldArray[x, y]; }
			catch(IndexOutOfRangeException)
			{ return ""; }
			lastHit = new Tuple<int, int>(x, y);
			fieldArray[x, y] = new Tuple<bool, Battleship>(true, op.Item2);
			if (op.Item1 == true)
				return "Already hit this spot!";
			else if (op.Item2 == null)
				return "Miss!";
			bool hit = op.Item2.Hit();
			if (hit)
			{
				string ret = $"You sunk my Battleship! Length: {op.Item2.length}";
				shipsLeft--;
				if (shipsLeft == 0)
					ret = "You sunk my last Battleship! Game Over!";
				return ret;
			}
			else
				return "Hit!";
		}
		public void DisplayField(int columnPos)
		{
			Console.CursorTop = 0;
			Console.CursorLeft = columnPos;
			Console.WriteLine("   " + name);
			Console.CursorLeft = columnPos;
			Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
			for (int i = 0; i < 10; i++)
			{
				Console.CursorLeft = columnPos;
				Console.Write(char.ConvertFromUtf32('A' + i) + ": ");
				for (int j = 0; j < 10; j++)
				{
					var op = fieldArray[i, j];
					char icon = (op.Item1) ? 'X' : 'O';
					if (op.Item1 && op.Item2 != null)
						icon = '!';
					else if(isPlayerField && op.Item2 != null)
						icon = '\'';

					if (i == lastHit.Item1 && j == lastHit.Item2)
						Console.BackgroundColor = ConsoleColor.White; //Highlight
					
					Console.Write(icon);

					if (i == lastHit.Item1 && j == lastHit.Item2) //Reset
						Console.BackgroundColor = ConsoleColor.Black;

					Console.Write(' ');
				}
				Console.Write('\n');
			}
		}

		//Item1: False if not attacked yet, true it it was.
		//Item2: Battleship if one is associated with the spot, null otherwise.
		private Tuple<bool, Battleship>[,] fieldArray;
		private Tuple<int, int> lastHit;
		private int shipsLeft = 0;
		private bool isPlayerField;
		private string name;
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace KnightScythe
{
	//Stupid simple CPU opponent
	//No neural networks or anything, just a bit of guesswork
    class CPU
    {
		public CPU(string CPUName, bool showField)
		{
			Field = new Field($"{CPUName}'s Field", showField);
			hitPositions = new List<Tuple<int, int>>();
			random = new Random();
			this.CPUName = CPUName;
			foreach (var size in Utils.ShipSizes)
			{
				while (!Utils.AddBattleship(Field, size, false)) { }
			}
		}
		public string makeMove(Field opponentField)
		{
			int x = random.Next(0, 10), y = random.Next(0, 10);
			while (hitPositions.Contains(new Tuple<int, int>(x, y)))
			{
				x = random.Next(0, 10);
				y = random.Next(0, 10);
			}
			string res = opponentField.Sink(x, y);
			Console.WriteLine($"{CPUName}'s Turn: {Convert.ToChar(y + 'A')}-{x}! Result: {res}");
			/*if (res.Contains("Hit") && !lastX.HasValue)
			{
				lastX = x;
				lastY = y;
			}*/
			hitPositions.Add(new Tuple<int, int>(x, y));
			LastAttack = $"{Convert.ToChar(y + 'A')}-{x}!";
			return res;
		}
		//private int? lastX = null, lastY = null;
		//private int lastAngle = 0;
		private Random random;
		private string CPUName;
		private List<Tuple<int, int>> hitPositions;

		public Field Field { get; }
		public string LastAttack { get; private set; }
	}
}

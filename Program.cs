using System;

namespace KnightScythe
{
	class Program
    {
		static void Main(string[] args)
		{
			var ship = new Battleship(5, 0, 0, 0);
			var ship2 = new Battleship(2, 0, 2, 2);
			CPU cpu, cpu2;
			var playerField = new Field("Player's Board", true);
			Console.Write("Will you play against the computer, or will it play itself? (1/0 Players): ");
			int numPlayers = int.Parse(Console.ReadLine());
			if (numPlayers == 0)
			{
				cpu = new CPU("CPU2", true);
				cpu2 = new CPU("CPU1", true);
			}
			else
			{
				cpu = new CPU("CPU", false);
				cpu2 = new CPU("Unused", false);
				foreach (var size in Utils.ShipSizes)
				{
					Console.Clear();
					playerField.DisplayField(0);
					while (!Utils.AddBattleship(playerField, size, true)) { }
				}
			}
			bool gameEnded = false;
			string p1LastTurn = "", p2LastTurn = "";
			while (!gameEnded)
			{
				Console.Clear();
				//playerField.DisplayField(0);
				string res = "";
				if (numPlayers == 0)
				{
					cpu2.Field.DisplayField(0);
					cpu.Field.DisplayField(40);
					Console.WriteLine($"Player 1's last turn: {p1LastTurn}");
					Console.WriteLine($"Player 2's last turn: {p2LastTurn}");
					res = cpu2.LastAttack + " " + cpu2.makeMove(cpu.Field);
				}
				else
				{
					playerField.DisplayField(0);
					cpu.Field.DisplayField(40);
					Console.WriteLine($"Player 1's last turn: {p1LastTurn}");
					Console.WriteLine($"Player 2's last turn: {p2LastTurn}");
					while (res == "")
					{
						Console.Write("Enter a position to attack (for example, A-12): ");
						string rawinput = Console.ReadLine();
						string[] input = rawinput.Split('-');
						int y = char.Parse(input[0].ToUpper()) - 'A';
						int x = int.Parse(input[1]) - 1; //Arrays versus humans- who would win?
						res = cpu.Field.Sink(x, y);
						if (res == "") Console.WriteLine("Invalid position. Try again.");
						res = $"{rawinput.ToUpper()}! {res}";
					}
				}
				p1LastTurn = res;
				if (res.Contains("Game Over"))
					gameEnded = true;
				res = cpu.makeMove(numPlayers == 0 ? cpu2.Field : playerField);
				if (res.Contains("Game Over"))
					gameEnded = true;
				p2LastTurn = cpu.LastAttack + " " + res;
			}
			//Final Results
			Field p1field = numPlayers == 0 ? cpu2.Field : playerField;
			/*Console.Clear();
			p1field.DisplayField(0);
			cpu.Field.DisplayField(40);
			Console.WriteLine(p1LastTurn);
			Console.WriteLine(p2LastTurn);*/
			Console.ReadKey();
		}
	}

}
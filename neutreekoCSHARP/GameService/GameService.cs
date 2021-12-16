// Include namespace system
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neutreekoCSHARP
{

    public class GameService
	{
		public static void Start()
		{
			Console.WriteLine("INSTRUCTIONS");
			Console.WriteLine("  Positions");
			Console.WriteLine("    0 0  0 1  ...  0 4");
			Console.WriteLine("    1 0  ...  ...  ...");
			Console.WriteLine("    ...  ...  ...  ...");
			Console.WriteLine("    4 0  ...  ...  4 4");
			Console.WriteLine("  Directions");
			Console.WriteLine("    7  8  9");
			Console.WriteLine("    4     6");
			Console.WriteLine("    1  2  3");
			Console.WriteLine("  Accepted entries");
			Console.WriteLine("    [0-4] [0-4] [1-46-9]");
			Console.WriteLine("    $1 position.i");
			Console.WriteLine("    $2 position.j");
			Console.WriteLine("    $3 direction");
			Console.WriteLine("  Exemple");
			Console.WriteLine("    (0,1) right <=> 0 3 6\n");
			// Start
			var game = new AIGame();
			game.start();
		}
	}
}

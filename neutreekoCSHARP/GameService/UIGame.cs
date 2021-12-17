using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neutreekoCSHARP
{
	public class UIGame : Game
	{
		public int[,] turn(int pawnRow, int pawnColumn, int direction)
		{
			var ai = new AI(this);
			ai.game_.currentPlayer_ = 1;
			playUserMove(pawnRow, pawnColumn, direction);
			new BoardOperations(ai.game_.board_).displayBoard();
			if (hasWon(currentPlayer_))
				Console.WriteLine("Game over. Player\'s " + currentPlayer_.ToString() + " victory.");

			return ai.game_.board_;
		}

		public int[,] AIMove()
        {
			var ai = new AI(this);
			ai.game_.currentPlayer_ = 2;
			ai.playBestMove();

			if (hasWon(currentPlayer_))
				Console.WriteLine("Game over. Player\'s " + currentPlayer_.ToString() + " victory.");

			return ai.game_.board_;
		}
	}
}

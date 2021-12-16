// Include namespace system
using System;

namespace neutreekoCSHARP
{
    /**
Introduces an AI with a min-max algorithm.
	 */
    public class AIGame : Game
	{
		public void start()
		{
			// Initialisation.
			var ai = new AI(this);
			// Main loop.
			do
			{
				currentPlayer_ = getOpponent();
				boardOperations_.displayBoard();
                Console.WriteLine("Player " + currentPlayer_.ToString() + " : ");
				var player = (currentPlayer_ == 1) ? Settings.PLAYER_1 : Settings.PLAYER_2;
				if (player == PlayerType.AI)
				{
					ai.playBestMove();
				}
				else
				{
                    playUserMove();
				}
			} while (!hasWon(currentPlayer_));
			boardOperations_.displayBoard();
            Console.WriteLine("Game over. Player\'s " + currentPlayer_.ToString() + " victory.");
		}
	}
}

// Include namespace system
using System;

namespace neutreekoCSHARP
{
    public class AI
	{
		public Game game_;
		public AI(Game game)
		{
			game_ = game;
		}
		public int forEachPossibleMove(Strategy strategy, int player)
		{
			var res = 0;
			// For each piece
			for (var i = 0; i < 3; i++)
			{
				var oldPosition = game_.pawns_[player, i];
				// foreach valid move
				foreach (Coords direction in game_.directions_)
				{
					var move = new Move(oldPosition, direction);
					if (game_.boardOperations_.isValidMove(move))
					{
                        Coords newPosition = game_.move(move);
					 
						strategy.currentMove_ = move;
						
						res = strategy.run();
						
						game_.pawns_[player, i] = oldPosition;
						game_.boardOperations_.swap(newPosition, oldPosition);
						
						if (strategy.shouldExitNow())
						{
							return res;
						}
					}
				}
			}
			return res;
		}
		public void playBestMove()
		{
			// Initialisation.
			var strategy = new FindBestMoveStrategy(this);
			// We apply the min-max algorithm.
			forEachPossibleMove(strategy, game_.currentPlayer_);
			// Interpretation
			if (strategy.maxScore > 0)
			{
				Console.WriteLine("I'm going to win...\n");
			}
			else if (strategy.maxScore < 0)
			{
				Console.WriteLine("You can win ...\n");
			}
			// Finally, we make the best move.
			game_.move(strategy.bestMove);
		}
		private class FindBestMoveStrategy : Strategy
		{
            public FindBestMoveStrategy(AI ai)
            {
                this.ai = ai;
            }
			public int maxScore = -1001;
			public Move bestMove = null;
			private int maxNOpenings = 0;
            private readonly AI ai;

            public override int run()
			{
				var minMax = new MinMax(ai);
				// We evaluate the move according to the victory / defeat criterion.
				var score = minMax.run();
				// We update the best possible move.
				if (score >= maxScore)
				{
					// In the event of a tie, we choose a move aiming to maximize the possibilities
					// opponent's error.
					var nOpenings = minMax.nOpenings_;
					if (score > maxScore || (score == maxScore && nOpenings > maxNOpenings))
					{
						maxScore = score;
						bestMove = currentMove_;
						maxNOpenings = nOpenings;
					}
				}
				return -1;
			}
		}
	}
}

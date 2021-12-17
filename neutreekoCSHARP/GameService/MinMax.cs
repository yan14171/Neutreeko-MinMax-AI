// Include namespace system
using System;

namespace neutreekoCSHARP
{
    public class MinMax
	{
		public Game game_;
		public AI ai_;
		public int score_;
		public int nOpenings_;
		public MinMax(AI ai)
		{
			game_ = ai.game_;
			ai_ = ai;
		}
		public int run()
		{
			if (game_.hasWon(game_.currentPlayer_))
				return 1000;

			var min = new StrategyForMin(1001, -1001, 0, game_, ai_);
            score_ = ai_.forEachPossibleMove(min, game_.getOpponent());
			nOpenings_ = min.nOpenings_;
            return score_;
		}

		private abstract class MinMaxStrategy : Strategy
		{
			public int minScore_;
			public int maxScore_;
			public int depth_;
			public int player_;
			public MinMaxStrategy(int minScore, int maxScore, int depth)
			{
				minScore_ = minScore;
				maxScore_ = maxScore;
				depth_ = depth;
			}
		}

		private class StrategyForMax : MinMaxStrategy
		{
            private readonly Game game_;
            private readonly AI ai_;

            public StrategyForMax(int minScore, int maxScore, int depth, Game game_, AI ai_)
				:base (minScore, maxScore, depth)
			{
				player_ = game_.getOpponent();
                this.game_ = game_;
                this.ai_ = ai_;
            }

			public override int run()
			{
				if (game_.hasWon(player_))
				{
					return (-1000 + depth_);
				}
				if (depth_ == Settings.MAX_MINMAX_DEPTH)
				{
					return 0;
				}
				var min = new StrategyForMin(minScore_, maxScore_, depth_ + 1, game_, ai_);
				maxScore_ = Math.Max(maxScore_, ai_.forEachPossibleMove(min, player_));
				return maxScore_;
			}

			public override bool shouldExitNow()
			{
				return depth_ >= Settings.MAX_OPENINGS_DEPTH && maxScore_ >= minScore_;
			}
		}

		private class StrategyForMin : MinMaxStrategy
		{
            private readonly Game game_;
            private readonly AI ai_;
            public int nOpenings_ = 0;
			public StrategyForMin(int minScore, int maxScore, int depth, Game game_, AI ai_)
				:base(minScore, maxScore, depth)
			{
				player_ = game_.currentPlayer_;
                this.game_ = game_;
                this.ai_ = ai_;
            }
			public override int run()
			{
				if (game_.hasWon(player_))
				{
					// We won
					return 1000 - depth_;
				}
				if (depth_ == Settings.MAX_MINMAX_DEPTH)
				{
					// Max depth reached.
					return 0;
				}
				var max = new StrategyForMax(minScore_, maxScore_, depth_ + 1, game_, ai_);
				var score = ai_.forEachPossibleMove(max, player_);
				if (depth_ == 0 && score > 0)
				{
					nOpenings_++;
				}
				minScore_ = Math.Min(minScore_, score);
				return minScore_;
			}

			public override bool shouldExitNow()
			{
				return depth_ >= Settings.MAX_OPENINGS_DEPTH && minScore_ <= maxScore_;
			}
		}
	}
}

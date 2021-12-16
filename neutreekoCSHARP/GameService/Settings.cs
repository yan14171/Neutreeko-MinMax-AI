// Include namespace system

namespace neutreekoCSHARP
{
    public sealed class Settings
	{
		/**
			* Choice of two opponents.
			* ie. The game can be multiplayer, human-AI, or AI-AI. 
			*/
		public
		const PlayerType PLAYER_1 = PlayerType.HUMAN;
		public
		const PlayerType PLAYER_2 = PlayerType.AI;
			/**
			* Maximum depth of recursive analysis.
			* ie. Number of hits the AI will see ahead of time. 
			*/
		public
		const int MAX_MINMAX_DEPTH = 7;
			/**
			* Maximum depth of the recursive count of the number of possible errors.
			* The number must be less than or equal to the depth of the assessment.
			* ie. The AI will count the number of fatal errors that the opponent can
			* do in the next round. 
			*/
		public
		const int MAX_OPENINGS_DEPTH = 4;
	}
}

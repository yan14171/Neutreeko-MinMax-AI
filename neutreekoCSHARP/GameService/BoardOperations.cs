// Include namespace system
using System;

namespace neutreekoCSHARP
{
    public class BoardOperations
	{
		private int[,] board_;
		public BoardOperations(int[,] board)
		{
			board_ = board;
		}
		public void displayBoard()
		{
			for (var i = 0; i < 5; i++)
			{
				for (var j = 0; j < 5; j++)
				{
					if (board_[i, j] != 0)
						Console.Write(board_[i, j].ToString() + " ");
					else
						Console.Write("- ");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		public void swap(Coords a, Coords b)
		{
			var tmp = board_[a.i, a.j];
			board_[a.i, a.j] = board_[b.i, b.j];
			board_[b.i, b.j] = tmp;
		}
		public bool isValidMove(Move move)
		{
			var newPosition = move.position.add(move.direction);
			if (newPosition.isOnBoard())
			{
				return board_[newPosition.i, newPosition.j] == 0;
			}
			return false;
		}
	}
}

// Include namespace system
using System;

namespace neutreekoCSHARP
{
    public abstract class Game
	{
		public int[,] board_ = new int[5, 5];
		public BoardOperations boardOperations_;
		// Coordinates of the pawns. [, player1, player2] [pawn1, pawn2, pawn3].
		public Coords[,] pawns_ = new Coords[3, 3];
		public int currentPlayer_ = 2;
		// 8 directions possibles (NO-N-NE-O-E-SO-S-SE)
		public
		readonly Coords[] directions_ = new Coords[8];
		public Game()
		{
			boardOperations_ = new BoardOperations(board_);

			// Initial position of the pawns on the board.
			this.board_[0, 1] = this.board_[0, 3] = this.board_[3, 2] = 1;
			this.board_[1, 2] = this.board_[4, 1] = this.board_[4, 3] = 2;
			this.pawns_[1, 0] = new Coords(0, 1);
			this.pawns_[1, 1] = new Coords(0, 3);
			this.pawns_[1, 2] = new Coords(3, 2);
			this.pawns_[2, 0] = new Coords(1, 2);
			this.pawns_[2, 1] = new Coords(4, 1);
			this.pawns_[2, 2] = new Coords(4, 3);
			// We fill the vector with 8 possible directions.
			var k = 0;
			for (var i = -1; i <= 1; i++)
			{
				for (var j = -1; j <= 1; j++)
				{
					if (i != 0 || j != 0)
					{
						this.directions_[k++] = new Coords(i, j);
					}
				}
			}
		}

		public bool hasWon(int player)
		{
			for (var i = 0; i < 3; i++)
			{
				// Every possible pawn.
				foreach ( Coords direction in this.directions_)
				{
					// Each direction.
					// We see if the following two boxes in this direction
					// contain the other two pawns.
					var square2 = this.pawns_[player, i].add(direction);
					var square3 = square2.add(direction);
					if (square3.isOnBoard())
					{
						// If we are still in the field.
						if (this.board_[square2.i, square2.j] == player && this.board_[square3.i, square3.j] == player)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public Coords move(Move move)
		{
			// Player owning the pawn.
			var oldPosition = move.position;
			var player = this.board_[oldPosition.i, oldPosition.j];
			var newPosition = oldPosition.clone();
			// We move the pawn in the direction as long as we can.
			while (this.boardOperations_.isValidMove(new Move(newPosition, move.direction)))
			{
				newPosition.addAssign(move.direction);
			}
			// We found the final position. We put the pawn there.
			this.boardOperations_.swap(newPosition, oldPosition);
			// We update the coordinates of the player's pawns.
			for (var i = 0; i < 3; i++)
			{
				if (this.pawns_[player, i].equals(oldPosition))
				{
					this.pawns_[player, i] = newPosition;
					break;
				}
			}
			return newPosition;
		}

		public void playUserMove()
		{
			while (true)
			{
				var i = Convert.ToInt64(Console.ReadLine());
				var j = Convert.ToInt64(Console.ReadLine());
				var direction = Convert.ToInt64(Console.ReadLine());
				var isValidDirection = true;
				if (direction >= 1 && direction <= 3)
				{
					direction += 4;
				}
				else if (direction == 4)
				{
					direction--;
				}
				else if (direction == 6)
				{
					direction -= 2;
				}
				else if (direction >= 7 && direction <= 9)
				{
					direction -= 7;
				}
				else
				{
					isValidDirection = false;
				}
				var position = new Coords((int)i, (int)j);
				
				if (isValidDirection)
				{
					if (position.isOnBoard())
					{
						if (this.board_[i, j] == this.currentPlayer_)
						{
							var move = new Move(position, this.directions_[direction]);
							if (this.boardOperations_.isValidMove(move))
							{
								this.move(move);
								return;
							}
							else
							{
								Console.WriteLine("Direction invalid");
							}
						}
						else
						{
							Console.WriteLine("No pawns available");
						}
					}
					else
					{
						Console.WriteLine("Position invalid");
					}
				}
				else
				{
					Console.WriteLine("Direction invalid");
				}
			}
		}
		public int getOpponent()
		{
			return (this.currentPlayer_ % 2) + 1;
		}
	}
}

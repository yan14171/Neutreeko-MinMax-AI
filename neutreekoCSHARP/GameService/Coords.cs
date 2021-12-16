// Include namespace system

namespace neutreekoCSHARP
{
    public class Coords
	{
		public int i;
		public int j;
		public Coords(int i, int j)
		{
			this.i = i;
			this.j = j;
		}
		public Coords add(Coords coords)
		{
			return new Coords(this.i + coords.i, this.j + coords.j);
		}
		public void addAssign(Coords coords)
		{
			this.i += coords.i;
			this.j += coords.j;
		}
		public Coords clone()
		{
			return new Coords(i, j);
		}
		public bool equals(Coords coords)
		{
			return this.i == coords.i && this.j == coords.j;
		}
		public bool isOnBoard()
		{
			return this.i >= 0 && this.i < 5 && this.j >= 0 && this.j < 5;
		}
	}
}

// Include namespace system

namespace neutreekoCSHARP
{
    public abstract class Strategy
	{
		//Turn on which the strategy is applied.
		public Move currentMove_;
		public abstract int run();

		public virtual bool shouldExitNow()
		{
			return false;
		}
	}
}

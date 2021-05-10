namespace FoodOrdering.Common.Functional
{
	public class Error
	{
		public virtual string Message { get; }

		public Error()
		{
		}

		public Error(string message)
		{
			Message = message;
		}
	}
}

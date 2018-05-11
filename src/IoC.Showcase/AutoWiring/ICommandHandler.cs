namespace IoC.Showcase.AutoWiring
{
	public interface ICommandHandler<T>
	{
	}

	public class TextCommand : ICommandHandler<string>
	{
	}

	public class NumberCommand : ICommandHandler<int>
	{
	}
}
namespace AIv2 {
	public interface ICommand {
		void Execute(Bot bot);
		BotHandleStratagy CreateHandleStratagy();
	}
}
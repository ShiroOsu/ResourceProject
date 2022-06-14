namespace Code.Interfaces
{
    public interface IConsoleCommand
    {
        public string CommandWord { get; }
        public bool Process(string[] arguments);
    }
}

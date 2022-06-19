namespace Code.Interfaces
{
    public interface IConsoleCommand
    {
        public string CommandWord { get; }
        public string ToolTip { get; }
        public bool Process(string[] arguments);
    }
}

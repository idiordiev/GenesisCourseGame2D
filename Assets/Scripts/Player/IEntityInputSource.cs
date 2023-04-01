namespace Player
{
    public interface IEntityInputSource
    {
        float Direction { get; }
        bool Jump { get; }
        bool Attack { get; }

        void ResetOneTimeActions();
    }
}
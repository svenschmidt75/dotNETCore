namespace GoogleProblem20
{
    public interface ITimePiece
    {
        void Advance(long ms);
        long GetTime();
    }
}
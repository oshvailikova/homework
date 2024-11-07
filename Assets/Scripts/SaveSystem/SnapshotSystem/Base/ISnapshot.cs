namespace SaveSystem.SnapshotSystem.Base
{
    public interface ISnapshot
    {
        string GetSnapshot();
        void SetSnapshot(string state);
    }
}
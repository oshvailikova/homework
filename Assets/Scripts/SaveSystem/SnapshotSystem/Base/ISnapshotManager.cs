namespace SaveSystem.SnapshotSystem.Base
{
    public interface ISnapshotManager
    {
        void SaveSnapshot(ISnapshot snapshot);
        ISnapshot LoadLastSnapshot();
        ISnapshot PeekLastSnapshot();
        void ClearSnapshots();
    }
}

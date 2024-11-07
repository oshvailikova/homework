using SaveSystem.SnapshotSystem.Base;
using System.Collections.Generic;

namespace SaveSystem.SnapshotSystem
{
    public sealed class SnapshotManager : ISnapshotManager
    {
        private readonly Stack<ISnapshot> _snapshots = new Stack<ISnapshot>();

        public void SaveSnapshot(ISnapshot snapshot)
        {
            _snapshots.Push(snapshot);
        }

        public ISnapshot LoadLastSnapshot()
        {
            return _snapshots.Count > 0 ? _snapshots.Pop() : null;
        }

        public ISnapshot PeekLastSnapshot()
        {
            return _snapshots.Count > 0 ? _snapshots.Peek() : null;
        }

        public void ClearSnapshots()
        {
            _snapshots.Clear();
        }
    }
}

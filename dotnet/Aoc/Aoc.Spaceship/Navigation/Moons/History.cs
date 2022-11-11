using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class History
    {
        public SortedDictionary<Vector, SortedDictionary<Vector, SortedDictionary<Vector, SortedSet<Vector>>>> _history;

        public History()
        {
            _history =
                new SortedDictionary<Vector, SortedDictionary<Vector, SortedDictionary<Vector, SortedSet<Vector>>>>();
        }

        public bool AddSnapshot(Snapshot snapshot)
        {
            if (!_history.ContainsKey(snapshot.Vector0))
                _history.Add(snapshot.Vector0, new SortedDictionary<Vector, SortedDictionary<Vector, SortedSet<Vector>>>());
            var zeroList = _history[snapshot.Vector0];
            if (!zeroList.ContainsKey(snapshot.Vector1))
                zeroList.Add(snapshot.Vector1, new SortedDictionary<Vector, SortedSet<Vector>>());
            var oneList = zeroList[snapshot.Vector1];
            if (!oneList.ContainsKey(snapshot.Vector2))
                oneList.Add(snapshot.Vector2, new SortedSet<Vector>());
            var twoList = oneList[snapshot.Vector2];
            if (twoList.Contains(snapshot.Vector3))
                return true;
            twoList.Add(snapshot.Vector3);
            return false;
        }
    }
}

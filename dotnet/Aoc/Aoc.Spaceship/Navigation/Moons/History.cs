using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Spaceship.Navigation.Moons
{
    public class History
    {
        public SortedDictionary<long, SortedDictionary<long, SortedSet<long>>> _history;

        public History()
        {
            _history =
                new SortedDictionary<long, SortedDictionary<long, SortedSet<long>>>();
        }

        public bool AddSnapshot(Snapshot snapshot)
        {
            if (!_history.ContainsKey(snapshot.Vector0.GetLong()))
                _history.Add(snapshot.Vector0.GetLong(), new SortedDictionary<long, SortedSet<long>>());
            var zeroList = _history[snapshot.Vector0.GetLong()];
            if (!zeroList.ContainsKey(snapshot.Vector1.GetLong()))
                zeroList.Add(snapshot.Vector1.GetLong(), new SortedSet<long>());
            var oneList = zeroList[snapshot.Vector1.GetLong()];
            if (oneList.Contains(snapshot.Vector2.GetLong()))
                return true;
            oneList.Add(snapshot.Vector2.GetLong());
            return false;
        }
    }
}

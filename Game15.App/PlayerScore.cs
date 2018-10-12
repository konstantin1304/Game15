using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15.App
{
    class PlayerScore:IComparable
    {
        public string Name { get; set; }

        public int Time { get; set; }

        public int CompareTo(object obj)
        {
            PlayerScore ps = obj as PlayerScore;
            if (ps.Time > Time) return -1;
            if (ps.Time == Time) return 0;
            return 1;
        }
    }
}

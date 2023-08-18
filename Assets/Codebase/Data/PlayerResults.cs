using Assets.Codebase.Utils;
using System;
using System.Collections.Generic;

namespace Assets.Codebase.Data
{
    [Serializable]
    public class PlayerResults
    {
        public Dictionary<CombinationId, int> Combinations;
    }
}

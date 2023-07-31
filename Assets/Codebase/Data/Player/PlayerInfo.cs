using Assets.Codebase.Utils;
using System;

namespace Assets.Codebase.Data.Player
{
    [Serializable]
    public class PlayerInfo
    {
        private int _id;
        private string _name;
        private PlayerState _state;
        private SerializableDictionary<CombinationId, int> _combinations; 
    }
}

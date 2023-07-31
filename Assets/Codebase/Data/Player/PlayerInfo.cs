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


        public PlayerInfo(int id, string name)
        {
            _id = id;
            _name = name;
            _state = PlayerState.Idle;

            _combinations = new SerializableDictionary<CombinationId, int>();
            var combinations = Enum.GetValues(typeof(CombinationId));
            foreach (CombinationId combination in combinations)
            {
                _combinations.Add(combination, 0);
            }
        }
    }
}

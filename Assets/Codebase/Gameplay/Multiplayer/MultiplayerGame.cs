using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Codebase.Gameplay.Multiplayer
{
    [RequireComponent(typeof(PunTurnManager))]
    public class MultiplayerGame : MonoBehaviour, IPunTurnManagerCallbacks
    {
        private PunTurnManager _turnManager;

        private void Awake()
        {
            _turnManager = GetComponent<PunTurnManager>();
        }

        private void Start()
        {
            _turnManager.TurnManagerListener = this;
        }

        public void OnPlayerFinished(Player player, int turn, object move)
        {
            throw new System.NotImplementedException();
        }

        public void OnPlayerMove(Player player, int turn, object move)
        {
            throw new System.NotImplementedException();
        }

        public void OnTurnBegins(int turn)
        {
            throw new System.NotImplementedException();
        }

        public void OnTurnCompleted(int turn)
        {
            throw new System.NotImplementedException();
        }

        public void OnTurnTimeEnds(int turn)
        {
            throw new System.NotImplementedException();
        }
    }
}
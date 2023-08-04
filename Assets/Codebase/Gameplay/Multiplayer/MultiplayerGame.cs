using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Gameplay.Multiplayer
{
    [RequireComponent(typeof(PunTurnManager))]
    public class MultiplayerGame : MonoBehaviour, IPunTurnManagerCallbacks
    {
        [SerializeField] private Button _button;

        private PunTurnManager _turnManager;
        private int _activePlayerId = 0;

        private void Awake()
        {
            _turnManager = GetComponent<PunTurnManager>();
            _turnManager.TurnManagerListener = this;
        }

        private void Start()
        {
            BeginNewTurnIfMaster();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(EndPlayerTurn);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void BeginNewTurnIfMaster()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _turnManager.BeginTurn();
            }
        }

        private void EndPlayerTurn()
        {
            PhotonNetwork.LocalPlayer.SetFinishedTurn(_turnManager.Turn);
        }



        // Photon Callbacks
        public void OnPlayerFinished(Player player, int turn, object move)
        {
            _activePlayerId++;

            if (_activePlayerId > PhotonNetwork.CurrentRoom.PlayerCount)
            {
                _activePlayerId = 1;
            }

            if (PhotonNetwork.LocalPlayer.ActorNumber == _activePlayerId)
            {
                _button.gameObject.SetActive(true);
            }
            else
            {
                _button.gameObject.SetActive(false);
            }
        }

        public void OnPlayerMove(Player player, int turn, object move)
        {
            throw new System.NotImplementedException();
        }

        public void OnTurnBegins(int turn)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == _activePlayerId)
            {
                _button.gameObject.SetActive(true);
            }
            else
            {
                _button.gameObject.SetActive(false);
            }
        }

        public void OnTurnCompleted(int turn)
        {
            if (_turnManager.Turn == turn)
            {
                _activePlayerId = 1;
                BeginNewTurnIfMaster();
            }
        }

        public void OnTurnTimeEnds(int turn)
        {
            throw new System.NotImplementedException();
        }
    }
}
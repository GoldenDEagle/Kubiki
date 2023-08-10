using Assets.Codebase.Gameplay.General.CanvasVersion;
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
        [SerializeField] private Button _throwButton;
        [SerializeField] private SetOfDices _dices;

        private PunTurnManager _turnManager;
        private int _activePlayerId = 1;

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
            _throwButton.onClick.AddListener(MakeAThrow);
            _dices.OnRollStarted += DisableButton;
            _dices.OnRollEnded += EnableButton;
        }

        private void OnDisable()
        {
            _throwButton.onClick.RemoveAllListeners();
            _dices.OnRollStarted -= DisableButton;
            _dices.OnRollEnded -= EnableButton;
        }

        private void BeginNewTurnIfMaster()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _turnManager.BeginTurn();
            }
        }

        private void MakeAThrow()
        {
            if (_dices.CurrentThrowNumber > 2)
            {
                _dices.ResetThrowNumber();
                EndPlayerTurn();
                return;
            }

            _dices.MakeATrow();
        }

        private void EndPlayerTurn()
        {
            Debug.Log("Finished turn " + _turnManager.Turn);
            _turnManager.SendMove(null, true);
        }

        private void EnableButton()
        {
            _throwButton.interactable = true;
        }

        private void DisableButton()
        {
            _throwButton.interactable = false;
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
                _throwButton.gameObject.SetActive(true);
            }
            else
            {
                _throwButton.gameObject.SetActive(false);
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
                _throwButton.gameObject.SetActive(true);
            }
            else
            {
                _throwButton.gameObject.SetActive(false);
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
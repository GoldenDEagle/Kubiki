using Assets.Codebase.Network;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.UI.Window
{
    public class LobbyWindow : MonoBehaviour
    {
        private const string PlayersKey = "Игроки: ";

        [SerializeField] private int _multiplayerSceneId;
        [SerializeField] private TMP_Text _playerCounter;
        [SerializeField] private TMP_Text _playerNames;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _cancelButton;

        private int _playerCount;
        private int _minPlayersToStart = 1;

        private bool _readyToStart;
        private bool _startingGame;

        private void Start()
        {
            UpdatePlayerCount();
        }

        private void OnEnable()
        {
            NetworkCallbacks.Instance.OnNewPlayerEnteredRoom += UpdatePlayerCount;
            NetworkCallbacks.Instance.OnAnyPlayerLeftRoom += UpdatePlayerCount;
            _cancelButton.onClick.AddListener(CancelClicked);
            _startGameButton.onClick.AddListener(StartClicked);
        }

        private void OnDisable()
        {
            NetworkCallbacks.Instance.OnNewPlayerEnteredRoom -= UpdatePlayerCount;
            NetworkCallbacks.Instance.OnAnyPlayerLeftRoom -= UpdatePlayerCount;
            _cancelButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.RemoveAllListeners();
        }

        private void UpdatePlayerCount()
        {
            _playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if (_playerCount >= _minPlayersToStart)
            {
                _readyToStart = true;
            }
            else
            {
                _readyToStart = false;
            }

            _playerCounter.text = PlayersKey + _playerCount.ToString();
            _playerNames.text = string.Empty;
            var allPlayers = PhotonNetwork.CurrentRoom.Players.Values;
            foreach (var player in allPlayers)
            {
                _playerNames.text += player.NickName + " ";
            }
        }

        private void StartClicked()
        {
            if (_readyToStart && !_startingGame)
            {
                StartAGame();
            }
        }

        private void CancelClicked()
        {
            NetworkCallbacks.Instance.OnRoomLeft += GoBackToMenu;
            PhotonNetwork.LeaveRoom();
        }

        private void GoBackToMenu()
        {
            NetworkCallbacks.Instance.OnRoomLeft -= GoBackToMenu;
            PhotonNetwork.LoadLevel(1);
        }

        private void StartAGame()
        {
            _startingGame = true;

            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(_multiplayerSceneId);
        }
    }
}
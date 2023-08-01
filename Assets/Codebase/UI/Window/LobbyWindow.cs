using Assets.Codebase.Network;
using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.UI.Window
{
    public class LobbyWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerCounter;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _cancelButton;

        private bool _isLeaving = false;

        private void OnEnable()
        {
            _cancelButton.onClick.AddListener(CancelClicked);
            _startGameButton.onClick.AddListener(StartClicked);
        }

        private void OnDisable()
        {
            _cancelButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (_isLeaving) return;

            _playerCounter.text = "Игроки: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        }

        private void StartClicked()
        {

        }

        private void CancelClicked()
        {
            _isLeaving = true;
            NetworkCallbacks.Instance.OnRoomLeft += GoBackToMenu;
            PhotonNetwork.LeaveRoom();
        }

        private void GoBackToMenu()
        {
            NetworkCallbacks.Instance.OnRoomLeft -= GoBackToMenu;
            PhotonNetwork.LoadLevel(1);
        }
    }
}
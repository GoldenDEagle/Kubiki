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

        private void Update()
        {
            _playerCounter.text = "Игроки: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        }
    }
}
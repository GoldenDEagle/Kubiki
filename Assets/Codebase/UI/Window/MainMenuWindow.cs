using Assets.Codebase.Network;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.UI.Window
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private int _lobbySceneId;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _singleplayerButton;

        private void OnEnable()
        {
            _multiplayerButton.onClick.AddListener(MultiplayerButtonClicked);
        }

        private void OnDisable()
        {
            _multiplayerButton.onClick.RemoveAllListeners();
        }

        private void MultiplayerButtonClicked()
        {
            NetworkCallbacks.Instance.OnRoomJoined += GoToLobby;
            NetworkCallbacks.Instance.OnRoomCreationFailed += MultiplayerFailed;

            _multiplayerButton.gameObject.SetActive(false);
            _singleplayerButton.gameObject.SetActive(false);
            _nameField.gameObject.SetActive(false);
            _loadingText.gameObject.SetActive(true);


        }

        private void GoToLobby()
        {
            NetworkCallbacks.Instance.OnRoomJoined -= GoToLobby;

            PhotonNetwork.LoadLevel(_lobbySceneId);
        }

        private void MultiplayerFailed()
        {
            NetworkCallbacks.Instance.OnRoomCreationFailed -= MultiplayerFailed;

            _multiplayerButton.gameObject.SetActive(true);
            _singleplayerButton.gameObject.SetActive(true);
            _nameField.gameObject.SetActive(true);
            _loadingText.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            NetworkCallbacks.Instance.OnRoomJoined -= GoToLobby;
            NetworkCallbacks.Instance.OnRoomCreationFailed -= MultiplayerFailed;
        }
    }
}
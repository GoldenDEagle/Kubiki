using Assets.Codebase.Infrastructure.Services.Network;
using Assets.Codebase.Network;
using Assets.CodeBase.Infrastructure.Services;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.UI.Window
{
    public class MainMenuWindow : MonoBehaviour
    {
        private const string NameKey = "name";

        [SerializeField] private int _lobbySceneId;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _singleplayerButton;

        private INetworkService _netService;
        string _playerName;

        private void Awake()
        {
            _netService = ServiceLocator.Container.Single<INetworkService>();
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(NameKey))
            {
                _nameField.text = PlayerPrefs.GetString(NameKey);
            }
            else
            {
                _nameField.text = string.Empty;
            }
        }

        private void OnEnable()
        {
            NetworkCallbacks.Instance.OnConnectedToServer += AfterConnectedToServer;
            _multiplayerButton.onClick.AddListener(MultiplayerButtonClicked);
        }

        private void OnDisable()
        {
            NetworkCallbacks.Instance.OnConnectedToServer -= AfterConnectedToServer;
            _multiplayerButton.onClick.RemoveAllListeners();
        }

        private void MultiplayerButtonClicked()
        {
            if (_nameField.text.Length == 0)
            {
                return;
            }
            else
            {
                SetLocalPlayerName();
            }

            NetworkCallbacks.Instance.OnRoomJoined += GoToLobby;
            NetworkCallbacks.Instance.OnRoomCreationFailed += MultiplayerFailed;

            _netService.JoinRandomRoom();

            _multiplayerButton.gameObject.SetActive(false);
            _singleplayerButton.gameObject.SetActive(false);
            _nameField.gameObject.SetActive(false);
            _loadingText.gameObject.SetActive(true);
        }

        private void SetLocalPlayerName()
        {
            PhotonNetwork.LocalPlayer.NickName = _nameField.text;
            PlayerPrefs.SetString(NameKey, _nameField.text);
            PlayerPrefs.Save();
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

        private void AfterConnectedToServer()
        {
            _multiplayerButton.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            NetworkCallbacks.Instance.OnRoomJoined -= GoToLobby;
            NetworkCallbacks.Instance.OnRoomCreationFailed -= MultiplayerFailed;
        }
    }
}
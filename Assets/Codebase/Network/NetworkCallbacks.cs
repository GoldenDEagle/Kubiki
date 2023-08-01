using Assets.Codebase.Infrastructure.Services.Network;
using Assets.CodeBase.Infrastructure.Services;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace Assets.Codebase.Network
{
    public class NetworkCallbacks : MonoBehaviourPunCallbacks
    {
        public static NetworkCallbacks Instance;

        public event Action OnConnectedToServer;
        public event Action OnRoomJoined;
        public event Action OnRoomCreationFailed;
        public event Action OnRoomLeft;
        public event Action OnNewPlayerEnteredRoom;
        public event Action OnAnyPlayerLeftRoom;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log("Connected to master");
            OnConnectedToServer?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room");
            OnRoomJoined?.Invoke();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("Room joining failed");
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed creating room");
            OnRoomCreationFailed?.Invoke();
        }

        public override void OnLeftRoom()
        {
            Debug.Log("Left room");
            OnRoomLeft?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("New player entered room");
            OnNewPlayerEnteredRoom?.Invoke();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left room");
            OnAnyPlayerLeftRoom?.Invoke();
        }
    }
}
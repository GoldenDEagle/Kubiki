using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.Services.Network
{
    public class NetworkService : INetworkService
    {
        public void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateNewRoom()
        {
        }

        public void JoinRandomRoom()
        {
            //int randomNumber = Random.Range(0, 10000);
            //RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };

            PhotonNetwork.JoinRandomOrCreateRoom();
        }
    }
}

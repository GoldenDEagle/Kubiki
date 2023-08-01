using Assets.Codebase.Infrastructure.Services.Container;

namespace Assets.Codebase.Infrastructure.Services.Network
{
    public interface INetworkService : IService
    {
        public void ConnectToServer();
        public void JoinRandomRoom();
        public void CreateNewRoom();
    }
}

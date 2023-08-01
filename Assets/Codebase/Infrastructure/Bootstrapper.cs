using Assets.Codebase.Infrastructure.Services.Container;
using Assets.Codebase.Infrastructure.Services.GameStates;
using Assets.Codebase.Infrastructure.Services.Network;
using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Codebase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
            RegisterServices();
            ServiceLocator.Container.Single<INetworkService>().ConnectToServer();
            LoadMenu();
        }

        private void RegisterServices()
        {
            ServiceLocator.Container.RegisterSingle<IGameStateMachine>(new GameStateMachine());
            ServiceLocator.Container.RegisterSingle<INetworkService>(new NetworkService());
        }

        private void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
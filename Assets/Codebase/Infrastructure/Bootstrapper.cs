using Assets.Codebase.Infrastructure.Services.Container;
using Assets.Codebase.Infrastructure.Services.GameStates;
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
            LoadMenu();
        }

        private void RegisterServices()
        {
            ServiceLocator.Container.RegisterSingle<IGameStateMachine>(new GameStateMachine());
        }

        private void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
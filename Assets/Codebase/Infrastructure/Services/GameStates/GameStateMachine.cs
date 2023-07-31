using Assets.Codebase.Infrastructure.Services.GameStates;
using System;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private GameState _state = GameState.Idle;
        public GameState State => _state;


        public event Action<GameState> OnBeforeStateEnter;
        public event Action<GameState> OnAfterStateEnter;

        public GameStateMachine()
        {
            
        }

        public void SwitchState(GameState newState, string sceneName = "")
        {
            OnBeforeStateEnter?.Invoke(newState);

            if (sceneName != "")
            {
                SceneManager.LoadScene(sceneName);
            }

            _state = newState;

            OnAfterStateEnter?.Invoke(newState);
        }

    }
}

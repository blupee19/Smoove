using System;
using UnityEngine;
using Smoove.Utilities;

namespace Smoove.Core
{
    /// <summary>
    /// Central game manager responsible for tracking and transitioning game states.
    /// Attach this to a persistent GameObject in your bootstrap or first scene.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<GameState> OnGameStateChanged;

        [field: SerializeField] public GameState CurrentState { get; private set; } = GameState.MainMenu;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            SetState(CurrentState);
        }

        /// <summary>
        /// Transitions the game to a new state and broadcasts the change.
        /// </summary>
        public void SetState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            OnGameStateChanged?.Invoke(newState);
            Debug.Log($"[GameManager] State changed to: {newState}");
        }
    }

    public enum GameState
    {
        MainMenu,
        Loading,
        Gameplay,
        Paused,
        GameOver
    }
}

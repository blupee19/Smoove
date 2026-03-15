using UnityEngine;
using UnityEngine.InputSystem;

namespace Smoove.Gameplay.Input
{
    /// <summary>
    /// Singleton that reads from the Unity Input System asset and exposes gameplay input state
    /// (move, jump, roll, restart) for other systems to consume.
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset _inputActions;

        [Header("Action Map References")]
        [SerializeField] private string _actionMapName = "Player";

        [Header("Action Name References")]
        [SerializeField] private string _move = "Move";
        [SerializeField] private string _jump = "Jump";
        [SerializeField] private string _roll = "Roll";
        [SerializeField] private string _restart = "Restart";

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _rollAction;
        private InputAction _restartAction;

        /// <summary>Current horizontal move input (-1 to 1).</summary>
        public float MoveInput { get; private set; }

        /// <summary>True while jump is held/triggered.</summary>
        public bool JumpInput { get; private set; }

        /// <summary>Current roll input value.</summary>
        public float RollInput { get; private set; }

        /// <summary>True when restart was triggered.</summary>
        public bool RestartInput { get; private set; }

        /// <summary>Singleton instance. Persists across scenes.</summary>
        public static InputSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (_inputActions == null)
            {
                return;
            }

            var map = _inputActions.FindActionMap(_actionMapName);
            _moveAction = map.FindAction(_move);
            _jumpAction = map.FindAction(_jump);
            _rollAction = map.FindAction(_roll);
            _restartAction = map.FindAction(_restart);
            RegisterInputActions();
        }

        private void OnEnable()
        {
            _moveAction?.Enable();
            _jumpAction?.Enable();
            _rollAction?.Enable();
            _restartAction?.Enable();
        }

        private void OnDisable()
        {
            _moveAction?.Disable();
            _jumpAction?.Disable();
            _rollAction?.Disable();
            _restartAction?.Disable();
        }

        private void RegisterInputActions()
        {
            _moveAction.performed += context => MoveInput = context.ReadValue<float>();
            _moveAction.canceled += context => MoveInput = 0;

            _rollAction.performed += context => RollInput = context.ReadValue<float>();
            _rollAction.canceled += context => RollInput = 0;

            _jumpAction.performed += context => JumpInput = true;
            _jumpAction.canceled += context => JumpInput = false;

            _restartAction.performed += context => RestartInput = true;
            _restartAction.canceled += context => RestartInput = false;
        }
    }
}

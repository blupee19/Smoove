using UnityEngine;
using UnityEngine.InputSystem;

namespace Smoove.Gameplay.Input
{
    /// <summary>
    /// Reads and exposes processed input values from Unity's Input System.
    /// Attach this alongside a PlayerInput component. Set PlayerInput's
    /// "Behavior" to "Send Messages" or "Invoke Unity Events".
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool SprintHeld { get; private set; }
        public bool CrouchHeld { get; private set; }

        // Called by PlayerInput via SendMessage
        public void OnMove(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            LookInput = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            JumpPressed = value.isPressed;
        }

        public void OnAttack(InputValue value)
        {
            AttackPressed = value.isPressed;
        }

        public void OnInteract(InputValue value)
        {
            InteractPressed = value.isPressed;
        }

        public void OnSprint(InputValue value)
        {
            SprintHeld = value.isPressed;
        }

        public void OnCrouch(InputValue value)
        {
            CrouchHeld = value.isPressed;
        }

        /// <summary>
        /// Resets one-shot inputs. Call this at the end of your update loop
        /// after all systems have consumed the input.
        /// </summary>
        public void ConsumeOneShots()
        {
            JumpPressed = false;
            AttackPressed = false;
            InteractPressed = false;
        }
    }
}

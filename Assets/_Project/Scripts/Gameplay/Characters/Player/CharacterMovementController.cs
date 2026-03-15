using UnityEngine;
using UnityEngine.InputSystem;

namespace Smoove.Gameplay.Characters.Player
{
    /// <summary>
    /// Handles movement for the player character.
    /// </summary>
    public class CharacterMovementController : MonoBehaviour
    {
        [Tooltip("Variable")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _rollSpeed = 3f;
        [SerializeField] private float _jumpForce = 1f;
        [SerializeField] private float _groundCheckDistance = 0.2f;

        [Header("Component References")]
        private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;

        [Tooltip("Input Variables")]
        private float _moveInput;
        private float _rollInput;
        private bool _jumpInput;
        private bool _restartInput;
        private bool _jumpConsumed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            GetInputs();
            Move();
            Roll();
            Jump();
        }

        private void Move()
        {
            if (_rigidbody == null)
            {
                return;
            }

            _rigidbody.AddForce(new Vector2(_moveInput, 0) * _moveSpeed);
        }

        private void Roll()
        {
            if (_rigidbody == null)
            {
                return;
            }

            _rigidbody.AddTorque(_rollInput * _rollSpeed);
        }

        private void Jump()
        {
            if (_rigidbody == null)
            {
                return;
            }

            if (IsGrounded() && _jumpInput && !_jumpConsumed)
            {
                _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                _jumpConsumed = true;
            }
        }

        private bool IsGrounded()
        {
            if (_groundCheck == null)
            {
                return false;
            }

            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckDistance, _groundLayer);
        }
 
        private void GetInputs()
        {
            var input = Smoove.Gameplay.Input.InputSystem.Instance;
            if (input == null)
            {
                return;
            }

            _moveInput = input.MoveInput;
            _rollInput = input.RollInput;
            _restartInput = input.RestartInput;
            _jumpInput = input.JumpInput;

            if (!_jumpInput)
            {
                _jumpConsumed = false;
            }
        }

    }
}

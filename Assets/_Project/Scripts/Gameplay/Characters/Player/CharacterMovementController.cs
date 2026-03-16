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

            // this function uses no momentum, it works as constant force 
            Vector2 vel = _rigidbody.linearVelocity;
            vel.x = _moveInput * _moveSpeed;
            _rigidbody.linearVelocity = vel;

            // the lile below will use the momentum of the character, but it has a friction-y feel to it
            //_rigidbody.AddForce(new Vector2(_moveInput * _moveSpeed, 0), ForceMode2D.Force); 
        }

        private void Roll()
        {
            if (_rigidbody == null)
            {
                return;
            }
            // not included in the touch buttons but use q or e to roll
            _rigidbody.AddTorque(_rollInput * _rollSpeed);
        }

        private void Jump()
        {
            if (_rigidbody == null)
            {
                return;
            }
            // i declared a variable to consume the jump button and then you can only use jump again when it's true
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
            // an empty gameobject is on the foot of the player checking if we're touching the ground
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

            // i think there can be a better way to do this but i'll leave it for now
            if (!_jumpInput)
            {
                _jumpConsumed = false;
            }
        }

    }
}

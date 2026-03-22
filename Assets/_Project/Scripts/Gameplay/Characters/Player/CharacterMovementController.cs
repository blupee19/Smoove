using UnityEngine;

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
        [SerializeField] private float _powerUpJumpForce = 2f;

        [Header("Component References")]
        private Smoove.Gameplay.Systems.PowerUpSystem _powerUp;
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
            _powerUp = GetComponent<Smoove.Gameplay.Systems.PowerUpSystem>();

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

            // this function uses no momentum, it works as constant force (R)
            Vector2 vel = _rigidbody.linearVelocity;
            vel.x = _moveInput * _moveSpeed;
            _rigidbody.linearVelocity = vel;

            // the line below will use the momentum of the character, but it has a friction-y feel to it (R)
            //_rigidbody.AddForce(new Vector2(_moveInput * _moveSpeed, 0), ForceMode2D.Force);
        }

        private void Roll()
        {
            if (_rigidbody == null)
            {
                return;
            }
            // not included in the touch buttons but use q or e to roll (R)
            _rigidbody.AddTorque(_rollInput * _rollSpeed);
        }

        private void Jump()
        {
            if (_rigidbody == null)
            {
                return;
            }
            // i declared a variable to consume the jump button and then you can only use jump again when it's true (R)
            if (IsGrounded() && _jumpInput && !_jumpConsumed)
            {
                // this code is stacking up power ups right now, we can clamp it from 0 to 1 in the future (R)
                // if the player collected a power up, it jumps higher by doin simple addition else it just jumps normally (R)
                // can probably optimise this and make it modular later, this implementation is probably shit (R)
                float jumpForce = _jumpForce;
                if (_powerUp != null && _powerUp.PowerUpCount > 0)
                {
                    jumpForce += _powerUpJumpForce;
                    _powerUp.ConsumeOne();
                }

                _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                _jumpConsumed = true;
            }
        }

        private bool IsGrounded()
        {
            if (_groundCheck == null)
            {
                return false;
            }
            // an empty gameobject is on the foot of the player checking if we're touching the ground (R)
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckDistance, _groundLayer);
        }

        private void GetInputs()
        {
            // this line of code can be called at awake or start because it only needs to be assigned once (R)
            var input = Smoove.Gameplay.Input.InputSystem.Instance;
            if (input == null)
            {
                return;
            }

            _moveInput = input.MoveInput;
            _rollInput = input.RollInput;
            _restartInput = input.RestartInput;
            _jumpInput = input.JumpInput;

            // i think there can be a better way to do this but i'll leave it for now (R)
            if (!_jumpInput)
            {
                _jumpConsumed = false;
            }
        }

    }
}

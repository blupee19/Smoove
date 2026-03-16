using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UIElements;

namespace Smoove.Gameplay.Characters.Player
{
    /// <summary>
    /// Handles player health and collision response (e.g. knockback from enemies).
    /// </summary>
    public class CharacterHealth : MonoBehaviour
    {
        private int _health;
        [SerializeField] private float _knockbackForce = 4f;
        [SerializeField] private float _waitToKill = 0.5f;
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_rigidbody == null || collision.contactCount == 0 || !collision.gameObject.CompareTag("Enemy"))
            {
                return;
            }

            // doesn't work with the constant linear velocity movement, just to add a knockback effect when hitting an obstacle
            Vector2 knockbackDir = collision.GetContact(0).normal;
            _rigidbody.AddForce(knockbackDir * _knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(KnockbackRoutine());
        }

        private IEnumerator KnockbackRoutine()
        {
            // a timer to wait and kill the player and reload the scene, can add particle effects later
            yield return new WaitForSeconds(_waitToKill);
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}

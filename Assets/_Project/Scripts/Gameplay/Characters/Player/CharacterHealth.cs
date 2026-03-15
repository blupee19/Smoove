using UnityEngine;
using UnityEngine.SceneManagement;

namespace Smoove.Gameplay.Characters.Player
{
    /// <summary>
    /// Handles player health and collision response (e.g. knockback from enemies).
    /// </summary>
    public class CharacterHealth : MonoBehaviour
    {
        private int _health;
        private float _knockbackForce = 4f;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_rigidbody == null || !collision.gameObject.CompareTag("Enemy"))
            {
                return;
            }

            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

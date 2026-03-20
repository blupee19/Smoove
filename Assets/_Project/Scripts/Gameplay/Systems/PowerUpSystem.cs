using UnityEngine;

namespace Smoove.Gameplay.Systems
{
    /// <summary>
    /// Holds the player's power-up count and detects collection when a trigger tagged "PowerUp" enters.
    /// </summary>
    public class PowerUpSystem : MonoBehaviour
    {
        private uint _powerUpCount;

        // idk why cursor did this, and then made a public method to use the _powerUpCount variable rather than making it public?? arent we supposed to not make public methods?? (R)
        public int PowerUpCount => (int)_powerUpCount;

        public void ConsumeOne()
        {
            _powerUpCount--;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("PowerUp"))
            {
                return;
            }

            // just incrementing an int when consuming a power up (R)
            Debug.Log("Player hit a power up");
            _powerUpCount++;
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// EnemyCollision
    /// This class implements collision between enemies and player's bullets.
    /// </summary>
    public class EnemyCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                OnDamage(other.transform.position);

                // Destroy bullet.
                Destroy(other.gameObject);
            }
        }


        void OnDamage(Vector3 collisionPoint)
        {
            GetComponentInParent<Enemy>().OnDamage(collisionPoint);
        }
    }
}

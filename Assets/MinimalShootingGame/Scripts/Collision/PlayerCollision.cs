using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// PlayerCollision
    /// This class implements collision between enemies and player.
    /// </summary>
    public class PlayerCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBullet"))
            {
                OnDamage(other.transform.position);

                // Destroy the bullet.
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Enemy"))
            {
                OnDamage(other.transform.position);

                // Destroy the enemy.
                other.GetComponentInParent<Enemy>().DestroyNow();
            }
        }


        void OnDamage(Vector3 collisionPoint)
        {
            GetComponentInParent<Player>().DestroyNow();
        }
    }
}

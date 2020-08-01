using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// Bullet
    /// This class implements a bullet.
    /// It contains...
    /// - Straight type.
    /// - Missile type.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        // Private variables.
        // Default direction.
        Vector3 direction = new Vector3(0, 0, 1);

        // Default speed.
        float speed = 1.0f;

        // If this variable sets true, the bullet will fire as a missile.
        bool isFollowTarget = false;
        // The missile target.
        Transform followTarget = null;


        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }


        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }


        /// <summary>
        /// Turn on the missile system.
        /// After this method had been called, this bullet will find the enemy target and follow it.
        /// </summary>
        public void SetFollowTarget()
        {
            this.isFollowTarget = true;
        }


        /// <summary>
        /// Find a nearest enemy.
        /// If couldn't find one, it returns null.
        /// </summary>
        /// <returns></returns>
        Transform FindNearestEnemy()
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

            if (enemies.Length <= 0)
            {
                return null;
            }

            Enemy nearestEnemy = enemies[0];
            float nearestDistance = float.MaxValue;
            for (int i = 0; i < enemies.Length; ++i)
            {
                float sqrDistance = Vector3.SqrMagnitude(enemies[i].transform.position - transform.position);
                if (sqrDistance < nearestDistance)
                {
                    nearestDistance = sqrDistance;
                    nearestEnemy = enemies[i];
                }
            }

            return nearestEnemy.transform;
        }


        /// <summary>
        /// Find a farthest enemy.
        /// If couldn't find one, it returns null.
        /// </summary>
        /// <returns></returns>
        Transform FindFarthestEnemy()
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

            if (enemies.Length <= 0)
            {
                return null;
            }

            Enemy farthestEnemy = enemies[0];
            float farthestDistance = 0.0f;
            for (int i = 0; i < enemies.Length; ++i)
            {
                float sqrDistance = Vector3.SqrMagnitude(enemies[i].transform.position - transform.position);
                if (sqrDistance > farthestDistance)
                {
                    farthestDistance = sqrDistance;
                    farthestEnemy = enemies[i];
                }
            }

            return farthestEnemy.transform;
        }


        void DetermineMissileTarget()
        {
            if (this.isFollowTarget)
            {
                if (this.followTarget == null)
                {
                    if (Random.Range(0, 100) < 50)
                    {
                        this.followTarget = FindFarthestEnemy();
                    }
                    else
                    {
                        this.followTarget = FindNearestEnemy();
                    }
                }
            }
        }


        void DetermineDirection()
        {
            if (this.followTarget != null)
            {
                // Calculate rotation to target.
                this.direction = (this.followTarget.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(this.direction);

                // This variable represents the quality of the missile.
                // If this value is small, it turns very slowly.
                // If this value is large, it turns very fast to target.
                float angularVelocity = 4.0f;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * angularVelocity);
            }
        }


        void Move()
        {
            Vector3 velocity = transform.forward * this.speed;
            transform.position += Time.deltaTime * velocity;
        }


        void CheckArea()
        {
            // It is destroy when position is over.
            if (transform.position.z >= 10.0f ||
                transform.position.z <= -10.0f ||
                transform.position.x >= 6.0f ||
                transform.position.x <= -6.0f)
            {
                Destroy(gameObject);
            }
        }


        // Update is called once per frame
        void Update()
        {
            DetermineMissileTarget();
            DetermineDirection();
            Move();
            CheckArea();
        }


        /// <summary>
        /// Draw gizmos to target on the editor screen.
        /// </summary>
        //private void OnDrawGizmos()
        //{
        //    if (this.followTarget != null)
        //    {
        //        Gizmos.color = Color.red;
        //        Gizmos.DrawLine(transform.position, transform.position + (this.followTarget.position - transform.position));
        //    }
        //}
    }
}

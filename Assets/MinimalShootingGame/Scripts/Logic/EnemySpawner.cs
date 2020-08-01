using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// EnemySpawner
    /// This class spawns multiple enemies.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy prefab array")]
        [SerializeField]
        Enemy[] prefabEnemies;

        [Header("Delay before spawn enemies")]
        [Space(20)]
        [SerializeField]
        float startDelay = 1.0f;

        [Header("Interval between each wave")]
        [SerializeField]
        float intervalMin = 1.0f;

        [SerializeField]
        float intervalMax = 3.0f;


        [Header("Spawn count per one wave")]
        [SerializeField]
        int countMin = 2;

        [SerializeField]
        int countMax = 5;


        [Header("Spawn area")]
        [SerializeField]
        Vector3 spawnArea = Vector3.one;


        private void OnEnable()
        {
            StartCoroutine(SpawnLoop());
        }


        /// <summary>
        /// Spawns enemies infinite.
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnLoop()
        {
            // Wait for seconds before start.
            if (this.startDelay > 0.0f)
            {
                yield return new WaitForSeconds(this.startDelay);
            }

            while (true)
            {
                RunWave();

                // Wait for the next wave.
                float interval = Random.Range(this.intervalMin, this.intervalMax);
                yield return new WaitForSeconds(interval);
            }
        }


        void RunWave()
        {
            // It determines how many enemies to be spawned on this wave.
            int count = Random.Range(this.countMin, this.countMax + 1);
            for (int i = 0; i < count; ++i)
            {
                // Pick one enemy prefab randomly.
                int enemyIndex = Random.Range(0, this.prefabEnemies.Length);

                // Instantiate it.
                Enemy enemy = GameObject.Instantiate(this.prefabEnemies[enemyIndex], transform, false);

                // Set its position.
                enemy.transform.position = GetRandomPosition();
                enemy.Wakeup();
            }
        }


        /// <summary>
        /// Get the random spawn position inside of the spawn area.
        /// </summary>
        /// <returns></returns>
        Vector3 GetRandomPosition()
        {
            float x = Random.Range(-this.spawnArea.x, this.spawnArea.x);
            float y = Random.Range(-this.spawnArea.y, this.spawnArea.y);
            float z = Random.Range(-this.spawnArea.z, this.spawnArea.z);

            return transform.position + new Vector3(x, y, z);
        }


        /// <summary>
        /// Draw the rectanble of this spawn area for debug.
        /// </summary>
        private void OnDrawGizmos()
        {
            Vector3 leftTop = transform.position + new Vector3(-this.spawnArea.x, this.spawnArea.y, this.spawnArea.z);
            Vector3 rightTop = transform.position + new Vector3(this.spawnArea.x, this.spawnArea.y, this.spawnArea.z);
            Vector3 leftBottom = transform.position + new Vector3(-this.spawnArea.x, this.spawnArea.y, -this.spawnArea.z);
            Vector3 rightBottom = transform.position + new Vector3(this.spawnArea.x, this.spawnArea.y, -this.spawnArea.z);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(rightBottom, leftBottom);
            Gizmos.DrawLine(leftBottom, leftTop);
        }
    }
}

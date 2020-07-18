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


        // Private variables.
        List<Vector3> spawnPoints = new List<Vector3>();


        private void OnEnable()
        {
            MakeSpawnPoints();
            StartCoroutine(SpawnLoop());
        }


        /// <summary>
        /// Make spawn points.
        /// If you want to more spawn points, edit this method.
        /// </summary>
        void MakeSpawnPoints()
        {
            int maxPoints = 5;
            int rowCount = Mathf.CeilToInt(this.countMax / (float)maxPoints);
            rowCount = Mathf.Clamp(rowCount, 1, 3);

            this.spawnPoints.Clear();
            float z = transform.position.z;
            for (int row = 0; row < rowCount; ++row)
            {
                z += 2.0f;
                this.spawnPoints.Add(new Vector3(-3.0f, 0, z));
                this.spawnPoints.Add(new Vector3(-1.5f, 0, z));
                this.spawnPoints.Add(new Vector3(0, 0, z));
                this.spawnPoints.Add(new Vector3(1.5f, 0, z));
                this.spawnPoints.Add(new Vector3(3.0f, 0, z));
            }
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

                // Wait for next wave.
                float interval = Random.Range(this.intervalMin, this.intervalMax);
                yield return new WaitForSeconds(interval);
            }
        }


        void RunWave()
        {
            // Suffle it.
            Utility.Shuffle<Vector3>(this.spawnPoints);

            // It determines how many enemies to be spawned on this wave.
            int count = Random.Range(this.countMin, this.countMax + 1);
            for (int i = 0; i < count; ++i)
            {
                // Pick one enemy prefab randomly.
                int enemyIndex = Random.Range(0, this.prefabEnemies.Length);

                // Instantiate it.
                Enemy enemy = GameObject.Instantiate(this.prefabEnemies[enemyIndex], transform, false);

                // Set its position.
                enemy.transform.position = spawnPoints[i];
                enemy.Wakeup();
            }
        }
    }
}

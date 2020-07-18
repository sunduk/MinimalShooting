using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    public class Player : MonoBehaviour
    {
        [Header("Prefab explosion")]
        [SerializeField]
        GameObject prefabExplosion;


        public void DestroyNow()
        {
            // Instantiate the destroy effect.
            GameObject.Instantiate(this.prefabExplosion, transform.position, Quaternion.identity);

            gameObject.SetActive(false);

            PlayManager.Instance.GameOver();
        }
    }
}

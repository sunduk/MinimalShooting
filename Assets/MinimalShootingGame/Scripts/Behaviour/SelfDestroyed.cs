using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// SelfDestroyed
    /// This class implements auto destroy game object after wait for seconds.
    /// </summary>
    public class SelfDestroyed : MonoBehaviour
    {
        // How long it will wait before destroy.
        [SerializeField]
        float delay = 1.0f;

        // If this variable sets true, this object is deactive.
        [SerializeField]
        bool keepObjectAlive = false;


        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(DelayAndDestroy());
        }


        public void SetDelay(float delay)
        {
            this.delay = delay;
        }


        public void SetUndead(bool isUndead)
        {
            this.keepObjectAlive = isUndead;
        }


        IEnumerator DelayAndDestroy()
        {
            yield return new WaitForSeconds(this.delay);

            if (this.keepObjectAlive)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

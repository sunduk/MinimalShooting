using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    public class MaterialChanger : MonoBehaviour
    {
        [SerializeField]
        Material material;

        [SerializeField]
        float interval = 0.1f;

        [SerializeField]
        bool loop = false;


        Material originalMaterial;


        private void OnEnable()
        {
            Renderer ren = GetComponent<Renderer>();
            this.originalMaterial = ren.sharedMaterial;
            StartCoroutine(PlayMaterial());
        }


        private void OnDisable()
        {
            Renderer ren = GetComponent<Renderer>();
            ren.material = this.originalMaterial;
        }


        IEnumerator PlayMaterial()
        {
            Renderer ren = GetComponent<Renderer>();

            if (this.loop)
            {
                while (true)
                {
                    ren.material = this.material;
                    yield return new WaitForSeconds(this.interval);

                    ren.material = this.originalMaterial;
                    yield return new WaitForSeconds(this.interval);
                }
            }
            else
            {
                ren.material = this.material;
                yield return new WaitForSeconds(this.interval);
            }

            base.enabled = false;
        }
    }
}

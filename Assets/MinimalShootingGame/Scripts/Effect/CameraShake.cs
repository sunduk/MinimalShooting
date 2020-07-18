using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] float _amount = 1.0f;
        [SerializeField] float _duration = 0.5f;

        Vector3 originPos;

        void OnEnable()
        {
            originPos = transform.localPosition;
            StartCoroutine(Shake(this._amount, this._duration));
        }

        public IEnumerator Shake(float _amount, float _duration)
        {
            float timer = 0;
            while (timer <= _duration)
            {
                if (Time.timeScale > 0.0f)
                {
                    Vector3 circle = Random.insideUnitCircle;
                    circle.z = circle.y;
                    circle.y = 0;

                    transform.localPosition = circle * _amount + originPos;

                    timer += Time.deltaTime;
                }

                yield return null;
            }
            transform.localPosition = originPos;

        }
    }
}

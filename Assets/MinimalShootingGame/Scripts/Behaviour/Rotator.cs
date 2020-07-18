using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// Rotator
    /// This class implements to rotate transform.
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        [Header("Turn speed")]
        [SerializeField]
        float turnSpeed = 1.0f;

        [Header("Angle axis")]
        [SerializeField]
        Vector3 axis = Vector3.up;

        [Header("Random or not")]
        [SerializeField]
        bool randomAxis = false;


        private void OnEnable()
        {
            if (this.randomAxis)
            {
                // If the randomAxis sets true, it will be invert rotation axis by 50 percent probably.
                if (Random.Range(0, 100) < 50)
                {
                    this.axis *= -1.0f;
                }
            }
        }


        /// <summary>
        /// When apply speed faster or slower.
        /// </summary>
        /// <param name="rate"></param>
        public void SetSpeedOption(float rate)
        {
            this.turnSpeed *= rate;
        }


        // Update is called once per frame
        void Update()
        {
            // Rotate 360 degree per one seconds.
            // If turnSpeed is less than 1, it will be slower.
            // If turnSpeed is greater than 1, it will be faster.
            transform.Rotate(this.axis, 360.0f * this.turnSpeed * Time.deltaTime);
        }
    }
}

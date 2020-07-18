using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinimalShooting
{
    /// <summary>
    /// CircleMovement
    /// This class implements circle movement on the XZ plane.
    /// </summary>
    public class CircleMovement : MonoBehaviour
    {
        // Radius.
        [SerializeField]
        float radius = 0.5f;

        // It determines how fast turn this can be.
        [SerializeField]
        float rate = 10.0f;


        // Update is called once per frame
        void Update()
        {
            // Use trigonometric functions to implement.
            // Cos for x coords.
            // Sin for z coords.
            float x = Mathf.Cos(Time.time * this.rate) * this.radius;
            float z = Mathf.Sin(Time.time * this.rate) * this.radius;
            transform.localPosition = new Vector3(x, 0.0f, z);
        }
    }
}

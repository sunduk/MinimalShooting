using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MinimalShooting.ControllerPackage
{
    /// <summary>
    /// KeyboardController
    /// </summary>
    public class KeyboardController : MonoBehaviour
    {
        private Vector3 _inputVector;
        public Vector3 InputVector
        {
            get
            {
                return _inputVector;
            }
        }


        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            this._inputVector = new Vector3(h, 0.0f, v);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MinimalShooting.ControllerPackage
{
    /// <summary>
    /// TouchDragController
    /// </summary>
    public class TouchDragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField]
        Canvas canvas;

        [SerializeField]
        Image imageJoystick;

        private Vector3 _inputVector;
        public Vector3 InputVector
        {
            get
            {
                return _inputVector;
            }
        }

        public Vector3 touchPosition { get; private set; }

        public enum TouchState
        {
            PointerDown,
            PointerDrag,
        }
        public TouchState touchState { get; private set; }


        public void OnPointerDown(PointerEventData e)
        {
            MoveKnob(e);
            this.touchPosition = this._inputVector;
            this.touchState = TouchState.PointerDown;
        }


        public void OnDrag(PointerEventData e)
        {
            if (Time.timeScale <= 0.0f)
            {
                return;
            }

            MoveKnob(e);
            this.touchState = TouchState.PointerDrag;
        }


        /// <summary>
        /// Move joystick image.
        /// </summary>
        /// <param name="e"></param>
        void MoveKnob(PointerEventData e)
        {
            Vector2 pos;
            RectTransform rectTransform = GetComponent<RectTransform>();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                e.position,
                e.pressEventCamera,
                out pos))
            {
                pos.x = (pos.x / rectTransform.rect.width);
                pos.y = (pos.y / rectTransform.rect.height);

                Vector3 knobPosition = new Vector3(pos.x, 0, pos.y);
                knobPosition = (knobPosition.magnitude > 1.0f) ? knobPosition.normalized : knobPosition;

                Vector3 joystickPosition = new Vector3(
                    knobPosition.x * (rectTransform.rect.width),
                    knobPosition.z * (rectTransform.rect.height));
                this.imageJoystick.rectTransform.anchoredPosition = joystickPosition;

                this._inputVector = Camera.main.ScreenToWorldPoint(this.imageJoystick.transform.position);
                this._inputVector.y = 0;
            }
        }


        public void OnPointerUp(PointerEventData e)
        {
        }
    }
}

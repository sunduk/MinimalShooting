// original : https://github.com/maydinunlu/virtual-joystick-unity

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MinimalShooting.ControllerPackage
{
    public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        enum JoystickType
        {
            Fixed,
            Floating,
        }

        [SerializeField]
        Canvas canvas;

        [SerializeField]
        JoystickType joystickType = JoystickType.Fixed;

        [SerializeField]
        Image imageBg;

        [SerializeField]
        Image imageJoystick;

        private Vector3 _inputVector;
        public Vector3 InputVector
        {
            get
            {
                return this._inputVector;
            }
        }


        void MoveJoystickToCurrentTouchPosition()
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.transform as RectTransform, Input.mousePosition, this.canvas.worldCamera, out pos);
            this.imageBg.rectTransform.position = this.canvas.transform.TransformPoint(pos);
        }


        public void OnPointerDown(PointerEventData e)
        {
            if (this.joystickType == JoystickType.Floating)
            {
                MoveJoystickToCurrentTouchPosition();
            }

            OnDrag(e);
        }


        public void OnDrag(PointerEventData e)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.imageBg.rectTransform,
                e.position,
                e.pressEventCamera,
                out pos))
            {

                pos.x = (pos.x / this.imageBg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / this.imageBg.rectTransform.sizeDelta.y);

                this._inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
                this._inputVector = (this._inputVector.magnitude > 1.0f) ? this._inputVector.normalized : this._inputVector;

                Vector3 joystickPosition = new Vector3(
                    this._inputVector.x * (this.imageBg.rectTransform.sizeDelta.x * .4f),
                    this._inputVector.z * (this.imageBg.rectTransform.sizeDelta.y * .4f));
                this.imageJoystick.rectTransform.anchoredPosition = joystickPosition;
            }
        }

        public void OnPointerUp(PointerEventData e)
        {
            this._inputVector = Vector3.zero;
            this.imageJoystick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}

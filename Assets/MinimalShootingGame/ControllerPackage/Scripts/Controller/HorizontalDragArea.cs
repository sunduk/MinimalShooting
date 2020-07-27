using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MinimalShooting.ControllerPackage
{
    public class HorizontalDragArea : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
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

        public bool isStop { get; private set; }


        public void OnPointerDown(PointerEventData e)
        {
            OnDrag(e);
        }


        public void OnDrag(PointerEventData e)
        {
            Vector2 pos;
            RectTransform rectTransform = GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                e.position,
                e.pressEventCamera,
                out pos))
            {
                this.isStop = false;

                pos.x = (pos.x / rectTransform.rect.width);

                Vector3 nobPosition = new Vector3(pos.x, 0, 0);
                nobPosition = (nobPosition.magnitude > 1.0f) ? nobPosition.normalized : nobPosition;

                Vector3 joystickPosition = new Vector3(
                    nobPosition.x * (rectTransform.rect.width), 
                    0);
                this.imageJoystick.rectTransform.anchoredPosition = joystickPosition;

                _inputVector = Camera.main.ScreenToWorldPoint(this.imageJoystick.transform.position);
                _inputVector.y = 0;
                _inputVector.z = 0;
            }
        }


        public void OnPointerUp(PointerEventData e)
        {
            this.isStop = true;
        }
    }
}

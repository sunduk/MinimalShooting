using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

namespace MinimalShooting.ControllerPackage
{
    public class SimpleMovement : MonoBehaviour
    {
        // Player movement speed by joystick.
        [SerializeField]
        float joystickMovementSpeed;

        // Player movement speed by keyboard.
        [SerializeField]
        float keyboardMovementSpeed;


        // Private variables.
        Vector3 firstTouchDistance;


        void Update()
        {
            Movement();
        }


        void JoystickMovement()
        {
            Vector3 velocity = GameControllerSetting.Instance.joystick.InputVector * this.joystickMovementSpeed;
            transform.position = transform.position + Time.deltaTime * velocity;

            if (GameControllerSetting.Instance.lookAtByDirection)
            {
                if (velocity.magnitude > 0)
                {
                    Quaternion to = Quaternion.LookRotation(velocity.normalized);
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, to, Time.deltaTime * GameControllerSetting.Instance.turnSpeed);
                }
            }
        }


        void HorizontalJoystickMovement()
        {
            Vector3 targetPosition = GameControllerSetting.Instance.horizonJoystick.InputVector;
            Vector3 myPosition = transform.position;

            targetPosition.y = myPosition.y;
            targetPosition.z = myPosition.z;

            if (GameControllerSetting.Instance.allowTeleport)
            {
                myPosition.x = targetPosition.x;
                transform.position = myPosition;
            }
            else
            {
                Vector3 velocity = Vector3.zero;

                if ((targetPosition - myPosition).magnitude < 0.1f)
                {
                    velocity = Vector3.zero;
                }
                else
                {
                    velocity = (targetPosition - myPosition).normalized * this.joystickMovementSpeed;
                }

                if (GameControllerSetting.Instance.horizonJoystick.isStop)
                {
                    velocity = Vector3.zero;
                }

                transform.position = transform.position + Time.deltaTime * velocity;
            }
        }


        void TouchAndDragMovement()
        {
            if (GameControllerSetting.Instance.touchDragController.touchState == TouchDragController.TouchState.PointerDown)
            {
                Vector3 touchPosition = GameControllerSetting.Instance.touchDragController.touchPosition;
                Vector3 myPosition = transform.position;
                myPosition.y = 0;

                this.firstTouchDistance = (myPosition - touchPosition);
            }
            else if (GameControllerSetting.Instance.touchDragController.touchState == TouchDragController.TouchState.PointerDrag)
            {
                Vector3 input = GameControllerSetting.Instance.touchDragController.InputVector;
                Vector3 myPosition = transform.position;
                Vector3 newPosition = input + this.firstTouchDistance;

                if (GameControllerSetting.Instance.onlyXMovement)
                {
                    newPosition.z = myPosition.z;
                }
                transform.position = newPosition;
            }
        }


        void KeyboardMovement()
        {
            // Make the speed same for each direction straight and diagonal.
            Vector3 input = Vector3.ClampMagnitude(GameControllerSetting.Instance.keyboardController.InputVector, 1.0f);
            Vector3 velocity = input * this.keyboardMovementSpeed;
            transform.position += Time.deltaTime * velocity;
        }


        void Movement()
        {
            switch (GameControllerSetting.Instance.controllerType)
            {
                case ControllerType.Keyboard:
                    KeyboardMovement();
                    break;

                case ControllerType.VirtualJoystick:
                    JoystickMovement();
                    break;

                case ControllerType.VirtualJoystickHorizontal:
                    HorizontalJoystickMovement();
                    break;

                case ControllerType.TouchAndDrag:
                    TouchAndDragMovement();
                    break;
            }

            ClampBoundary();
        }


        void ClampBoundary()
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -5.0f, 5.0f);
            pos.z = Mathf.Clamp(pos.z, -9.5f, 9.5f);
            transform.position = pos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MinimalShooting.ControllerPackage
{
    public enum ControllerType
    {
        // Keybaord movement. WASD, Arrow keys.
        Keyboard,

        // Virtual joystick that every direction allowed.
        VirtualJoystick,

        // Virtual joystick that only horizontal movement allowed.
        VirtualJoystickHorizontal,

        // Virtual point that follow the touch position.
        TouchAndDrag,
    }

    public class GameControllerSetting : SingletonMonobehaviour<GameControllerSetting>
    {
        [Header("Controller")]
        public ControllerType controllerType;


        //--------------------------------------
        // Joystick.
        [Header("----- Joystick -----")]
        public VirtualJoystick joystick;

        // If this variable is false, the player always look at front direction.
        public bool lookAtByDirection = true;
        // It is activated when lookAtByDirection sets true.
        public float turnSpeed = 7.0f;
        //--------------------------------------


        //--------------------------------------
        // Horizontal Drag.
        [Header("----- Horizontal Drag -----")]
        public HorizontalDragArea horizonJoystick;

        // The speed will be ignored when this variable is true.
        public bool allowTeleport = false;
        //--------------------------------------


        //--------------------------------------
        // Touch And Drag.
        [Header("----- Touch And Drag -----")]
        public TouchDragController touchDragController;
        public bool onlyXMovement;
        //--------------------------------------


        //--------------------------------------
        // Keyboard control.
        [Header("----- Keyboard -----")]
        public KeyboardController keyboardController;
        //--------------------------------------


        private void OnEnable()
        {
            ApplyControllerType();
        }


        public void ApplyControllerType()
        {
            // Disable all controllers.
            this.keyboardController.gameObject.SetActive(false);
            this.joystick.gameObject.SetActive(false);
            this.horizonJoystick.gameObject.SetActive(false);
            this.touchDragController.gameObject.SetActive(false);

            // Enable the specific controller.
            switch (this.controllerType)
            {
                case ControllerType.Keyboard:
                    this.keyboardController.gameObject.SetActive(true);
                    break;

                case ControllerType.VirtualJoystick:
                    this.joystick.gameObject.SetActive(true);
                    break;

                case ControllerType.VirtualJoystickHorizontal:
                    this.horizonJoystick.gameObject.SetActive(true);
                    break;

                case ControllerType.TouchAndDrag:
                    this.touchDragController.gameObject.SetActive(true);
                    break;
            }
        }
    }
}

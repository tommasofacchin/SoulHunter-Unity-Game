using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootJoystick : MonoBehaviour
{

    public Transform playerTransform;
    public FixedJoystick joystick;
    public bool isJoystickActive;

    public float horizontal;
    public float vertical;


    private void Update()
    {

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
            isJoystickActive = true;
        }
        else
        {
            horizontal = 0;
            vertical = 0;
            isJoystickActive = false;
        }

    }
}


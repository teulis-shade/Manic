using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    public enum BoundSide 
    { 
        Left,
        Right, 
        Top, 
        Bottom
    }

    public BoundSide bound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CameraController camera = FindObjectOfType<CameraController>();
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null) {
            switch (bound)
            {
                case CameraBound.BoundSide.Left:
                    camera.leftLocked = true;
                    break;

                case CameraBound.BoundSide.Right:
                    camera.rightLocked = true;
                    break;

                case CameraBound.BoundSide.Top:
                    camera.topLocked = true;
                    break;

                case CameraBound.BoundSide.Bottom:
                    camera.botLocked = true;
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CameraController camera = FindObjectOfType<CameraController>();
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            switch (bound)
            {
                case CameraBound.BoundSide.Left:
                    camera.leftLocked = false;
                    break;

                case CameraBound.BoundSide.Right:
                    camera.rightLocked = false;
                    break;

                case CameraBound.BoundSide.Top:
                    camera.topLocked = false;
                    break;

                case CameraBound.BoundSide.Bottom:
                    camera.botLocked = false;
                    break;

                default:
                    break;
            }
        }
    }
}

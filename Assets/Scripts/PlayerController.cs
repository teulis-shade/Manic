using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool moving = false;
    private Vector2 movement = Vector2.zero;
    [SerializeField] private float speed;
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moving = true;
        }
        else if (ctx.canceled)
        {
            moving = false;
        }
        movement = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (moving)
        {
            gameObject.transform.Translate(movement * Time.deltaTime * speed);
        }
    }

    public void OnAttack()
    {

    }

    public void OnAim()
    {

    }
}

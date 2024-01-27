using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private bool moving = false;
    private bool charging = false;
    private bool firing = false;
    private Vector2 movement = Vector2.zero;
    [SerializeField] private float speed;
    [SerializeField] private float chargingSpeed;
    [SerializeField] private GameObject gun;
    [SerializeField] private float scale;
    private float currentCharge = 0f;
    private Bullet bullet;
    private BulletCharger bulletCharger;
    private Vector2 aimingDirection;
    private Meter meter;
    private float sanity;

    //sprites
    //[SerializeField] private SpriteRenderer face;
    [SerializeField] private PlayerAnimator playerAnimator;
    private void Start()
    {
        bullet = FindObjectOfType<Bullet>();
        bullet.gameObject.SetActive(false);
        bulletCharger = FindObjectOfType<BulletCharger>();
        bulletCharger.gameObject.SetActive(false);
        meter = FindObjectOfType<Meter>();
        sanity = 100;
    }
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
        if (charging)
        {
            currentCharge += chargingSpeed * Time.deltaTime;
            currentCharge = meter.UpdateMeterPreview(currentCharge);
            bulletCharger.Charging(currentCharge * scale);
        }
        if (firing)
        {
            firing = false;
            bullet.StartFiring(currentCharge * scale, aimingDirection, bulletCharger.gameObject.transform.position);
            meter.UpdateMeterOut(currentCharge);
            bulletCharger.StopCharging();
            currentCharge = 0f;
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            charging = true;
        }
        else if (ctx.canceled && charging)
        {
            charging = false;
            firing = true;
        }
    }
    public void OnAim(InputAction.CallbackContext ctx)
    {
        Vector2 aimVector = ctx.ReadValue<Vector2>();
        if (ctx.control.path.Equals("/Mouse/position"))
        {
            aimVector = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>()) - gameObject.transform.position;
        }
        aimingDirection = aimVector.normalized;

        //change player sprites
        playerAnimator.ChangeSprite(aimingDirection);
        


        gun.transform.localRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(aimingDirection.y, -aimingDirection.x, 0f));
    }

    public void ReduceSanity(float loss)
    {
        sanity -= loss;
    }
}

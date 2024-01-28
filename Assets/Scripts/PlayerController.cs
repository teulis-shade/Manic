using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private bool moving = false;
    private bool charging = false;
    private bool firing = false;
    private bool grabbing = false;
    private Body grabbedBody = null;
    private Vector2 movement = Vector2.zero;
    [SerializeField] private float speed;
    [SerializeField] private float chargingSpeed;
    [SerializeField] private GameObject gun;
    [SerializeField] private float scale;
    [SerializeField] private float meleeRadius;
    [HideInInspector] [SerializeField] private bool occultGun;
    [SerializeField] private float maxSanity;
    [SerializeField] private float sanityReduction;
    [SerializeField] private float sanityReductionCooldown;
    private CameraController cam;
    private float currentCharge = 0f;
    private Bullet bullet;
    private BulletCharger bulletCharger;
    private Vector2 aimingDirection;
    private Meter meter;
    [SerializeField] private float sanity;

    private Coroutine sanityCoroutine;

    private Animator animator;

    private AudioSource cameraAudioSource;
    private AudioSource playerAudioSource;

    //sprites
    //[SerializeField] private SpriteRenderer face;
    private SpriteRenderer gunSprite;
    public AudioClip normalMusic;
    public AudioClip zootedMusic;
    public AudioClip lessLaugh;
    public AudioClip insaneLaugh;

    public SaveMusic saveMusicPrefab;
    public SaveMusic saveMusic;

    private void Start()
    {
        bullet = FindObjectOfType<Bullet>();
        bullet.gameObject.SetActive(false);
        bulletCharger = FindObjectOfType<BulletCharger>();
        bulletCharger.gameObject.SetActive(false);
        meter = FindObjectOfType<Meter>();
        sanity = maxSanity;
        animator = GetComponent<Animator>();
        gunSprite = gun.GetComponent<SpriteRenderer>();
        cam = FindObjectOfType<CameraController>();
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
        playerAudioSource = GetComponent<AudioSource>();

        //save music

        //float clip_time = Camera.main.GetComponent<AudioSource>().time;
        //SaveMusic saveMusic = FindObjectOfType<SaveMusic>();

        if (FindObjectOfType<SaveMusic>() == null)
        {//first time: instantiate
            print("if i see this twice i say fuck");
            DontDestroyOnLoad(Instantiate(saveMusicPrefab));
            saveMusic = FindObjectOfType<SaveMusic>();
            //get and set
            cameraAudioSource.clip = normalMusic;
            
            cameraAudioSource.Play();
            saveMusic.SetMusicLength(cameraAudioSource.time);
            //float clip_time = cameraAudioSource.time;
        }else{
            //get and start music
            saveMusic = FindObjectOfType<SaveMusic>();
           
            cameraAudioSource.clip = normalMusic;
            
            cameraAudioSource.Play();
            cameraAudioSource.time = saveMusic.GetMusicLength();
        }

        

    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
        if (ctx.performed)
        {
            moving = true;
            animator.SetBool("IsWalking", true);
        }
        else if (ctx.canceled)
        {
            moving = false;
            animator.SetBool("IsWalking", false);
        }
    }

    private void Update()
    {
        if (sanity == 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        cam.UpdatePostProcessing(1 - sanity / maxSanity);

        if (sanity / maxSanity < 0.33f)
        {
            float clip_time = playerAudioSource.time;
            playerAudioSource.clip = insaneLaugh;
            playerAudioSource.Play();
            playerAudioSource.time = clip_time;

        }
        else if (sanity / maxSanity < 0.66f)
        {
            float clip_time = playerAudioSource.time;
            playerAudioSource.clip = lessLaugh;
            playerAudioSource.Play();
            playerAudioSource.time = clip_time;
        }
        else
        {
            playerAudioSource.Stop();
            playerAudioSource.time = 0;
            playerAudioSource.clip = null;
        }

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
        if (grabbing & grabbedBody != null)
        {
            grabbedBody.transform.position = transform.position;
        }

        // Hide gun if the animation if going upwards
        if (occultGun) {
            gunSprite.sortingOrder = -1;
        } else {
            gunSprite.sortingOrder = 1;
        }

        //if (Random.Range(1,8192) == 1){
        Flicker(Random.Range(0f,1f) > (sanity/maxSanity - .5f) * 2f);
        // if (Random.Range(0,1) > sanity/maxSanity){
        //     print("pizza");
        //     Flicker(true);

        // }else{
        //     Flicker(false);
        // }
        
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed & grabbing & grabbedBody != null)
        {
            grabbedBody.Thrown(aimingDirection);
            grabbing = false;
            grabbedBody = null;
        }
        else if (ctx.performed && !grabbing)
        {
            charging = true;
        }
        else if (ctx.canceled && charging && !grabbing)
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
        animator.SetFloat("X", aimingDirection.x);
        animator.SetFloat("Y", aimingDirection.y);
        gun.transform.localRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(aimingDirection.y, -aimingDirection.x, 0f));
    }

    public void OnMelee(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, meleeRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                if (collider.GetComponent<Enemy>().CheckMeleeable())
                {
                    collider.GetComponent<Enemy>().Melee();
                    break;
                }
            }
        }
    }

    public void ReduceSanity(float loss)
    {
        sanity -= loss;
        if (sanity < 0)
        {
            sanity = 0;
        }
        if (sanityCoroutine != null)
        {
            StopCoroutine(sanityCoroutine);
            sanityCoroutine = null;
        }
        sanityCoroutine = StartCoroutine(ReduceSanity());
    }

    IEnumerator ReduceSanity()
    {
        yield return new WaitForSeconds(sanityReductionCooldown);
        while (true)
        {
            sanity += sanityReduction * .01f;
            if (sanity > maxSanity)
            {
                sanity = maxSanity;
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    public void OnGrab(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !grabbing && !charging)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, meleeRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<Body>() != null)
                {
                    if (collider.GetComponent<Body>().enabled == true)
                    {
                        grabbing = true;
                        grabbedBody = collider.GetComponent<Body>();
                        break;
                    }
                }
            }
        }
    }

    public void Flicker(bool flicker){
        //print(flicker);
        foreach (Enemy enemy in Resources.FindObjectsOfTypeAll<Enemy>())
        {
            enemy.Flicker(flicker);
        }
        if (!flicker)
        {
            float clip_time = cameraAudioSource.time;
            cameraAudioSource.clip = normalMusic;
            
            cameraAudioSource.Play();
            cameraAudioSource.time = clip_time;

            saveMusic.SetMusicLength(clip_time);
        }else {
            float clip_time = cameraAudioSource.time;
            cameraAudioSource.clip = zootedMusic;
            
            cameraAudioSource.Play();
            cameraAudioSource.time = clip_time;


            saveMusic.SetMusicLength(clip_time);
        }
        





    }
}

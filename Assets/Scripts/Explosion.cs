using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float sanityLoss;
    List<Enemy> enemySanity;
    PlayerController playerInside;

    private void Start()
    {
        playerInside = null;
        enemySanity = new List<Enemy>();
    }

    public void SetCharge(float charge)
    {
        sanityLoss = charge / 2f;
    }
    public void StartFade(float timer)
    {
        StartCoroutine(Wait(timer));
    }

    IEnumerator Wait(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        foreach (Enemy enemy in enemySanity)
        {
            //Debug.Log(enemy.gameObject.name);
            enemy.LoseSanity(sanityLoss * Time.deltaTime);
        }
        if (playerInside != null)
        {
            playerInside.ReduceSanity(sanityLoss * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemySanity.Add(collision.GetComponent<Enemy>());
        }
        if (collision.GetComponent<PlayerController>())
        {
            playerInside = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemySanity.Remove(collision.GetComponent<Enemy>());
        }
        if (collision.GetComponent<PlayerController>() != null)
        {
            playerInside = null;
        }
    }
}

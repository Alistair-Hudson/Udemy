using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Paramters
    [Header("Enemy")]
    [SerializeField] float health = 100f;
    [SerializeField] float shotCountDown;
    [SerializeField] float minShotTime = 0.2f;
    [SerializeField] float maxShotTime = 3f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionTime = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float volume = 0.7f;
    [SerializeField] int points = 5;
    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = -10f;
    [SerializeField] AudioClip projectileSound;




    // Start is called before the first frame update
    void Start()
    {
        shotCountDown = Random.Range(minShotTime, maxShotTime);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownnShoot();
    }

    private void CountDownnShoot()
    {
        shotCountDown -= Time.deltaTime;
        if (0f >= shotCountDown)
        {
            GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, volume);
            shotCountDown = Random.Range(minShotTime, maxShotTime);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (0 >= health)
        {
            GameObject explosionInstance = Instantiate(deathVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, volume);
            FindObjectOfType<GameSession>().AddToScore(points);
            Destroy(gameObject);
            Destroy(explosionInstance, explosionTime);
        }
    }
}

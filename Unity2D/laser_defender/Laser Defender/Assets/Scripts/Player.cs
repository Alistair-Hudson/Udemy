using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Paramters
    [Header("Player")]
    [SerializeField] float speedTuner = 1f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 500;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float volume = 0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject laser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectilePeriod = 0.1f;
    [SerializeField] AudioClip projectileSound;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // State Variables
    Coroutine shootingCoroutine;
 

    // Start is called before the first frame update
    void Start()
    {
        SetMoveBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speedTuner;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speedTuner;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shootingCoroutine);
        }
    }

    IEnumerator ShootContinuously()
    {
        while (true)
        {
            GameObject laserInstance = Instantiate(laser, transform.position, Quaternion.identity);
            laserInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position, volume);
            yield return new WaitForSeconds(projectilePeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (0 >= health)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, volume);
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}

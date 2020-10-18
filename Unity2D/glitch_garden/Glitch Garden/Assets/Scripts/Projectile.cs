using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float translationSpeed = 1f;
    [SerializeField] int damage = 10;
    [SerializeField] GameObject hitVFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * translationSpeed);
        
    }

    public void SetTranslationSpeed(float newSpeed)
    {
        translationSpeed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();
        var attacker = collision.GetComponent<Attacker>();
        if (attacker && health)
        {
            if (!hitVFX)
            {
                return;
            }
            else
            {
                var hit = Instantiate(hitVFX, transform.position, Quaternion.identity);
                Destroy(hit, 1f);
            }
            health.DealDamage(damage);
            Destroy(gameObject);
        }

    }
}

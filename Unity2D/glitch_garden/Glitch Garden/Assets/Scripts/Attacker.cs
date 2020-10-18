using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float movementSpeed = 1f;

    GameObject currentTarget;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AddAttacker();
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        if (levelController)
        {
            levelController.SubAttacker();
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * movementSpeed);
    }

    public void SetMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
            return;
        }
        Health health = currentTarget.GetComponent<Health>();
        {
            if (health)
            {
                health.DealDamage(damage);
            }
        }
    }
}

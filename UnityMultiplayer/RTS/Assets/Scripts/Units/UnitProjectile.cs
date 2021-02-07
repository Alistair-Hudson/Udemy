using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : NetworkBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] int damage = 20;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float launchForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * launchForce;    
    }

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<NetworkIdentity>(out NetworkIdentity networkIdentity))
        {
            if(networkIdentity.connectionToClient == connectionToClient)
            {
                return;
            }
        }

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DamageReceived(damage);
            DestroySelf();
        }
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}

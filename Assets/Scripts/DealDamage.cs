using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int damage = 5;

    private ulong owner;

    public void SetOwner(ulong owner)
    {
        this.owner = owner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody == null)
        {
            return;
        }

        if(collision.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject netobj))
        {
            if(netobj.OwnerClientId == owner)
            {
                return;
            }
        }

        if(collision.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.takeDamage(damage);
        }
            
    }

    
}

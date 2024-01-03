using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    [field: SerializeField] public int MaxHealth { get; private set; } = 100;

    private bool isDead;

    public Action<Health> onDie;

    public override void OnNetworkSpawn()
    {
        if(!IsServer) return;

        currentHealth.Value = MaxHealth;
    }

    public void takeDamage(int damage)
    {
        modifyHealth(-damage);
    }

    public void restoreHealth(int health)
    {
        modifyHealth(health);
    }

    public void modifyHealth(int health)
    {
        if(isDead) return;

        int newHealth = currentHealth.Value + health;

        currentHealth.Value = Mathf.Clamp(newHealth, 0, MaxHealth);

        if(currentHealth.Value <= 0)
        {
            onDie?.Invoke(this);
            isDead = true;
        }
    }

}

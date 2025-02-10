using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{

    [Header("refs")]
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;

    public override void OnNetworkSpawn()
    {
        if(!IsClient) return;

        health.currentHealth.OnValueChanged += handleHealthChange;

        handleHealthChange(0, health.currentHealth.Value);
    }

    public override void OnNetworkDespawn()
    {
        if(!IsClient) { return; }

        health.currentHealth.OnValueChanged -= handleHealthChange;
    }

    private void handleHealthChange(int oldHealth, int newHealth)
    {
        healthBar.fillAmount = (float)newHealth / health.MaxHealth;
    }
}

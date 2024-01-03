using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinWallet : NetworkBehaviour
{
    public NetworkVariable<int> coinsTotal = new NetworkVariable<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Coin>(out Coin coin))
        {
            int value = coin.Collect();

            if (!IsServer) return;

            coinsTotal.Value += value;
        }
    }
}

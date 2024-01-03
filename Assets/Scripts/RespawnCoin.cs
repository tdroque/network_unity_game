using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCoin : Coin
{
    public event Action<RespawnCoin> OnCollected;

    private Vector3 prePostion;

    private void Update()
    {
        if(prePostion != transform.position)
        {
            prePostion = transform.position;
            Show(true);
        }

    }

    public override int Collect()
    {
        if (!IsServer)
        {
            Show(false);
            return 0;   
        }

        if (collected) return 0;

        collected = true;

        OnCollected?.Invoke(this);

        return coinValue;
    }

    internal void Reset()
    {
        collected = false;
    }
}

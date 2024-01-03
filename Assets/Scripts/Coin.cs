using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

abstract public class Coin : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected int coinValue = 10;
    protected bool collected;

    public abstract int Collect();

    public void SetCoinValue(int value) 
    {
        coinValue = value;
    }

    protected void Show(bool show)
    {
        spriteRenderer.enabled = show;
    }

}

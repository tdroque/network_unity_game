using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAim : NetworkBehaviour
{
    [SerializeField] private Transform turrentTransform;
    [SerializeField] private InputReader input;

    private void LateUpdate()
    {
        if(!IsOwner) return;

        Vector2 mouseScreen = input.aimPostion;
        Vector2 aimWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

        turrentTransform.up = new Vector2(aimWorld.x - turrentTransform.position.x, aimWorld.y - turrentTransform.position.y);

    }
}

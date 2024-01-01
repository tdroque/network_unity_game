using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodytransform;
    [SerializeField] private Rigidbody2D body;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float rotationSpeed = 360f;

    private Vector2 pMovement;
    public override void OnNetworkSpawn() 
    { 
        if(!IsOwner) return;
        inputReader.moveEvent += HandleMovement;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return; 
        inputReader.moveEvent -= HandleMovement;
    }

    void Update()
    {
        if (!IsOwner) return;

        bodytransform.Rotate(0f, 0f, pMovement.x * -rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        body.velocity = (Vector2) bodytransform.transform.up * pMovement.y * movementSpeed;
    }

    private void HandleMovement(Vector2 input)
    {
        pMovement = input;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletFire : NetworkBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject serverBullet;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject flash;
    [SerializeField] private Collider2D playerCollider;

    [Header("Settings")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate;
    [SerializeField] private float flashTime = 0.5f;

    private bool bulletEnabled = false;
    private float preShoot;
    private float flashTimer;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        input.primaryFireEvent += HandlePrimaryFire;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        input.primaryFireEvent += HandlePrimaryFire;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;

            if(flashTimer <= 0f)
            {
                flash.SetActive(false);
            }
        }

        if(!IsOwner) return;

        if(!bulletEnabled) return;

        if(Time.time < 1 / fireRate + preShoot) return;

        BulletServerRpc(bulletSpawnPoint.position, bulletSpawnPoint.up);

        SpawnBullet(bulletSpawnPoint.position, bulletSpawnPoint.up);

        preShoot = Time.time;

    }

    [ServerRpc]
    private void BulletServerRpc(Vector3 spawn, Vector3 direction)
    {
        GameObject bullet = Instantiate(serverBullet, spawn, Quaternion.identity);

        bullet.transform.up = direction;

        Physics2D.IgnoreCollision(playerCollider, bullet.GetComponent<Collider2D>());

        if(bullet.TryGetComponent<DealDamage>(out DealDamage damage))
        {
            damage.SetOwner(OwnerClientId);
        }
               
        if (bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * bulletSpeed;
        }

        BulletClientRpc(bulletSpawnPoint.position, bulletSpawnPoint.up);

    }

    [ClientRpc]
    private void BulletClientRpc(Vector3 spawn, Vector3 direction)
    {
        if(IsOwner) return;
        SpawnBullet(spawn, direction);
    }

    private void SpawnBullet(Vector3 spawn, Vector3 direction)
    {
        flash.SetActive(true);
        flashTimer = flashTime;

        GameObject bullet = Instantiate(playerBullet , spawn, Quaternion.identity);

        bullet.transform.up = direction;

        Physics2D.IgnoreCollision(playerCollider, bullet.GetComponent<Collider2D>());

        if(bullet.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * bulletSpeed;
        }

    }

    void HandlePrimaryFire(bool shoot)
    {
        this.bulletEnabled = shoot;
    }
}

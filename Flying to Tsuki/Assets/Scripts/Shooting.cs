using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletForce = 20f;
    public float FireRate = 2f;
    public AudioClip BulletSound;

    private Vector3 shootDirection;
    private float nextTimeToShoot = 0f;


    private void Update()
    {
        if(Time.time >= nextTimeToShoot)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                nextTimeToShoot = Time.time + 1f / FireRate;
            }
        }
        
    }

    private void Shoot()
    {
        GameManager.Instance.AudioSource.PlayOneShot(BulletSound);
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;

        var bulletInstance = Instantiate(BulletPrefab.GetComponent<Rigidbody2D>(), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(shootDirection.x * BulletForce, shootDirection.y * BulletForce);
    }
}

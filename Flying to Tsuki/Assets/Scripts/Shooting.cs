using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Rigidbody2D ShootPoint;
    public float BulletForce = 20f;
    public float FireRate = 2f;
    public AudioClip BulletSound;
    public Joystick Joystick;
    //private Vector2 shootDirection;
    private float nextTimeToShoot = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Time.time >= nextTimeToShoot)
        {
            if (Joystick.Horizontal >= 0.01f || Joystick.Vertical >= 0.01f || Joystick.Horizontal <= -0.01f || Joystick.Vertical <= -0.01f)
            {
                Shoot();
                nextTimeToShoot = Time.time + 1f / FireRate;
            }
        }
    }

    private void Shoot()
    {
        GameManager.Instance.AudioSource.PlayOneShot(BulletSound);

        var bullet = Instantiate(BulletPrefab, transform.position, ShootPoint.transform.rotation);
        var bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(ShootPoint.transform.up * BulletForce, ForceMode2D.Impulse);
        


    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(Joystick.Direction.y, Joystick.Direction.x) * Mathf.Rad2Deg - 90f;
        ShootPoint.rotation = angle;
    }
}

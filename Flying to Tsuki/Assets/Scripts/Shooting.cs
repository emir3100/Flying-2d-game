using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Linq;

public class Shooting : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletForce = 20f;
    public float FireRate = 2f;
    public AudioClip BulletSound;
    private Vector2 shootDirection;
    private float nextTimeToShoot = 0f;
    private Rigidbody2D rb;
    public bool canShoot;
    private Vector2 shootPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (Time.time >= nextTimeToShoot)
        {
            if (Input.GetButton("Fire1") && canShoot)
            {
                Shoot();
                nextTimeToShoot = Time.time + 1f / FireRate;
            }
        }
    }

    private void Shoot()
    {
        GameManager.Instance.AudioSource.PlayOneShot(BulletSound);
        shootDirection = shootPos;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - rb.position;
        shootDirection = shootDirection.normalized;

        var bulletInstance = Instantiate(BulletPrefab.GetComponent<Rigidbody2D>(), transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(shootDirection.x * BulletForce, shootDirection.y * BulletForce);
    }

    private void FixedUpdate()
    {
        canShoot = false;
        var touches = Input.touches.Where(x => x.fingerId != JoystickTouch.FirstTriggerId);
        
        if (touches.Count() > 0) 
        {
            canShoot = true;
            shootPos = touches.First().position;
        }



        //if (Input.touchCount > 0)
        //{

        //foreach (var item in Input.touches)
        //{
        //    Debug.Log($"phaes:{item.phase}, finger:{item.fingerId}");
        //}
        //    var finger2 = Input.GetTouch(1);

        //    if (EventSystem.current.IsPointerOverGameObject(finger2.fingerId) && !EventSystem.current.IsPointerOverGameObject(finger1.fingerId))
        //        canShoot = false;
        //    else
        //        canShoot = true; YES

        //    if (finger2.phase == TouchPhase.Ended)
        //        canShoot = false;

        //}

            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || Input.GetTouch(0).phase == TouchPhase.Moved)
            //{
            //}
        //Debug.Log(Input.GetTouch(0).phase);


    }



}

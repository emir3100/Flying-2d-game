using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float DetectionRadius = 0.45f;
    public LayerMask PlayerLayer;
    public GameObject HitEffect;

    public AudioClip PlayerDeath;
    public AudioClip HitSound;
    
    private Transform otherTarget;
    private Transform target;
    private static bool playerDead;

    private static Enemy instance;
    public static Enemy Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Enemy>();
            return instance;
        }
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        otherTarget = GameObject.FindWithTag("OtherTarget").transform;
        playerDead = GameManager.Instance.PlayerDead;
    }

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        float step = MoveSpeed * Time.deltaTime;

        if (!playerDead)
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        else
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;
            transform.position = Vector2.MoveTowards(transform.position, otherTarget.position, step);
        }
    }

    private void Attack()
    {
        var hitPlayer = Physics2D.OverlapCircleAll(this.transform.position, DetectionRadius, PlayerLayer);
        var player = hitPlayer.FirstOrDefault();

        if (player != null)
            PlayerDie(player);
    }

    private void PlayerDie(Collider2D player)
    {
        GameManager.Instance.AudioSource.PlayOneShot(PlayerDeath);
        GameManager.Instance.AudioSource.PlayOneShot(HitSound);
        Destroy(HitEffect.gameObject, 5f);
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<Shooting>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<CircleCollider2D>().enabled = false;
        player.GetComponent<IKManager2D>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        foreach(Transform child in player.transform)
            child.gameObject.SetActive(false);
        GameManager.Instance.PlayerDead = true;
        Instantiate(HitEffect, transform.position, Quaternion.identity);
        playerDead = true;
        GameManager.Instance.StartCoroutine(GameManager.Instance.ShowMenu());
    }


}

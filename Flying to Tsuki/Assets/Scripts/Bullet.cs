using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject HitEffect;
    public AudioClip HitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("method called!");
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);
            GameManager.Instance.AudioSource.PlayOneShot(HitSound);
            GameManager.Instance.Points += 1;
            Destroy(collision.gameObject);
        }

        Destroy(this.gameObject, 5f);
    }

}

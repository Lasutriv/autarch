﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage = 10;
    public float speed = 20f;
    public Rigidbody2D rb;
    public CharacterController2D firedBy;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: Circle cast and to do damage as explosion
        Debug.Log("Boom");
        //var damagedPlayer = collision.GetComponent<CharacterController2D>();

        //if (damagedPlayer != null)
        //{
        //    damagedPlayer.TakeDamage(damage, firedBy);
        //}

        Destroy(gameObject);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private const float MOVEMENT_THRESHHOLD = 0.1f;
    [SerializeField] private const float DESTROY_WAIT = 1f;
    [SerializeField] private GameObject explosionArea;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 vec, ForceMode2D mode)
    {
        rb.AddForce(vec, mode);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < MOVEMENT_THRESHHOLD)
        {
            Destroy(this.gameObject, DESTROY_WAIT);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explosion = Instantiate(explosionArea, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(explosion, 0.1f);
    }

}

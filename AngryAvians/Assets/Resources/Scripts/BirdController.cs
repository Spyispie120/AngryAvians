﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    protected SpriteRenderer sr;

    public Rigidbody2D RB { get; private set; }
    public SpringJoint2D SpringJoint { get; private set; }

    public const string DIRECTORY = "Sprites/Avians/DefaultAvian/";
    [SerializeField] protected Sprite defaultSprite;
    [SerializeField] protected Sprite flyingSprite;

    protected bool isFlying;


    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        SpringJoint = GetComponent<SpringJoint2D>();
        //anchor = springJoint.attachedRigidbody.transform.GetChild(0);

        Debug.Log(defaultSprite);
        Debug.Log(flyingSprite);
    }

    protected virtual void Start()
    {
        isFlying = false;
        SetSprite(DIRECTORY);
    }

    protected void SetSprite(String dir)
    {
        sr = this.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        defaultSprite = sr.sprite;
        flyingSprite = Resources.Load<Sprite>(dir + "avian_fly") as Sprite;
    }

    public void Release()
    {
        sr.sprite = flyingSprite;
        isFlying = true;
    }


    
}

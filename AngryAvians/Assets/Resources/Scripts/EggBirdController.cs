using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;

public class EggBirdController : BirdController
{
    public new const string DIRECTORY = "Sprites/Avians/EggAvian/";
    [SerializeField] private float EGG_FORCE = 5f;
    [SerializeField] private GameObject eggGO;
    [SerializeField] private float eggCount = 1f;

    private float EGG_DISTANCE = 5f;

    protected override void Start()
    {
        base.Start();
        SetSprite(DIRECTORY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isFlying && eggCount-- > 0)
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Egg egg = Instantiate(eggGO, transform.position + direction.normalized * EGG_DISTANCE, Quaternion.identity).GetComponent<Egg>();
            egg.Push(EGG_FORCE * RB.mass * direction, ForceMode2D.Impulse);
            RB.AddForce(EGG_FORCE * RB.mass * -direction, ForceMode2D.Impulse);
        }
    }
}

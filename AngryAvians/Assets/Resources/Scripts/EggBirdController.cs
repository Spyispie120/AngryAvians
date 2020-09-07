using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;

public class EggBirdController : BirdController
{
    public new const string DIRECTORY = "Sprites/Avians/EggAvian/";
    [SerializeField] public const float EGG_FORCE = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isFlying)
        {
            Vector2 direction = Input.mousePosition - this.transform.position;
            RB.AddForce(EGG_FORCE * RB.mass * -direction);
        }
    }
    
}

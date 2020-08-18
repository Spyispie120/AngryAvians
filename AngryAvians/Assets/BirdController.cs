using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    [SerializeField] 
    private float releaseTime;

    private Rigidbody2D rb;
    private SpringJoint2D springJoint;

    private bool isMouseDown;
    private bool holdBird;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            isMouseDown = true;
        }
        
        if(Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) {
            rb.isKinematic = false;
            isMouseDown = false;
            
            if(holdBird) StartCoroutine(Release(releaseTime));
            holdBird = false;
            
        }

        if(isMouseDown && holdBird)
        {
            rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            rb.isKinematic = true;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D rayHit;

        if(isMouseDown)
        {
            rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
            if(rayHit.collider != null)
            {
                if(rayHit.collider.CompareTag("Bird"))
                {
                    holdBird = true;
                }
            }
        }
    }

    private IEnumerator Release(float time)
    {
        yield return new WaitForSeconds(time);
        springJoint.enabled = false;
        //this.enabled = false;
    }
    
}

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
    [SerializeField]
    private Transform anchor;
    private SpriteRenderer sr;

    private bool isMouseDown;
    private bool holdBird;

    private const string DIRECTORY = "Sprites/Avians/DefaultAvian/";
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite flyingSprite;
    [SerializeField] private float MAX_DISTANCE;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        //anchor = springJoint.attachedRigidbody.transform.GetChild(0);

        sr = this.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        defaultSprite = sr.sprite;
        flyingSprite = Resources.Load<Sprite>(DIRECTORY + "avian_fly") as Sprite;

        Debug.Log(defaultSprite);
        Debug.Log(flyingSprite);
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
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 connectedAnchorWorldPos = anchor.position;// transform.TransformPoint(springJoint.connectedAnchor);
            Debug.Log(connectedAnchorWorldPos);
            Vector2 mouseDir = (mouseWorldPos - connectedAnchorWorldPos).normalized;
            float mouseLength = Mathf.Abs(Vector2.Distance(mouseWorldPos, connectedAnchorWorldPos));//springJoint.distance;//

            Vector2 movePoint = mouseLength <= MAX_DISTANCE ? 
                                mouseWorldPos : 
                                connectedAnchorWorldPos + mouseDir * MAX_DISTANCE;

            Debug.DrawLine(connectedAnchorWorldPos, movePoint, Color.cyan, 1);

            rb.MovePosition(movePoint);
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
        sr.sprite = flyingSprite;
        //this.enabled = false;
    }
    
}

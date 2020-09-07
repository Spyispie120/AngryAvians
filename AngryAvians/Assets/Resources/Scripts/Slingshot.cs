using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Slingshot : MonoBehaviour
{
    private LineRenderer line;

    private int index = 0;
    [SerializeField]
    private BirdController[] birds;
    public BirdController currentBird;

    [SerializeField] private float MAX_DISTANCE;

    [SerializeField]
    private Transform anchor;

    [SerializeField]
    private float releaseTime;

    private bool isMouseDown;
    private bool holdBird;
    

    public Rigidbody2D rb;
    public SpringJoint2D springJoint;

    // Start is called before the first frame update
    void Start()
    {
        currentBird = birds[index++];
        rb = currentBird.RB;
        springJoint = currentBird.SpringJoint;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            isMouseDown = true;
        }


        
        if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse))
        {
            rb.isKinematic = false;
            isMouseDown = false;

            // Release
            if (holdBird)
            {
                StartCoroutine(Release(releaseTime));
            }
            holdBird = false;

        }

        if (isMouseDown && holdBird)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 connectedAnchorWorldPos = anchor.position;// transform.TransformPoint(springJoint.connectedAnchor);
            Debug.Log(connectedAnchorWorldPos);
            Vector2 mouseDir = (mouseWorldPos - connectedAnchorWorldPos).normalized;
            float mouseLength = Mathf.Abs(Vector2.Distance(mouseWorldPos, connectedAnchorWorldPos));//springJoint.distance;//

            Vector2 movePoint = mouseLength <= MAX_DISTANCE ?
                                mouseWorldPos :
                                connectedAnchorWorldPos + mouseDir * MAX_DISTANCE;

            //Debug.DrawLine(connectedAnchorWorldPos, movePoint, Color.cyan, 1);

            rb.MovePosition(movePoint);
            rb.isKinematic = true;
        }

    }



    private void FixedUpdate()
    {
        RaycastHit2D rayHit;

        if (isMouseDown)
        {
            rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
            if (rayHit.collider != null)
            {
                if (rayHit.collider.CompareTag("Bird"))
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
        yield return new WaitForSeconds(1f);
        if (index < birds.Length)
        {
            currentBird = birds[index++];
            currentBird.transform.position = anchor.position;
            rb = currentBird.RB;
            springJoint = currentBird.SpringJoint;
        }
    }

}

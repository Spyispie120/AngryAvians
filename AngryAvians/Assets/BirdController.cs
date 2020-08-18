using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BirdController : MonoBehaviour
{
    [SerializeField] 
    private float releaseTime;

    private Rigidbody2D rb;
    private SpringJoint2D springJoint;

    private bool isMouseDown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            isMouseDown = true;
        }
        
        if(Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) {
            StartCoroutine(Release(releaseTime));
            isMouseDown = false;
        }

        if(isMouseDown)
        {
            rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }
    }

    private IEnumerator Release(float time)
    {
        yield return new WaitForSeconds(time);
        springJoint.enabled = false;
        this.enabled = false;
    }
    
}

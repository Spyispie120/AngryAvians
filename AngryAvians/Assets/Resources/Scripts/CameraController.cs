using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Slingshot slingshot;
    [SerializeField] private Vector2 slingshotOffset;
    private Camera cam;

    private Vector3 originalPosition;
    [SerializeField] private float panSpeed = 0.5f;
    [SerializeField] private float lerpSpeed = 2f;
    private bool isMouseDown;
    private Vector3 oldPosition;
    private Vector3 oldPositionScreen;
    private BoxCollider2D bound;
    private Vector2 boundX, boundY;

    public bool lerpToBird;


    [SerializeField] private Vector2 cameraSize;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
        cam = this.GetComponent<Camera>();
        bound = GetComponent<BoxCollider2D>();
        boundX = new Vector2(this.transform.position.x - bound.size.x / 2, this.transform.position.x + bound.size.x / 2);
        boundY = new Vector2(this.transform.position.y - bound.size.y / 2, this.transform.position.y + bound.size.y / 2);
        Debug.Log(boundX);
        Debug.Log(boundY);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mouseScroll = Input.mouseScrollDelta;

        float val = cam.orthographicSize + -mouseScroll.y;
        cam.orthographicSize =  val < cameraSize.x ? cameraSize.x : val > cameraSize.y ? cameraSize.y : val;

        if(Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            isMouseDown = true;
            oldPosition = transform.position;
            oldPositionScreen = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse))
        {
            isMouseDown = false;
        }

        if(!slingshot.HoldBird)
        {
            
            if (isMouseDown)
            {
                Vector3 currentClick = Input.mousePosition;
                Vector3 different = oldPositionScreen - currentClick;

                float x = oldPosition.x + different.x * panSpeed;
                float y = oldPosition.y + different.y * panSpeed;
                LerpCameraBound(x, y);
            }
            else if(slingshot.releasedBird != null)
            {
                if(lerpToBird)
                {
                    float x = slingshot.releasedBird.transform.position.x;
                    float y = slingshot.releasedBird.transform.position.y;
                    LerpCameraBound(x, y);
                }
               
            }

            // uncomment to lerp back to original position
            //else
            //{
            //    this.transform.position = Vector3.Lerp(this.transform.position, originalPosition, Time.deltaTime);
            //}
        } else
        {
            // slingshot lerp
            Vector3 slingshotPosition = new Vector3(slingshot.transform.position.x + slingshotOffset.x, slingshot.transform.position.y + slingshotOffset.y, cam.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, slingshotPosition, Time.deltaTime);
        }


    }

    private void LerpCameraBound(float x, float y)
    {
        Vector3 camPos = this.transform.position;
        Vector3 newPosition = new Vector3(x < boundX.x ? boundX.x : x > boundX.y ? boundX.y : x,
                                  y < boundY.x ? boundY.x : y > boundY.y ? boundY.y : y, camPos.z);

        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, Time.deltaTime * lerpSpeed);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton
    private static Paddle _instance;
    public static Paddle Instance => _instance;
    public bool PaddleIsTransforming { get; set; }=true;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    #endregion
    private Camera mainCamera;
    private float paddleIntialY;
    private float defaultPaddleWithInPixels = 200;
    private float defaultLeftClamp = 135;
    private float defaultRightClamp = 410;
    private SpriteRenderer sr;
    public float extendShrinkDuration = 10f;
    public float paddleWidth = 2f;
    public float paddleHeight = 0.2f;
    private BoxCollider2D boxCollider;
    void Start()
    {

        mainCamera = FindObjectOfType<Camera>();
        paddleIntialY = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();


    }

    void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {

        float paddleShift = (defaultPaddleWithInPixels - ((defaultPaddleWithInPixels / 2) * this.sr.size.x)) / 2;
        //  Debug.Log(paddleShift);
        float leftClamp = defaultLeftClamp - paddleShift;
        float rightClamp = defaultRightClamp + paddleShift;

        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        // Debug.Log(Input.mousePosition.x);
        //Debug.Log("mousePositionPixels"+mousePositionPixels);
        float mousePositionX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        // Debug.Log("mousePositionX"+mousePositionX);
        this.transform.position = new Vector3(mousePositionX, paddleIntialY, 0);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
            ballRb.velocity = Vector2.zero;
            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
        }
    }
    public void StartWidthAnimation(float newWidth)
    {
        StartCoroutine(AnimatePaddleWidth(newWidth));
    }

    public IEnumerator AnimatePaddleWidth(float width)
    {
        this.PaddleIsTransforming = true;
        this.StartCoroutine(ResetPaddleWidthAfterTime(this.extendShrinkDuration));
       
        if (width > this.sr.size.x)
        {     float currentwidth = this.sr.size.x;
            while (currentwidth < width)
            {  
                currentwidth += Time.deltaTime * 2;
                this.sr.size = new Vector2(currentwidth, paddleHeight);
                boxCollider.size = new Vector2(currentwidth, paddleHeight);
                yield return null;
            }
        }
        else
        {   float currentwidth = this.sr.size.x;
            while (currentwidth > width)
            {
                currentwidth -= Time.deltaTime * 2;
                this.sr.size = new Vector2(currentwidth, paddleHeight);
                boxCollider.size = new Vector2(currentwidth, paddleHeight);
                yield return null;
            }
        }
        this.PaddleIsTransforming=false;
    }

    private IEnumerator ResetPaddleWidthAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.StartWidthAnimation(this.paddleWidth);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Camera mainCamera;
    private float paddleIntialY;
    private float defaultPaddleWithInPixels = 200;
    private float defaultLeftClamp = 135;
    private float defaultRightClamp = 410;
    private SpriteRenderer sr;

    void Start()
    {

        mainCamera = FindObjectOfType<Camera>();
        paddleIntialY = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();



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
}

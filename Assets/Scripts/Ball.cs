using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> OnBallDeath;
    public static event Action<Ball> OnLightningBallEnable;
    public static event Action<Ball> OnLightningBallDisable;
    public SpriteRenderer sr;
    public bool isLigthiningBall;
    public ParticleSystem ligthiningEffect;
    public float lightingBallDuraction = 10f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    internal void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);

    }

    public void StartLightning()
    {
        if (!isLigthiningBall)
        {
            this.isLigthiningBall = true;
            this.sr.enabled = false;
            ligthiningEffect.gameObject.SetActive(true);
            OnLightningBallEnable?.Invoke(this);
            StartCoroutine(StopLightningAfterTime(this.lightingBallDuraction));
        }
    }

    private IEnumerator StopLightningAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StopLightningBall();
    }

    private void StopLightningBall()
    {
        if (this.isLigthiningBall)
        {
            isLigthiningBall = false;
            this.sr.enabled = true;
            ligthiningEffect.gameObject.SetActive(false);
            OnLightningBallDisable?.Invoke(this);


        }
    }
}


using UnityEngine;
using System;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int hitPoint = 1;
    public static event Action<Brick> OnBrickDestruction;
    public ParticleSystem DestroyEffect;
    private SpriteRenderer sr;
    private void Start()
    {
        this.sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite=BricksManager.Instance.Sprites[this.hitPoint-1];
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }
    private void ApplyCollisionLogic(Ball ball)
    {
        this.hitPoint--;
        if (this.hitPoint <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite=BricksManager.Instance.Sprites[this.hitPoint-1];
        }



    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect,DestroyEffect.main.startLifetime.constant);

    }
}

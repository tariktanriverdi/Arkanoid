
using UnityEngine;
using System;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int hitPoint = 1;
    public static event Action<Brick> OnBrickDestruction;
    public ParticleSystem DestroyEffect;
    private SpriteRenderer sr;
    private void Awake()
    {
        this.sr = this.gameObject.GetComponent<SpriteRenderer>();

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
            BricksManager.Instance.RemainingBricks.Remove(this);
            OnBrickDestruction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite=BricksManager.Instance.Sprites[this.hitPoint-1];
            this.sr.color=BricksManager.Instance.BricksColler[this.hitPoint];
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

    public void Init(Transform containerTransform, Sprite sprite, Color color, int hitPoint)
    {
       this.transform.SetParent(containerTransform);
        this.sr.sprite=sprite;
        this.sr.color=color;
        this.hitPoint=hitPoint;


    }
}

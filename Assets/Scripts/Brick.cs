
using UnityEngine;
using System;
using static UnityEngine.ParticleSystem;
using System.Collections.Generic;

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
            OnBrickDestroy();
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite = BricksManager.Instance.Sprites[this.hitPoint - 1];
            this.sr.color = BricksManager.Instance.BricksColler[this.hitPoint];
        }



    }

    private void OnBrickDestroy()
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float debuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;
        if (buffSpawnChance <= CollectablesManager.Instance.BuffChance)
        {
            alreadySpawned = true;
            Collectable newBuff = this.SpawnCollectable(true);
        }
        if(debuffSpawnChance<=CollectablesManager.Instance.DebuffChance&& !alreadySpawned)
        {
            Collectable newDebuff = this.SpawnCollectable(false);

        }
    }

    private Collectable SpawnCollectable(bool isBuff)
    {
          List<Collectable> collection;

        if(isBuff)
        {
            collection=CollectablesManager.Instance.AvailableBuffs;
        }else{
             collection=CollectablesManager.Instance.AvailableDebuffs;
        }
        int buffIndex=UnityEngine.Random.Range(0,collection.Count-1);
     Collectable prefab= collection[buffIndex];
     Collectable newCollectable=Instantiate(prefab,this.transform.position,Quaternion.identity) as Collectable;
     return newCollectable;
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);

    }

    public void Init(Transform containerTransform, Sprite sprite, Color color, int hitPoint)
    {
        this.transform.SetParent(containerTransform);
        this.sr.sprite = sprite;
        this.sr.color = color;
        this.hitPoint = hitPoint;


    }
}

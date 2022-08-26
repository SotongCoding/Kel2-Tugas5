using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tes ngespawn dengan PubSub. Akan digantikan dengan Unit Action

public class TestSpawn : MonoBehaviour
{
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         PublishSubscribe.Instance.Publish<SpawnBulletMessage>(new SpawnBulletMessage("Player1"));
    //     }
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         PublishSubscribe.Instance.Publish<SpawnBulletMessage>(new SpawnBulletMessage("Player2"));
    //     }
    //     else if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         PublishSubscribe.Instance.Publish<SpawnBombMessage>(new SpawnBombMessage());
    //     }
    // }
}

public struct MessageSpawnBullet
{
    public Transform shooter;
    public Transform bulletOutPos;
    public bool useBouncing;

    public MessageSpawnBullet(Transform shooter, Transform bulletOutPos, bool useBouncing)
    {
        this.shooter = shooter;
        this.bulletOutPos = bulletOutPos;
        this.useBouncing = useBouncing;
    }
}

public struct MessageSpawnBomb
{
    public Transform shooter;

    public MessageSpawnBomb(Transform shooter)
    {
        this.shooter = shooter;
    }
}
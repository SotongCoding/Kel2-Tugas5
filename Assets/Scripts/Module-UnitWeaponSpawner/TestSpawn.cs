using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tes ngespawn dengan PubSub. Akan digantikan dengan Unit Action

public class TestSpawn : MonoBehaviour
{
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     PublishSubscribe.Instance.Publish<SpawnBulletMessage>(new SpawnBulletMessage());
        // }
        // else if (Input.GetKeyDown(KeyCode.D))
        // {
        //     PublishSubscribe.Instance.Publish<SpawnBombMessage>(new SpawnBombMessage());
        // }
    }
}

public struct MessageSpawnBullet
{
    public Transform unit;
    public Vector3 outPos;

    public MessageSpawnBullet(Transform unit, Vector3 outPos)
    {
        this.unit = unit;
        this.outPos = outPos;
    }
}

public struct MessageSpawnBomb
{
    public Transform unit;

    public MessageSpawnBomb(Transform unit)
    {
        this.unit = unit;
    }
}
using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tes ngespawn dengan PubSub. Akan digantikan dengan Unit Action

public class TestSpawn : MonoBehaviour
{
    private void Update()
    {
        // spawn bullet biasa
        if (Input.GetKeyDown(KeyCode.A))
        {
            PublishSubscribe.Instance.Publish<SpawnBulletMessage>(new SpawnBulletMessage("Player1"));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PublishSubscribe.Instance.Publish<SpawnBulletMessage>(new SpawnBulletMessage("Player2"));
        }

        // spawn bouncing bullet
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            PublishSubscribe.Instance.Publish<MessageSpawnBouncingBullet>(new MessageSpawnBouncingBullet("Player1"));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            PublishSubscribe.Instance.Publish<MessageSpawnBouncingBullet>(new MessageSpawnBouncingBullet("Player2"));
        }

        // spawn bomb
        else if (Input.GetKeyDown(KeyCode.D))
        {
            PublishSubscribe.Instance.Publish<SpawnBombMessage>(new SpawnBombMessage());
        }
    }
}

public struct SpawnBulletMessage
{
    public string shooter;

    public SpawnBulletMessage(string shooter)
    {
        this.shooter = shooter;
    }
}

public struct MessageSpawnBouncingBullet
{
    public string shooter;

    public MessageSpawnBouncingBullet(string shooter)
    {
        this.shooter = shooter;
    }
}

public struct SpawnBombMessage { }
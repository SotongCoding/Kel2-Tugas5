using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
using TankU.PubSub;


public class TestSound : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("bullet_explosion"));
            Debug.Log("bullet_explosion");
        }
        if(Input.GetMouseButtonDown(1))
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("bomb_explosion"));
            Debug.Log("bomb_explosion");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("collect"));
            Debug.Log("collect");
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            PublishSubscribe.Instance.Publish<MessagePlaySoundOnce>(new MessagePlaySoundOnce("move"));
            Debug.Log("move Play");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PublishSubscribe.Instance.Publish<MessagePauseSoundOnce>(new MessagePauseSoundOnce("move"));
            Debug.Log("move Pause");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("shoot"));
            Debug.Log("shoot");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("gameover"));
            Debug.Log("gameover");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            PublishSubscribe.Instance.Publish<MessageSoundBgm>(new MessageSoundBgm("mainmenu"));
            Debug.Log("mainmenu");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PublishSubscribe.Instance.Publish<MessageSoundBgm>(new MessageSoundBgm("gameplay"));
            Debug.Log("gameplay");
        }

    }
}

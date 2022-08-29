using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PubSub;
using Agate.MVC.Core;

public class VfxTesting : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bomb_explosion",transform.position));
        if(Input.GetMouseButtonDown(1))
            PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bullet_explosion", transform.position));
        if (Input.GetKeyDown(KeyCode.Space))
            PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("move", transform.position));
    }
}

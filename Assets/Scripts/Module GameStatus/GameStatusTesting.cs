using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
using TankU.GameStatus;
using TankU.PubSub;

public class GameStatusTesting : MonoBehaviour
{
   
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PublishSubscribe.Instance.Publish<MessageStartGameplay>(new MessageStartGameplay());

        }
    }
}

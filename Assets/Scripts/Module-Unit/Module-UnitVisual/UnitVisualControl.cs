using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;

using TankU.PubSub;

namespace TankU.Unit.UnitVisual
{
    [System.Serializable]
    public class UnitVisualControl
    {
        public Animator animator;
        public Transform body;
        [Header("Tank Part")]
        [SerializeField]
        MeshRenderer[] mainRender;
        [SerializeField]
        MeshRenderer[] subRender;
        public Unit thisUnit { get; private set; }

        bool hasMove = false;

        public void Initial(Unit unit)
        {
            thisUnit = unit;
        }

        public void PlayVisual_Move(Vector3 target)
        {
            animator.Play("move");
            var targetRot = Quaternion.LookRotation(target);
            body.rotation = Quaternion.RotateTowards(body.rotation, targetRot, Time.deltaTime * 100);

            if (!hasMove)
            {
                PublishSubscribe.Instance.Publish<MessagePlaySoundOnce>(new MessagePlaySoundOnce("move"));
                PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("move", thisUnit.transform.position));
                hasMove = true;
            }
        }
        public void PlayVisual_Hit()
        {

        }
        public void PlayVisual_Idle()
        {
            animator.Play("idle");
            if (hasMove)
            {
                PublishSubscribe.Instance.Publish<MessagePauseSoundOnce>(new MessagePauseSoundOnce("move"));
                hasMove = false;
            }
        }
        public void SetUnitColor(Color mainColor, Color subColor)
        {
            for (int i = 0; i < mainRender.Length; i++)
            {
                mainRender[i].material.color = mainColor;
            }
            for (int i = 0; i < subRender.Length; i++)
            {
                subRender[i].material.color = subColor;
            }

        }
    }
}

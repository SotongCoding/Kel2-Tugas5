using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Unit.UnitVisual
{
    [System.Serializable]
    public class UnitVisualControl
    {
        public Animator animator;
        public Transform body;
        [Header("Tank Part")]
        [SerializeField]
        MeshRenderer bodyRender;
        [SerializeField]
        MeshRenderer headRender, frontRender;

        [SerializeField] ParticleSystem dustParticel;
        public void PlayVisual_Move(Vector3 target)
        {
            //VFXControl.Instance.PlayVFX("dust", thisUnit.transform.position);
            animator.Play("move");
            var targetRot = Quaternion.LookRotation(target);
            body.rotation = Quaternion.RotateTowards(body.rotation, targetRot, Time.deltaTime * 100);
        }
        public void PlayVisual_Hit()
        {

        }
        public void PlayVisual_Idle()
        {
            animator.Play("idle");
        }
        public void SetUnitColor(Color mainColor, Color subColor)
        {
            headRender.material.color = mainColor;
            frontRender.material.color = mainColor;

            bodyRender.material.color = subColor;

        }
    }
}

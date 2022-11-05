using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : ObjectBase
{
    [SerializeField] Animator animator;
    int ani_isCutedHash;
    private void Start()
    {
        ani_isCutedHash = Animator.StringToHash("±»¿³");
    }
    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger(ani_isCutedHash);
        PlayAudio(0);
    }
    protected override void Dead()
    {
        base.Dead();
        Destroy(gameObject);
    }
}

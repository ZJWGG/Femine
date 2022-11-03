using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStringToHash : MonoBehaviour
{
    public static AnimatorStringToHash Instance;
    Animator animator;
    int ani_WalkHash;
    int ani_CutHash;
    private void Awake()
    {
        Instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ani_WalkHash = Animator.StringToHash("×ßÂ·");
        ani_CutHash = Animator.StringToHash("¹¥»÷");
    }
    public void IsWalking()
    {
        animator.SetBool(AnimatorStringToHash.Instance.ani_WalkHash, true);
    }
    public void IsNotWalking()
    {
        animator.SetBool(AnimatorStringToHash.Instance.ani_WalkHash, false);
    }
    public void IsAttacking()
    {
        animator.SetTrigger(ani_CutHash);
    }

}

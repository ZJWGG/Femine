using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人AI状态
/// </summary>
public enum EnemyState
{
    Idle,
    Move,
    Pursue,//追击
    Attack,
    Hurt,
    Die
}
/// <summary>
/// 野猪AI
/// </summary>
public class PigController : ObjectBase
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CheckCollider checkCollider;

    //行动范围
    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    EnemyState enemyState;
    Vector3 targetPos;
    Transform GetTransform;
    //当状态修改时，进入新状态要做的事情
    public EnemyState EnemyState { 
        get => enemyState;
        set
        {
            enemyState = value;
            switch (enemyState) 
            {
                case EnemyState.Idle:
                    //播放动画
                    //关闭导航
                    //休息一段时间后去巡逻
                    animator.CrossFadeInFixedTime("Idle", 0.25f);
                    navMeshAgent.enabled = false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));
                    break;
                case EnemyState.Move:
                    //播放动画
                    //开启导航
                    //获取巡逻点
                    //移动到指定目标位置
                    animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    targetPos = GetTargetPos();
                    navMeshAgent.SetDestination(targetPos);
                    break;
                case EnemyState.Hurt:
                    animator.CrossFadeInFixedTime("Hurt", 0.25f);
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    break;
                case EnemyState.Die:
                    animator.CrossFadeInFixedTime("Die", 0.25f);
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    break;
                case EnemyState.Attack:
                    animator.CrossFadeInFixedTime("Attack", 0.25f);
                    navMeshAgent.enabled = false;
                    GetTransform.LookAt(PlayerController.Instance.GetTransform.position);
                    break;
                case EnemyState.Pursue:
                    animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    break;
            }

        }
    }
    private void Start()
    {
        EnemyState = EnemyState.Idle;
        GetTransform = GetComponent<Transform>();
        checkCollider.Init(this,10);
    }
    private void Update()
    {
        StateOnUpdate();
    }
    private void StateOnUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.Move:
                if (Vector3.Distance(GetTransform.position, targetPos) < 1.5f)
                {
                    EnemyState = EnemyState.Idle;
                }
                break;
            case EnemyState.Pursue:
                //距离玩家足够近，切换到攻击状态
                if (Vector3.Distance(GetTransform.position, PlayerController.Instance.GetTransform.position) < 1f)
                {
                    EnemyState = EnemyState.Attack;
                }
                //距离遥远，继续追击
                else
                {
                    navMeshAgent.SetDestination(PlayerController.Instance.GetTransform.position);
                }
                break;
        }
    }
    private void GoMove()
    {
        EnemyState = EnemyState.Move;
    }
    private Vector3 GetTargetPos()
    {
        return new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }
    public override void Hurt(int damage)
    {
        if (EnemyState == EnemyState.Die) return;
        CancelInvoke(nameof(GoMove));//取消切换到延迟状态的延迟调用
        base.Hurt(damage);
        if (Hp > 0)//没有死亡，切换到受伤状态
        {
            EnemyState = EnemyState.Hurt;
        }
    }
    protected override void Dead()
    {
        base.Dead();
        EnemyState = EnemyState.Die;
    }
    #region 动画事件
    private void StartHit()
    {
        checkCollider.StartHit();
    }
    private void StopHit()
    {
        checkCollider.StopHit();
    }
    private void StopAttack()
    {
        if (EnemyState != EnemyState.Die)
        {
            EnemyState = EnemyState.Pursue;
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void HurtOver()
    {
        if (EnemyState != EnemyState.Die)
        {
            EnemyState = EnemyState.Pursue;
        }
    }
    #endregion
}

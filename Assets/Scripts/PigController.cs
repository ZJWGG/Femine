using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ����AI״̬
/// </summary>
public enum EnemyState
{
    Idle,
    Move,
    Pursue,//׷��
    Attack,
    Hurt,
    Die
}
/// <summary>
/// Ұ��AI
/// </summary>
public class PigController : ObjectBase
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CheckCollider checkCollider;

    //�ж���Χ
    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    EnemyState enemyState;
    Vector3 targetPos;
    Transform GetTransform;
    //��״̬�޸�ʱ��������״̬Ҫ��������
    public EnemyState EnemyState { 
        get => enemyState;
        set
        {
            enemyState = value;
            switch (enemyState) 
            {
                case EnemyState.Idle:
                    //���Ŷ���
                    //�رյ���
                    //��Ϣһ��ʱ���ȥѲ��
                    animator.CrossFadeInFixedTime("Idle", 0.25f);
                    navMeshAgent.enabled = false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));
                    break;
                case EnemyState.Move:
                    //���Ŷ���
                    //��������
                    //��ȡѲ�ߵ�
                    //�ƶ���ָ��Ŀ��λ��
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
                //��������㹻�����л�������״̬
                if (Vector3.Distance(GetTransform.position, PlayerController.Instance.GetTransform.position) < 1f)
                {
                    EnemyState = EnemyState.Attack;
                }
                //����ңԶ������׷��
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
    #region �����¼�
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
    private void HurtOver()
    {
        if (EnemyState != EnemyState.Die)
        {
            EnemyState = EnemyState.Pursue;
        }
    }
    #endregion
}

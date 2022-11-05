using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ר�����������ײ������
/// </summary>
public class CheckCollider : MonoBehaviour
{
    private ObjectBase owner;
    private int damage;
    private bool canAttack;
    [SerializeField] List<string> enemyTags = new List<string>();
    [SerializeField] List<string> itemTags = new List<string>();
    public void Init(ObjectBase owner,int damage)
    {
        this.owner = owner;
        this.damage = damage;
    }
    /// <summary>
    /// �����˺����
    /// </summary>
    public void StartHit()
    {
        canAttack = true;
    }
    /// <summary>
    /// �ر��˺����
    /// </summary>
    public void StopHit()
    {
        canAttack = false;
        lastAttackObjectList.Clear();
    }
    private List<GameObject> lastAttackObjectList = new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        //������
        if (canAttack)
        {
            //�˴��˺���û�м��������λ&&���˵ı�ǩ�ڵ����б���
            if (!lastAttackObjectList.Contains(other.gameObject)&&enemyTags.Contains(other.tag)){
                lastAttackObjectList.Add(other.gameObject);
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
            return;
        }
        if (itemTags.Contains(other.tag))
        {
            owner.PlayAudio(1);
            Destroy(other.gameObject);
        }
    }
}

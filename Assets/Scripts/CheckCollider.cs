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
    private bool canHit;
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
        canHit = true;
    }
    /// <summary>
    /// �ر��˺����
    /// </summary>
    public void StopHit()
    {
        canHit = false;
        lastAttackObjectList.Clear();
    }
    private List<GameObject> lastAttackObjectList = new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        //������
        if (canHit)
        {
            //�˴��˺���û�м��������λ&&���˵ı�ǩ�ڵ����б���
            if (!lastAttackObjectList.Contains(other.gameObject)&&enemyTags.Contains(other.tag)){
                lastAttackObjectList.Add(other.gameObject);
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
            return;
        }
        //���ʰȡ
        if (itemTags.Contains(other.tag))
        {
            //���񵽵���Ʒtagתö��
            ItemType itemType = System.Enum.Parse<ItemType>(other.tag);
            if (owner.AddItem(itemType))
            {
                owner.PlayAudio(1);
                Destroy(other.gameObject);
            }
        }
    }
}

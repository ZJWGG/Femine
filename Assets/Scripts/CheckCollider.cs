using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 专门用来检测碰撞的物体
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
    /// 开启伤害检测
    /// </summary>
    public void StartHit()
    {
        canHit = true;
    }
    /// <summary>
    /// 关闭伤害检测
    /// </summary>
    public void StopHit()
    {
        canHit = false;
        lastAttackObjectList.Clear();
    }
    private List<GameObject> lastAttackObjectList = new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        //允许攻击
        if (canHit)
        {
            //此次伤害还没有检测过这个单位&&敌人的标签在敌人列表中
            if (!lastAttackObjectList.Contains(other.gameObject)&&enemyTags.Contains(other.tag)){
                lastAttackObjectList.Add(other.gameObject);
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
            return;
        }
        //检测拾取
        if (itemTags.Contains(other.tag))
        {
            //将捡到的物品tag转枚举
            ItemType itemType = System.Enum.Parse<ItemType>(other.tag);
            if (owner.AddItem(itemType))
            {
                owner.PlayAudio(1);
                Destroy(other.gameObject);
            }
        }
    }
}

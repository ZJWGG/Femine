using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField]List<AudioClip> audioClips;
    [SerializeField] float hp;
    /// <summary>
    /// µôÂäµÄÎïÆ·
    /// </summary>
    public GameObject lootObject;

    public float Hp { 
        get => hp; 
        set
        {
            hp = value;
            if (hp<=0)
            {
                hp = 0;
                Dead();
            }
            OnHpUpdate();
        }
    }

    public void PlayAudio(int Index)
    {
        audioSource.PlayOneShot(audioClips[Index]);
    }
    protected virtual void OnHpUpdate() { }
    protected virtual void Dead() 
    {
        if (lootObject!=null)
        {
            Instantiate(lootObject,
                transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.5f), Random.Range(-0.5f, 0.5f)),
                Quaternion.identity,
                null);
        }
    }
    public virtual void Hurt(int damage)
    {
        Hp -= damage;
    }
    public virtual bool AddItem(ItemType itemType)
    {
        return false;
    }


}

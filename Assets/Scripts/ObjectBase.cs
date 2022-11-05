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

    protected void PlayAudio(int Index)
    {
        audioSource.PlayOneShot(audioClips[Index]);
    }
    protected virtual void OnHpUpdate() { }
    protected virtual void Dead() 
    {

    }
    public virtual void Hurt(int damage)
    {
        hp -= damage;
    }


}

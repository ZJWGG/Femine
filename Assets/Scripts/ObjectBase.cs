using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField]List<AudioClip> audioClips;
    [SerializeField] float hp;
    public GameObject lootObject;//µôÂäµÄÎïÆ·

    public float Hp { 
        get => hp; 
        set
        {
            hp = value;
            if (hp <= 0)
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
    protected virtual void Dead() { }


}

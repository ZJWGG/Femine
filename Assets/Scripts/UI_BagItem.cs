using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BagItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Image bg;
    [SerializeField] Image iconImg;

    bool isSelect;
    public ItemDefine itemDefine;

    public bool IsSelect {
        get => isSelect;
        set {
            isSelect = value;
            if (IsSelect)
            {
                bg.color = Color.green;
            }
            else
            {
                bg.color = Color.white;
            }
        } 
    }

    //������ʱ�Ĳ���
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }
    //����˳�ʱ�Ĳ���
    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
    }
    /// <summary>
    /// ��ʼ���������һ��NUll�������൱��һ���ո���
    /// </summary>
    /// <param name="itemDefine"></param>
    public void Init(ItemDefine itemDefine)
    {
        this.itemDefine = itemDefine;
        isSelect = false;
        if (this.itemDefine == null)
        {
            iconImg.gameObject.SetActive(false);
        }
        else
        {
            iconImg.gameObject.SetActive(true);
            iconImg.sprite = itemDefine.Icon;
        }
    }

  
}

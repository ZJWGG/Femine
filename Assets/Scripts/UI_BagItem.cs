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

    //鼠标进入时的操作
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }
    //鼠标退出时的操作
    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
    }
    /// <summary>
    /// 初始化，如果传一个NUll过来，相当于一个空格子
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

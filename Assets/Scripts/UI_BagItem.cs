using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BagItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
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
    private void Update()
    {
        if (IsSelect && itemDefine != null && Input.GetMouseButtonDown(1))
        {
            if (PlayerController.Instance.UseItem(itemDefine.ItemType))
            {
                Init(null);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDefine == null)
        {
            return;
        }
        PlayerController.Instance.isDraging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemDefine == null)
        {
            return;
        }
        iconImg.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemDefine == null)
        {
            return;
        }
        PlayerController.Instance.isDraging = false;
        //�������߲鿴��ǰ����������
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hitInfo))
        {
            string targetTag = hitInfo.collider.tag;
            iconImg.transform.localPosition = Vector3.zero;//Icon��λ
            switch (itemDefine.ItemType)
            {
                case ItemType.Meat:
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);//��������е�item
                    }
                    if (targetTag == "Campfire")
                    {
                        Init(BagItemManager.Instance.GetItemDefine(ItemType.CookedMeat));//ֱ�ӽ������ڵ������滻Ϊ����
                    }
                    break;
                case ItemType.CookedMeat:
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);//��������е�item
                    }
                    if (targetTag == "Campfire")
                    {
                        hitInfo.collider.GetComponent<Campfire>().AddWood();//����ȼ��ʱ��
                        Init(null);//��������е�item
                    }
                    break; 
                case ItemType.Wood:
                    if (targetTag == "Ground") {
                        Instantiate(itemDefine.Prefab, hitInfo.point + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        Init(null);//��������е�item
                    }
                    if(targetTag=="Campfire")
                    {
                        hitInfo.collider.GetComponent<Campfire>().AddWood();//����ȼ��ʱ��
                        Init(null);//��������е�item
                    }
                    break;
                case ItemType.Campfire:
                    if (targetTag == "Ground")
                    {
                        Instantiate(itemDefine.Prefab, hitInfo.point, Quaternion.identity);
                        Init(null);//��������е�item
                    }
                    break;

            }
        }

    }
}

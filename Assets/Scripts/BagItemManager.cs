using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    None,
    Meat,
    CookedMeat,
    Wood,
    Campfire
}

/// <summary>
/// ��Ʒ����
/// </summary>
public class ItemDefine {
    public ItemType ItemType;
    public Sprite Icon;
    public GameObject Prefab;  
    public ItemDefine(ItemType itemType,Sprite icon,GameObject prefab)
    {
        ItemType = itemType;
        Icon = icon;
        Prefab = prefab;
    }
}
/// <summary>
/// ��Ʒ������
/// </summary>
public class BagItemManager : MonoBehaviour
{
    public static BagItemManager Instance;
    [SerializeField] Sprite[] icons;
    [SerializeField] GameObject[] itemPrefabs;
    private void Awake()
    {
        Instance = this;
    }
    public ItemDefine GetItemDefine(ItemType itemType)
    {
        //��ȥһ����ΪItemType�ĵ�һ��ΪNone
        return new ItemDefine(itemType, icons[(int)itemType - 1], itemPrefabs[(int)itemType - 1]);
    }
}

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
/// 物品定义
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
/// 物品管理器
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
        //减去一是因为ItemType的第一个为None
        return new ItemDefine(itemType, icons[(int)itemType - 1], itemPrefabs[(int)itemType - 1]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Bag : MonoBehaviour
{
    public static UI_Bag Instance;
    UI_BagItem[] items;
    [SerializeField] GameObject itemPrefabs;
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        items = new UI_BagItem[5];
        //生成篝火
        UI_BagItem item = Instantiate(itemPrefabs, transform).GetComponent<UI_BagItem>();
        item.Init(BagItemManager.Instance.GetItemDefine(ItemType.Campfire));
        items[0] = item;
        for (int i = 1; i < 5; i++)
        {
            item = Instantiate(itemPrefabs, transform).GetComponent<UI_BagItem>();
            item.Init(null);
            items[i] = item;
        }
    }
    public bool AddItem(ItemType itemType)
    {
        //查看一次背包还有没有空格子
        for(int i = 0; i < items.Length; i++)
        {
            //这是个空格子
            if (items[i].itemDefine == null)
            {
                ItemDefine itemDefine = BagItemManager.Instance.GetItemDefine(itemType);
                items[i].Init(itemDefine);
                return true;
            }
        }
        return false;
    }
}

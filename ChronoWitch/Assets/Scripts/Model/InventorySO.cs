using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItems> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 11;


    public event Action<Dictionary<int, InventoryItems>> OnInventoryUpdated;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItems>();
        for(int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItems.GetEmptyItem());
        }    
    }


    public void AddItem(Item item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].isEmpty)
            {
                inventoryItems[i] = new InventoryItems
                {
                    item = item
                };
                return;
            }
        }
    }


    public void AddItem(InventoryItems item)
    {
        AddItem(item.item);
    }


    public Dictionary<int, InventoryItems> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItems> returnValue = new Dictionary<int, InventoryItems>();
        for(int i = 0; i < inventoryItems.Count;i++)
        {
            if (inventoryItems[i].isEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItems GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public void SwapItems(int itemIndex1, int itemIndex2)
    {
        InventoryItems item1 = inventoryItems[itemIndex1];
        inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
        inventoryItems[itemIndex2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    public void RemoveItem(int itemIndex)
    {
        if(inventoryItems.Count > itemIndex)
        {
            if (inventoryItems[itemIndex].isEmpty)
                return;
            inventoryItems[itemIndex] = InventoryItems.GetEmptyItem();
            InformAboutChange();
        }
    }
}


[Serializable]
public struct InventoryItems
{
    public Item item;
    public bool isEmpty => item == null;

    public static InventoryItems GetEmptyItem() => new InventoryItems
    {
        item = null,
    };

}

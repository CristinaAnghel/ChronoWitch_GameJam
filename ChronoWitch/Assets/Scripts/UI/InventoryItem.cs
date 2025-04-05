using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image borderImage;


    public event Action<InventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;
    [SerializeField] private int craftingIndex;
    [SerializeField] private string itemName;

    private Item storedItem;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }


    public void Deselect()
    {
        borderImage.enabled = false;
    }


    public void SetData(Sprite sprite, string name, Item item)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemName = name;
        this.storedItem = item;
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled=true;
    }


    public void OnBeginDrag()
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }


    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }


    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }


    public void OnPointerClick(BaseEventData data)
    {
        
        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }


    public void SetIndex(int index)
    {
        craftingIndex = index;
    }


    public Item GetStoredItem()
    { return this.storedItem; }
}

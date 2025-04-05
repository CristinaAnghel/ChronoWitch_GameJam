using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryPage : MonoBehaviour
{

    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private InventoryItem craftingPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private InventoryDescription itemDescription;
    [SerializeField] private MouseFollower mouseFollower;
    [SerializeField] private RectTransform craftingPanel;
    [SerializeField] private RectTransform resultPanel;


    List<InventoryItem> listOfItems = new List<InventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

    [SerializeField] private ItemActionPanel actionPanel;

    private int craftingSize = 4;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
        mouseFollower.Toggle(false);
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i < (inventorySize-craftingSize-1); i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClick += HandleShowItemActions;
        }

        
        for(int i = 0; i < craftingSize; i++)
        {
            InventoryItem item = Instantiate(craftingPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(craftingPanel);
            item.SetIndex(i);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClick += HandleShowItemActions;
        }

        
        InventoryItem item1 = Instantiate(craftingPrefab, Vector3.zero, Quaternion.identity);
        item1.transform.SetParent(resultPanel);
        item1.SetIndex(10);
        listOfItems.Add(item1);
        item1.OnItemClicked += HandleItemSelection;
        item1.OnItemBeginDrag += HandleBeginDrag;
        item1.OnItemDroppedOn += HandleSwap;
        item1.OnItemEndDrag += HandleEndDrag;
        item1.OnRightMouseBtnClick += HandleShowItemActions;
        
    }

    internal void ResetAllItems()
    {
        foreach(var item in listOfItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfItems[itemIndex].Select();
    }

    public void UpdateData(int itemIndex, Sprite itemImage, string name, Item item)
    {
        if(listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(itemImage, name, item);
        }
    }

    private void HandleShowItemActions(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if(index == -1)
        {
            return;
        }
        OnItemRequested?.Invoke(index);
    }

    private void HandleEndDrag(InventoryItem obj)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if(index == -1)
        {
            return;
        }

        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(inventoryItem);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;

        HandleItemSelection(inventoryItem);
        OnStartDragging?.Invoke(index);
    }


    public void CreateDraggedItem(Sprite sprite, string name, Item item)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, name, item);
    }

    private void HandleItemSelection(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }


    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButton(actionName, performAction);
    }

    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfItems[itemIndex].transform.position;
    }

    private void DeselectAllItems()
    {
        foreach(InventoryItem item in listOfItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
    }

    public void Hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryUI;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private PlayerAgeSwitcher ageSwitcher;
    [SerializeField] private Timer timer;
    [SerializeField] private PotionItemSO[] potions;
    [SerializeField] private bool[] acquired;
    [SerializeField] private GameObject barrier;
    [SerializeField] private TMPro.TMP_Text potionText;

    //[SerializeField] private int startSize = 0;

    public List<InventoryItems> initialItems = new List<InventoryItems>();

    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip placeClip;
    [SerializeField] private AudioClip drinkPotionClip;


    public int modifyIndex;
    public float timeTillDeath;
    public int addTime;
    public bool canPass = false;
    public int sum = 0;


    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach(InventoryItems item in initialItems)
        {
            Debug.Log(item);
            if (item.isEmpty)
                continue;
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItems> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.item.Name, item.Value.item);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleswapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItems inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction("Drop", () => DropItem(itemIndex));
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if(itemAction != null)
        {
            //inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));

        }

    }

    private void DropItem(int itemIndex)
    {
        inventoryData.RemoveItem(itemIndex);
        inventoryUI.ResetSelection();
        audioSource.PlayOneShot(dropClip);
        inventoryUI.DeleteSize();
        //inventoryUI.MoveInventory();
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItems inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject);
            audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).isEmpty)
                inventoryUI.ResetSelection();

            audioSource.PlayOneShot(drinkPotionClip);
            inventoryUI.DeleteSize();
            PotionItemSO potion = (PotionItemSO)inventoryItem.item;
            modifyIndex = potion.modify;
            timeTillDeath = potion.timeTillDeath;
            addTime = potion.addTime;
            if (addTime == 0)
                ageSwitcher.SwitchAge(modifyIndex, timeTillDeath);
            else
                timer.AddTime(addTime);
            for(int i = 0; i < 6; i++)
            {
                if(acquired[i] == false && potions[i].name.Contains(potion.name))
                {
                    acquired[i] = true;
                }
                
                
            }
            CheckIfPass();
            //Debug.Log(modifyIndex);
        }
    }


    private void HandleDragging(int itemIndex)
    {
        InventoryItems inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty)
            return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.item.Name, inventoryItem.item);
    }

    private void HandleswapItems(int itemIndex1, int itemIndex2)
    {
        audioSource.PlayOneShot(placeClip);
        inventoryData.SwapItems(itemIndex1, itemIndex2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItems inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty)
        {
            inventoryUI.ResetSelection();
            //return;
        }
        Item item = inventoryItem.item;

        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.item.Name, item.Value.item);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }

        //CheckIfPass();
    }


    public void CheckIfPass()
    {
        
        for (int i = 0; i < 6; i++)
            if(acquired[i] == true)
                sum++;
        potionText.text = sum.ToString();
        if (sum == 6)
        {
            barrier.SetActive(false);
            //Destroy(barrier);
        }
    }
}

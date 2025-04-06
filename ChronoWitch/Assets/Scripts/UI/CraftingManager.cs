using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;

    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private InventoryPage inventoryUI;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonParent;
    [SerializeField] private AudioClip brewed;
    [SerializeField] private AudioSource audioSource;
    public event Action<Item> brewPotionClick;

    private GameObject button;
    private Item craftedItem;
    private string currentRecipe;
    private bool foundRecipe;

    public string[] recipes;
    public Item[] recipeResults;
    private InventoryItem resultSlot;

    private void Start()
    {
        //brewPotionClick += HandleBrewPotionClick;
        button = Instantiate(buttonPrefab, buttonParent.transform);
        button.GetComponent<Button>().onClick.AddListener(OnBrewButtonClicked);
        button.GetComponentInChildren<TMPro.TMP_Text>().text = "Brew";
        
    }

    private void HandleBrewPotionClick(Item crafteditem)
    {
        BrewPotion(crafteditem);
    }

    public void Update()
    {
        CheckForCompletedRecipe();
    }


    public void CheckForCompletedRecipe() 
    {
        //resultSlot = inventoryUI.GetComponent<List<InventoryItem>>()[10];
        //resultSlot.gameObject.SetActive(false);
        currentRecipe = "";
        button.active = false;

        for(int i = 6; i < inventoryData.Size; i++)
        {
            if(!inventoryData.GetItemAt(i).isEmpty)
                currentRecipe += inventoryData.GetItemAt(i).item.Name;
        }

        foundRecipe = false;

        for (int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipe)
            {
                if (currentRecipe.Contains(recipes[i]))
                {
                    foundRecipe = true;
                    button.active = true;
                    craftedItem = Instantiate(recipeResults[i]);
                    inventoryUI.UpdateData(10, craftedItem.ItemImage, craftedItem.Name, craftedItem);
                    inventoryUI.UpdateDescription(10, craftedItem.ItemImage, craftedItem.Name, craftedItem.Description);
                    
                    //inventoryData.AddItem(craftedItem);


                    return;
                }
                

            }

        }

        if(!foundRecipe)
        {
            
        }



    }

    private void OnBrewButtonClicked()
    {
        BrewPotion(craftedItem);
            
        craftedItem = null;
        currentRecipe = "";
        foundRecipe = false;
        button.active = false;
        audioSource.PlayOneShot(brewed);
        
    }


    public void BrewPotion(Item craftedItem)
    {
        inventoryData.AddItem(craftedItem);
        inventoryUI.AddSize();

        for (int i = 6; i < 10; i++)
        {
            if (inventoryData.GetItemAt(i).item != null)
            {
                inventoryData.RemoveItem(i);
                inventoryUI.DeleteSize();

            }
        }
        inventoryUI.ResetSelection();

        /*
        if (button != null)
        {
            Destroy(button);
            button = null;
            craftedItem = null;
        }
        */
    }

}  
    

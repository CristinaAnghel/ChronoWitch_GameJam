using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private Item currentItem;

    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private InventoryPage inventoryUI;
    public string[] recipes;
    public Item[] recipeResults;
    private InventoryItem resultSlot;

    public void Update()
    {
        CheckForCompletedRecipe();
    }


    public void CheckForCompletedRecipe() 
    {
        //resultSlot = inventoryUI.GetComponent<List<InventoryItem>>()[10];
        //resultSlot.gameObject.SetActive(false);
        string currentRecipe = "";

        for(int i = 6; i < inventoryData.Size; i++)
        {
            if(!inventoryData.GetItemAt(i).isEmpty)
                currentRecipe += inventoryData.GetItemAt(i).item.Name;
        }

        for(int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipe)
            {
                //resultSlot.gameObject.SetActive(true);

                Item craftedItem = Instantiate(recipeResults[i]);
                inventoryUI.UpdateData(10, craftedItem.ItemImage, craftedItem.Name, craftedItem);
                resultSlot.gameObject.SetActive(true);
                resultSlot.SetData(craftedItem.ItemImage, craftedItem.Name, craftedItem);
                return;
                //resultSlot.gameObject.SetActive(true);
                //return;
                //InventoryItem resultItem = new InventoryItem();

                //resultSlot.GetComponent<Image>().sprite = recipeResults[i].ItemImage;

            }
        }

        resultSlot.ResetData();
        resultSlot.gameObject.SetActive(false);

        //resultSlot.ResetData();

        //Debug.Log(currentRecipe);
    }

}  
    

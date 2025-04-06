using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private InventoryPage inventoryUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUpClip;


    private GameObject itemInRange;
    public Item pickedItem;
    [SerializeField] private Item[] items;
    //[SerializeField] private int startSize = 0;
    private int maxSize = 6;

    private void Start()
    {
        //Debug.Log(items.Length);
    }

    private void Update()
    {
        //Debug.Log(itemInRange);
        
        if (itemInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Picked up item: " + itemInRange.name);
            //Debug.Log(items.Length);

            for(int i = 0; i < items.Length; i++)
            {
                //Debug.Log(items[i].name);
                string name = items[i].name + "(Clone)";
                Debug.Log(name);
                if(name == itemInRange.name)
                {
                    //Debug.Log("YES");
                    if(inventoryUI.getSize() < maxSize)
                    {
                        pickedItem = Instantiate(items[i]);
                        inventoryData.AddItem(pickedItem);
                        int slot = FindFirstEmptySlot();
                        inventoryUI.UpdateData(slot, pickedItem.ItemImage, pickedItem.Name, pickedItem);
                        //Debug.Log(pickedItem.name);
                        Destroy(itemInRange);
                        itemInRange = null;
                        inventoryUI.AddSize();
                        audioSource.PlayOneShot(pickUpClip);
                    }
                    //else { return; }
                    
                }
            }
            //pickedItem = Instantiate(itemInRange.name);
            //inventoryData.AddItem(itemInRange);
            // Don't forget to remove the item from the world!
             // Clear it after picking up
        }
        Debug.Log(inventoryUI.getSize());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemInRange = collision.gameObject;
            Debug.Log(itemInRange.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (itemInRange != null && collision.gameObject == itemInRange)
            {
                itemInRange = null;
            }
        }
    }


    public int FindFirstEmptySlot()
    {
        int slot = 0;
        for (int i = 0; i < 6; i++)
            if (inventoryUI.listOfItems[i] == null)
            {
                slot = i;
                return slot;
            }

        return slot;
    }
}

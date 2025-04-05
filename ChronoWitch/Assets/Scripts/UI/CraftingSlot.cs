using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int index;


    public void SetIndex(int index)
    { this.index = index; }
}

using UnityEngine;

public class FC_SlotItem : MonoBehaviour
{
    public int itemSlot; 

    public void ChargeItem()
    {
        FC_InventoryManager.instance.ChargeItem(itemSlot);
    }
}

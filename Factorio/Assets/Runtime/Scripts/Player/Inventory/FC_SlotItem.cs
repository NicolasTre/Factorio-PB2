using UnityEngine;

public class FC_SlotItem : MonoBehaviour
{
    public int itemSlot;
    public BoxCollider2D? _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void ChargeItem()
    {

        FC_InventoryManager.instance.ChargeItem(itemSlot);

    }
}

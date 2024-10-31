using UnityEngine;

public class FC_SlotItem : MonoBehaviour
{
    public int itemSlot;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        ChargeItem();
    }

    public void ChargeItem()
    {
        FC_InventoryManager.instance.ChargeItem(itemSlot);
    }
}

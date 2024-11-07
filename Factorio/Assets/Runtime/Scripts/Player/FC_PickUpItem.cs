using UnityEngine;

public class FC_PickUpItem : MonoBehaviour
{
    [SerializeField] private Type _itemName;
    private readonly string PATHROOTITEMSO = "Items/ScriptableObject";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FC_ItemSo item = Resources.Load<FC_ItemSo>($"{PATHROOTITEMSO}/{_itemName.ToString()}");

            for (int i = 0; i < FC_InventoryManager.instance.inventory.Count; i++)
            {
                if (item.title == FC_InventoryManager.instance.inventory[i].title && item.isStackable && FC_InventoryManager.instance.inventory.Count > 0)
                {
                    item.quantities += FC_InventoryManager.instance.inventory[i].quantities;
                    FC_InventoryManager.instance.inventory.Remove(FC_InventoryManager.instance.inventory[i]);
                    break;
                }
            }

            FC_InventoryManager.instance.inventory.Add(item);
            Destroy(gameObject);
        }
    }
}

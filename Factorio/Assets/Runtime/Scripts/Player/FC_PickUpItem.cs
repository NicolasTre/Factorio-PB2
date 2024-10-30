using UnityEngine;

public class FC_PickUpItem : MonoBehaviour
{
    [SerializeField] private FC_ItemSo _itemSo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FC_InventoryManager.instance.inventory.Add(_itemSo);
            Destroy(gameObject);
        }
    }
}

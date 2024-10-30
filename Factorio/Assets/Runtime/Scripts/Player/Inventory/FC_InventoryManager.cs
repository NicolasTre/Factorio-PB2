using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class FC_InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject _inventoryPanel, _holderSlot;
    [SerializeField] private GameObject _prefabs;
    [SerializeField] private GameObject _holderDescription;
    [SerializeField] private TextMeshProUGUI _Title, _descriptionObject;
    [SerializeField] private Image _iconDescription;

    public List<FC_ItemSo> inventory;

    private GameObject slot;
    private int _inventoryLenght = 63;


    public static FC_InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void InventoryOpen(InputAction.CallbackContext context) // function for open inventory and create a slot and set item to slot
    {
        context.ReadValue<float>();

        if (!_inventoryPanel.activeInHierarchy)
        {
            _inventoryPanel.SetActive(true);

            if (_holderSlot.transform.childCount > 0)
            {
                foreach (Transform item in _holderSlot.transform)
                {
                    Destroy(item.gameObject);
                }
            }

            for (int i = 0; i < _inventoryLenght; i++)
            {
                if (i <= inventory.Count - 1)
                {
                    slot = Instantiate(_prefabs, transform.position, transform.rotation);
                    slot.transform.SetParent(_holderSlot.transform);

                    TextMeshProUGUI amount = slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
                    Image img = slot.transform.Find("Icon").GetComponent<Image>();

                    slot.GetComponent<FC_SlotItem>().itemSlot = i;

                    amount.text = inventory[i].amount.ToString();
                    img.sprite = inventory[i].icon;
                }
                else if (i > inventory.Count - 1)
                {
                    slot = Instantiate(_prefabs, transform.position, transform.rotation);
                    slot.transform.SetParent(_holderSlot.transform);
                    slot.GetComponent<FC_SlotItem>().itemSlot = i;

                    TextMeshProUGUI amount = slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
                    amount.gameObject.SetActive(false);
                }
            }
        }
        else if (_inventoryPanel.activeInHierarchy) 
        {
            _inventoryPanel.SetActive(false);
        }
    }

    public void ChargeItem(int i)
    {
        _holderDescription.SetActive(true);
        _Title.text = inventory[i].title;
        _descriptionObject.text = inventory[i].description;
        _iconDescription.sprite = inventory[i].icon;
    }
}

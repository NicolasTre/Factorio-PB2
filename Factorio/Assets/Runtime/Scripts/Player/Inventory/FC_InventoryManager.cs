using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class FC_InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject _inventoryPanel, _holderSlot;
    [SerializeField] private GameObject _prefabs;
    [SerializeField] private TextMeshProUGUI _Title, _descriptionObject;
    [SerializeField] private Image _iconDescription;

    [Header("Description")]
    [SerializeField] private GameObject _holderDescription;
    private int _amountToUse;
    [SerializeField] private TextMeshProUGUI _valueToUse;
    [SerializeField] private Button _plusButton, _minusButton;
    [SerializeField] private GameObject _useButton;
    [SerializeField] private GameObject _removeButton;


    [HideInInspector] public GameObject slot;
    private int _inventoryLenght = 35;
    
    public List<FC_Iitem> inventory = new();
    public static FC_InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void InventoryOpen(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            RefreshInventory();
        }
    }

    private void RefreshInventory()// function for open inventory and create a slot and set item to slot
    {
        if (!_inventoryPanel.activeInHierarchy)
        {
            _holderDescription.SetActive(false);
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

                    amount.text = inventory[i].quantities.ToString();
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
        if (i < 0 || i >= inventory.Count)
        {
            Debug.LogWarning($"Index {i} est hors de la plage de l'inventaire. Taille actuelle : {inventory.Count}");
            return;
        }

        if (inventory[i] == null)
        {
            return;
        }

        _amountToUse = 0;
        _valueToUse.text = _amountToUse + "/" + inventory[i].maxAmount; 

        _holderDescription.SetActive(true);
        _Title.text = inventory[i].title;
        _descriptionObject.text = inventory[i].description;
        _iconDescription.sprite = inventory[i].icon;

        _plusButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _plusButton.GetComponent<Button>().onClick.AddListener(delegate { PlusButton(i);  });

        _minusButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _minusButton.GetComponent<Button>().onClick.AddListener(delegate { MinusButton(i);  });

        _useButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _useButton.GetComponent<Button>().onClick.AddListener(delegate { UseItem(i); });

        _removeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _removeButton.GetComponent<Button>().onClick.AddListener(delegate { RemoveItem(i); });


    }

    public void UseItem(int i)
    {
        for (int x = 0; x < _amountToUse; x++)
        {
            if (inventory[i].quantities == 1)
            {
                inventory.Remove(inventory[i]);
                _holderDescription.SetActive(false);
                _amountToUse = 0; 
                break;
            }
            else
            {
                inventory[i].quantities--;
            }
        }
        RefreshInventory();
        _valueToUse.text = _amountToUse + "/" + inventory[i].maxAmount;
    }

    public void RemoveItem(int i)
    {
        for (int x  = 0; x < _amountToUse; x++)
        {
            if (inventory[i].quantities < 1)
            {
                inventory.Remove(inventory[i]);
                _holderDescription.SetActive(false);
                Destroy(slot);
                _amountToUse = 0;
                break;
            }
            else
            {
                inventory[i].quantities--;
            }
        }
        RefreshInventory();
        _valueToUse.text = _amountToUse + "/" + inventory[i].maxAmount;
    }

    public void PlusButton(int i)
    {
        if (_amountToUse <= inventory[i].quantities - 1)
        {
            _amountToUse++;
            _valueToUse.text = _amountToUse + "/" + inventory[i].maxAmount;
        }
    }

    public void MinusButton(int i)
    {
        if (_amountToUse > 0)
        {
            _amountToUse--;

            _valueToUse.text = _amountToUse + "/" + inventory[i].maxAmount;
        }
    }
}

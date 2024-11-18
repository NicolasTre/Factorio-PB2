using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Purchasing;

public class FC_InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject _inventoryPanel, _holderSlot;
    [SerializeField] private GameObject _prefabs;
    [SerializeField] private TextMeshProUGUI _Title, _descriptionObject;
    [SerializeField] private Image _iconDescription;

    public List<FC_FurnaceRessource> furnaceRessource;
    public List<FC_SlitterMachine> slitterMachine;


    [Header("Description")]
    [SerializeField] private GameObject _holderDescription;
    private int _amountToUse;
    [SerializeField] private TextMeshProUGUI _valueToUse;
    [SerializeField] private Button _plusButton, _minusButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _removeButton;

    private FC_SlotItem _currentSelectedSlot;


    [HideInInspector] public GameObject slot;
    private int _inventoryLenght = 35;
    
    public List<FC_Iitem> inventory = new();
    public static FC_InventoryManager instance;
    

    private void Awake()
    {
        instance = this;
        _currentSelectedSlot = null;

        _plusButton.onClick.AddListener(PlusButton);
        _minusButton.onClick.AddListener(MinusButton);
        _useButton.onClick.AddListener(UseItem);
        _removeButton.onClick.AddListener(RemoveItem);


    }

    public void InventoryOpen(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        if (!_inventoryPanel.activeInHierarchy)
        {
            _holderDescription.SetActive(false);
            _inventoryPanel.SetActive(true);
            RefreshInventory();
        }
        else
        {
            _inventoryPanel.SetActive(false);
        }
    }

    public void RefreshInventory()// function for open inventory and create a slot and set item to slot
    {
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
                SlotCreation(i);
                SetAmountText(true, i);

                Image img = slot.transform.Find("Icon").GetComponent<Image>();
                img.sprite = inventory[i].icon;
            }
            else if (i > inventory.Count - 1)
            {
                SlotCreation(i);
                SetAmountText(false);
            }
        }
    }

    private TextMeshProUGUI SetAmountText(bool value, int indexSlot = -1)
    {
        TextMeshProUGUI amount = slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

        if (value && indexSlot != -1)
        {
            amount.gameObject?.SetActive(value);
            amount.text = inventory[indexSlot].quantities.ToString();
        }
        else
        {
            amount.gameObject.SetActive(false);
        }

        return amount;
    }

    private void SlotCreation(int indexSlot)
    {
        slot = Instantiate(_prefabs, transform.position, transform.rotation, _holderSlot.transform);
        FC_SlotItem slotItem = slot.GetComponent<FC_SlotItem>();
        slotItem.itemSlot = indexSlot;
        slotItem.RefreshGetComponent();
    }

    public FC_Iitem GetItem(FC_SlotItem slot) // permet de get l'item actuellement dans le slot sélectionner
    {
        if (slot == null)
        {
            return null;
        }
        return inventory[slot.itemSlot];
    }

    /// <summary>
    ///chargeItem est appelé quand chargeItem de FC_SlotItem est appelé au clic du bouton de l'item ce qui charge la description de l'item
    /// </summary>
    /// <param name="newSlot"></param>

    public void ChargeItem(FC_SlotItem newSlot)
    {

        if (newSlot == null) // si il n'y a pas de prefab
        {
            return;
        }

        if (GetItem(newSlot) == null)//s'il n'y as pas d'item dans le slot
        {
            return;
        }

        _currentSelectedSlot = newSlot; 
        _amountToUse = 0;
        RedefDescriptionItem(_currentSelectedSlot.itemSlot);
    }

    /// <summary>
    /// Redéfinition de la description de l'item choisi dans l'inventaire en fonction de l'index de l'item.
    /// </summary>
    /// <param name="currentSlot"></param>
    public void RedefDescriptionItem(int currentSlot)
    {
        _valueToUse.text = _amountToUse + "/" + inventory[currentSlot].maxAmount;

        _holderDescription.SetActive(true);
        _Title.text = inventory[currentSlot].title;
        _descriptionObject.text = inventory[currentSlot].description;
        _iconDescription.sprite = inventory[currentSlot].icon;
    }

    /// <summary>
    ///Permet d'utiliser les items en fonction du nombre sélectionner dans l'inventaire
    /// </summary>
    public void UseItem()
    {
        if (_amountToUse >= 1)
        {
            FC_Iitem selectedItem = GetItem(_currentSelectedSlot);

            if (selectedItem == null)
            {
                Debug.LogWarning("Aucun item sélectionné !");
                return;
            }

            foreach (var furnace in furnaceRessource)
            {
                if (selectedItem.type == furnace._nameToInputItemPrefab)
                {
                    WhenUseItem(furnace);
                    return; 
                }
            }

            foreach (var slitter in slitterMachine)
            {
                if (selectedItem.type == slitter._nameToInputItemPrefab)
                {
                    WhenUseItem(slitter);
                    return;
                }
            }
        }
    }

    public void WhenUseItem(FC_IMachine machine)
    {
        machine.AddInputItem(_amountToUse);
        machine.StartProduction();
        RemoveItemWhenUse();
        ToggleInventory();
        _valueToUse.text = _amountToUse + "/" + GetItem(_currentSelectedSlot).maxAmount;
    }

    public void RemoveItemWhenUse()
    {
        for (int x = 0; x < _amountToUse; x++)
        {
            if (GetItem(_currentSelectedSlot).quantities == 1)
            {
                DesactivateItemInventory();
                break;
            }
            else
            {
                GetItem(_currentSelectedSlot).quantities--;
            }
        }
    }

    public void DesactivateItemInventory()
    {
        inventory.Remove(GetItem(_currentSelectedSlot));
        _holderDescription.SetActive(false);
        _amountToUse = 0;
    }

    public void RemoveItem()
    {
        for (int x  = 0; x < _amountToUse; x++)
        {
            if (GetItem(_currentSelectedSlot).quantities < 1)
            {
                DesactivateItemInventory();
                Destroy(slot);
                break;
            }
            else
            {
                GetItem(_currentSelectedSlot).quantities--;
            }
        }
        ToggleInventory();
        _valueToUse.text = _amountToUse + "/" + GetItem(_currentSelectedSlot).maxAmount;
    }

    public void PlusButton()
    {
        if (_amountToUse <= GetItem(_currentSelectedSlot).quantities - 1)
        {
            _amountToUse++;
            _valueToUse.text = _amountToUse + "/" + GetItem(_currentSelectedSlot).maxAmount;
        }
    }

    public void MinusButton()
    {
        if (_amountToUse > 0)
        {
            _amountToUse--;

            _valueToUse.text = _amountToUse + "/" + GetItem(_currentSelectedSlot).maxAmount;
        }
    }
}

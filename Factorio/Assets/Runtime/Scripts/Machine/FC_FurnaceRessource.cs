using UnityEngine;
using UnityEngine.UI;

public class FC_FurnaceRessource : FC_Machines, FC_IMachine
{
    [Header("Références")]
    [SerializeField] private GameObject _outputItemPrefab; // item to create 
    [SerializeField] private Transform _outputPosition; // where his gonna create or eject 

    protected override void Start()
    {
        base.Start();
        FC_InventoryManager.instance.furnaceRessource.Add(this);
    }

    protected override void ProduceOutputItem()
    {
        if (_outputItemPrefab != null && _outputPosition != null)
        { 
            Instantiate(_outputItemPrefab, _outputPosition.position, Quaternion.identity);

            inputItemCount--;
        }
    }
}
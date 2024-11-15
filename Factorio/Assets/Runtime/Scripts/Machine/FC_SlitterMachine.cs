using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FC_SlitterMachine : FC_Machines, FC_IMachine
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _outputItemPrefab1; // item to create 
    [SerializeField] private GameObject _outputItemPrefab2; // item to create 

    [Header("SpawnPosition")]
    [SerializeField] private Transform _outputPosition1; // where his gonna create or eject 
    [SerializeField] private Transform _outputPosition2; // where his gonna create or eject 

    private Vector3Int rotate = new(0, 0, 90);

    protected override void Start()
    {
        base.Start();
        FC_InventoryManager.instance.slitterMachine.Add(this);
    }

    protected override void ProduceOutputItem()
    {
        if (_outputItemPrefab1 != null && _outputPosition1 != null && _outputPosition2 != null)
        {
            Instantiate(_outputItemPrefab1, _outputPosition1.position, Quaternion.identity);
            Instantiate(_outputItemPrefab2, _outputPosition2.position, Quaternion.identity);
            inputItemCount--;
        }
    }
}

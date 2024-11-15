using UnityEngine;
using UnityEngine.UI;

public class FC_FurnaceRessource : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject _inputItemPrefab; // item to consomme
    
    [SerializeField] private GameObject _outputItemPrefab; // item to create 
    [SerializeField] private Transform _outputPosition; // where his gonna create or eject 
    [SerializeField] private Slider _productionSlider;

    [Header("Paramètres de Production")]
    public float productionTime = 25f;  //Time for create 
    private float _currentProductionTime = 0f;

    private bool isProcessing = false;
    public string _nameToInputItemPrefab;// title of the item to use

    [Header("Stockage")]
    public int inputItemCount = 0;  //number item for start product

    private void Update()
    {  
        if (inputItemCount >= 1)
        {
            StartProduction();
        }
        if (isProcessing && inputItemCount > 0)
        {
            _currentProductionTime += Time.deltaTime;

            if (_productionSlider != null)
            {
                _productionSlider.value = _currentProductionTime / productionTime;
            }
            if (_currentProductionTime >= productionTime)
            {
                ProduceOutputItem();
                _currentProductionTime = 0f;
                isProcessing = false;  // Arrêter le processus
            }
        }
    }

    public void StartProduction()
    {
        if (inputItemCount > 0 && !isProcessing)
        {
            isProcessing = true;
        }
    }

    private void ProduceOutputItem()
    {
        if (_outputItemPrefab != null && _outputPosition != null)
        { 
            Instantiate(_outputItemPrefab, _outputPosition.position, Quaternion.identity);

            inputItemCount--;
        }
    }

    public void AddInputItem(int amount)
    {
        inputItemCount += amount;
    }
}
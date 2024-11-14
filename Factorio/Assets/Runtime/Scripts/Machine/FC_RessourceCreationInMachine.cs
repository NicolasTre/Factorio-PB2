using UnityEngine;
using UnityEngine.UI;

public class ProductionMachine : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _outputPosition; 
    [SerializeField] private Slider _productionSlider;

    [Header("Paramètres de Production")]
    private float _productionTime = 15f; 
    private float _currentProductionTime = 0f;


    private void Update()
    {
        // Si la machine produit encore
        if (_currentProductionTime < _productionTime)
        {
            _currentProductionTime += Time.deltaTime;  
            if (_productionSlider != null)
            {
                _productionSlider.value = _currentProductionTime / _productionTime; 
            }
        }
        else
        {
            ProduceItem();
            _currentProductionTime = 0f; 
        }
    }

    //create item on exit
    private void ProduceItem()
    {
        if (_itemPrefab != null && _outputPosition != null)
        {
            Instantiate(_itemPrefab, _outputPosition.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab d'item ou position de sortie non définis !");
        }
    }
}

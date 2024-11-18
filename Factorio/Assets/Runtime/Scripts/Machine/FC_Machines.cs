using UnityEngine;
using UnityEngine.UI;

public class FC_Machines : MonoBehaviour
{
    [SerializeField] protected GameObject _inputItemPrefab; // item to consomme
    [SerializeField] protected Slider _productionSlider;
    [SerializeField] protected GameObject _childMachineToRotate;

    private Vector3Int _rotate = new(0, 0, 90);



    [Header("Paramètres de Production")]
    protected float productionTime = 25f;  //Time for create 
    protected float _currentProductionTime = 0f;
    protected bool isProcessing = false;
    protected bool _canDoRotate = false;
    public Type _nameToInputItemPrefab;// title of the item to use

    [Header("Stockage")]
    protected int inputItemCount = 0;  //number item for start product

    protected virtual void Start()
    {
        if (FC_InputPlayer.instance != null)
        {
            FC_InputPlayer.instance.OnRotateMachine.AddListener(OnRotation);
        }
    }
    protected virtual void Update()
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

    public virtual void StartProduction()
    {
        if (inputItemCount > 0 && !isProcessing)
        {
            isProcessing = true;
        }
    }
    protected virtual void ProduceOutputItem()
    {
        Debug.LogWarning("ProduceOutputItem non implémenté dans la classe enfant !");
    }


    public virtual void AddInputItem(int amount)
    {
        inputItemCount += amount;
    }

    public virtual void OnMouseEnter()
    {
        FC_InputPlayer.instance.SetCanRotate(true);
        _canDoRotate = true;
    }

    protected virtual void OnMouseExit()
    {
        FC_InputPlayer.instance.SetCanRotate(false);
        _canDoRotate = false;
    }

    public virtual void OnRotation()
    {
        if (!_canDoRotate) return;
        _childMachineToRotate.transform.rotation *= Quaternion.Euler(_rotate);

        Vector3 eulerAngles = _childMachineToRotate.transform.eulerAngles;
        eulerAngles.z = eulerAngles.z % 360f; // Limiter la rotation Z
        _childMachineToRotate.transform.eulerAngles = eulerAngles;
    }
}
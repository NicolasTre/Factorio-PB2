using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class FC_SlotItem : MonoBehaviour, IBeginDragHandler,IDragHandler,IDropHandler, IEndDragHandler 
{
    public int itemSlot;
    public BoxCollider2D? _collider;
    private Camera _cam;
    private Vector3 _offset;
    private Canvas _canvas;

    private RectTransform[] _childrenRectTransform;
    private Vector3[] _firstPosition;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _cam = Camera.main;

        _childrenRectTransform = new RectTransform[2];
        _firstPosition = new Vector3[2];
        _childrenRectTransform = GetComponentsInChildren<RectTransform>();
        _childrenRectTransform = _childrenRectTransform.Where(rt => rt != transform).ToArray();
    }

    public void RefreshGetComponent()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    public void ChargeItem()
    {
        FC_InventoryManager.instance.ChargeItem(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Début du drag, possibilité de changer l'apparence ici*
        for (int i = 0; i < _childrenRectTransform.Length; i++)
        {
            _firstPosition[i] = _childrenRectTransform[i].anchoredPosition;
            Debug.Log(_firstPosition[i]);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        for (int i = 0; i < _childrenRectTransform.Length; i++)
        {
            _childrenRectTransform[i].anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
        // Déplacement de l'objet sur le Canvas
        
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Conversion de la position dans le canvas vers la scène
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Crée un nouvel objet dans la scène à la position du clic
            GameObject newObject = Instantiate(gameObject, hit.point, Quaternion.identity);
            newObject.transform.SetParent(null); // Détache du Canvas
        }
        //remettre l'objet d'origine à sa position initiale
        for (int i = 0; i < _childrenRectTransform.Length; i++)
        {
            _childrenRectTransform[i].anchoredPosition = _firstPosition[i];
        }
        

        
    }
}

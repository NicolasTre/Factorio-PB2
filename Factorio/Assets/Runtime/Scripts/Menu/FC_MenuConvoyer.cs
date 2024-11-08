using UnityEngine;

public class FC_MenuConvoyer : MonoBehaviour
{
    [SerializeField] private DIRECTION _direction = DIRECTION.Right;
    Transform _transform;

    private FC_MenuLevelManager _lvlManager;
    public FC_MenuConvoyerData data { get; private set; }

    private void Awake()
    {
        _transform = transform;
        
        _lvlManager = FindObjectOfType<FC_MenuLevelManager>();
        data = new(_direction, _transform.position);
        
        _lvlManager.AddConvoyer(this);
    }
}
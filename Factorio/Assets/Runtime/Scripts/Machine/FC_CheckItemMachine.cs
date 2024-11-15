using UnityEngine;

public class FC_CheckItemMachine : MonoBehaviour
{
    [SerializeField] private FC_RessourceCreationInMachine _machine;
    private Collider2D _exitCollider;
    private void Awake()
    {
        _exitCollider = GetComponent<Collider2D>();
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FC_ItemData>())
        {
            _machine.SetCanProduct(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FC_ItemData>())
        {
            _machine.SetCanProduct(true);
        }
    }
}

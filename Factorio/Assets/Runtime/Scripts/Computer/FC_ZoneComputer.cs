using UnityEngine;

public class FC_ZoneComputer : MonoBehaviour
{
    [SerializeField] Animator animator;
    private BoxCollider2D zoneCollider;

    private void Awake()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("open Computer");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Close Computer");
    }
}
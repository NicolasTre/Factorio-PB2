using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class FC_ZoneComputer : MonoBehaviour
{
    [SerializeField] Animator anim;
    private BoxCollider2D zoneCollider;
    private bool isOpen = false;


    private void Awake()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("open Computer");
        anim.SetBool("isOpen", isOpen = true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Close Computer");
        anim.SetBool("isOpen", isOpen = false);
    }
}
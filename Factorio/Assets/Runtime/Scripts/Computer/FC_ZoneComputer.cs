using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class FC_ZoneComputer : MonoBehaviour
{
    [SerializeField] Animator anim;
    private BoxCollider2D zoneCollider;
    public bool isOpen { get; private set; }


    private void Awake()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
        isOpen = false;
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
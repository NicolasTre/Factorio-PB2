using UnityEngine;

public class FC_ItemData : MonoBehaviour
{
    public Transform transformItem { get; set; }
    public int quantities = 1;
    private void Awake()
    {
        transformItem = this.transform;
    }
}
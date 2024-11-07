using UnityEngine;

public class FC_ItemData : MonoBehaviour
{
    public Transform transformItem { get; set; }

    private void Awake()
    {
        transformItem = this.transform;
    }
}
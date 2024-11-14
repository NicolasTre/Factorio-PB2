using UnityEngine;

public abstract class FC_ManagerItemOnConvoyer : MonoBehaviour
{
    private void Update()
    {
        UpdateItem();
    }

    protected abstract void UpdateItem();
}
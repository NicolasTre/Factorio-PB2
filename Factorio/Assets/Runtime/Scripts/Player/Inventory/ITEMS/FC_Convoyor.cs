using UnityEngine;

[CreateAssetMenu(fileName = "Convoyor", menuName = "Items/Convoyor")]
public class FC_Convoyor : FC_ItemSo
{
    public FC_Convoyor() : base()
    {
        type = Type.Convoyor;
        description = "Item Convoyeur";
        isStackable = true;
    }
}

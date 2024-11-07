using UnityEngine;

[CreateAssetMenu(fileName = "Viande Cuite", menuName = "Items/Viande Cuite")]
public class FC_ViandeCuite : FC_ItemSo
{
    public FC_ViandeCuite() : base()
    {
        type = Type.ViandeCuite;
        title = type.ToString();
        description = "Item Viande Cuite pour les sandwichs";
        isStackable = true;
    }
}

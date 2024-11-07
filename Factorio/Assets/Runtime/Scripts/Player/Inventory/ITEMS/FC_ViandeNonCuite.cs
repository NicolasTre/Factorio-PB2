using UnityEngine;

[CreateAssetMenu(fileName = "Viande non cuite", menuName = "Items/Viande non cuite")]
public class FC_ViandeNonCuite : FC_ItemSo
{
    public FC_ViandeNonCuite() : base()
    {
        type = Type.ViandeNonCuite;
        description = "Item Viande non cuite pour les sandwichs";
        isStackable = true;
    }
}

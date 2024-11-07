using UnityEngine;

[CreateAssetMenu(fileName = "PainBasCoupeNonCuit", menuName = "Items/PainBasCoupeNonCuit")]
public class FC_PainBasCoupeNonCuit : FC_ItemSo
{
    public FC_PainBasCoupeNonCuit() : base()
    {
        type = Type.PainBasCoupeNonCuit;
        description = "Item Pain bas coupé non cuit pour les sandwichs";
        isStackable = true;
    }
}

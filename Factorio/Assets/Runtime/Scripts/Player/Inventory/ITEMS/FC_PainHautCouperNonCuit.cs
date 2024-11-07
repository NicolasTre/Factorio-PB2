using UnityEngine;

[CreateAssetMenu(fileName = "PainHautCoupeNonCuit", menuName = "Items/PainHautCoupeNonCuit")]
public class FC_PainHautCoupeNonCuit : FC_ItemSo
{
    public FC_PainHautCoupeNonCuit() : base()
    {
        type = Type.PainHautCoupeNonCuit;
        description = "Item Pain non coupé pour les sandwichs";
        isStackable = true;
    }
}

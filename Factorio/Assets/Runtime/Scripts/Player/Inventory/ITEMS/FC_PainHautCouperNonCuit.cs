using UnityEngine;

[CreateAssetMenu(fileName = "PainHautCoupeNonCuit", menuName = "Items/PainHautCoupeNonCuit")]
public class FC_PainHautCoupeNonCuit : FC_ItemSo
{
    public FC_PainHautCoupeNonCuit() : base()
    {
        type = Type.PainHautCoupeNonCuit;
        title = type.ToString();
        description = "Item Pain non coupé pour les sandwichs";
        isStackable = true;
    }
}

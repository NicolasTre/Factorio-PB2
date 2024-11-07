using UnityEngine;

[CreateAssetMenu(fileName = "PainHautCoupeCuit", menuName = "Items/PainHautCoupeCuit")]
public class FC_PainHautCoupeCuit : FC_ItemSo
{
    public FC_PainHautCoupeCuit() : base()
    {
        type = Type.PainHautCoupeCuit;
        title = type.ToString();
        description = "Item PainHautCoupeCuit pour les sandwichs";
        isStackable = true;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "PainBasCoupeCuit", menuName = "Items/PainBasCoupeCuit")]
public class FC_PainBasCoupeCuit : FC_ItemSo
{
    public FC_PainBasCoupeCuit() : base()
    {
        type = Type.PainBasCoupeCuit;
        description = "Item Pain bas coup� cuit pour les sandwichs";
        isStackable = true;
    }
}

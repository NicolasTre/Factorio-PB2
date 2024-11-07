using UnityEngine;

[CreateAssetMenu(fileName = "PainNonCoupe", menuName = "Items/PainNonCoupe")]
public class FC_PainNonCoupe : FC_ItemSo
{
    public FC_PainNonCoupe() : base()
    {
        type = Type.PainNonCoupe;
        title = type.ToString();
        description = "Item Pain non coupé pour les sandwichs";
        isStackable = true;
    }
}

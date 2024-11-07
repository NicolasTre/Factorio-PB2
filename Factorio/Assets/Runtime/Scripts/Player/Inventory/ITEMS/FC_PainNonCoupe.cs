using UnityEngine;

[CreateAssetMenu(fileName = "PainNonCoupe", menuName = "Items/PainNonCoupe")]
public class FC_PainNonCoupe : FC_ItemSo
{
    public FC_PainNonCoupe() : base()
    {
        type = Type.PainNonCoupe;
        description = "Item Pain non coup� pour les sandwichs";
        isStackable = true;
    }
}

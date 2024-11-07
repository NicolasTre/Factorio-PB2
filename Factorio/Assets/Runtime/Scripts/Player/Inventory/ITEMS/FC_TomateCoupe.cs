using UnityEngine;

[CreateAssetMenu(fileName = "TomateCoupe", menuName = "Items/TomateCoupe")]
public class FC_TomateCoupe : FC_ItemSo
{
    public FC_TomateCoupe() : base()
    {
        type = Type.TomateCoupe;
        description = "Item Tomate Coup� pour les sandwichs";
        isStackable = true;
    }
}
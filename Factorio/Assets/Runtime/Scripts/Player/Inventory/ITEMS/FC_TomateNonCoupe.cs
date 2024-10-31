using UnityEngine;

[CreateAssetMenu(fileName = "TomateNonCoupe", menuName = "Items/TomateNonCoupe")]
public class FC_TomateNonCoupe : FC_ItemSo
{
    public FC_TomateNonCoupe() : base()
    {
        type = Type.TomateNonCoupe;
        title = type.ToString();
        description = "Item Tomate non Coupé pour les sandwichs";
        isStackable = true;
    }
}
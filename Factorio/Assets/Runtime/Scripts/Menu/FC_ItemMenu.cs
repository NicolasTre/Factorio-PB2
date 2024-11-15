using UnityEngine;

[CreateAssetMenu(fileName = "FC_ItemMenu", menuName = "Scriptable Objects/FC_ItemMenu")]
public class FC_ItemMenu : FC_ItemSo
{
    public FC_ItemMenu() : base()
    {
        maxAmount = 0;
        type = Type.ButtonMenu;
        description = "Item Button for the main Menu";
        quantities = 0;
        isStackable = false;
    }
}

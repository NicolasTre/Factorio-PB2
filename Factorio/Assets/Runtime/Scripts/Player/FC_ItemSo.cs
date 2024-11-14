using UnityEngine;

[System.Serializable]
public enum Type
{
    None,
    PainHautCoupeCuit,
    PainBasCoupeCuit,
    PainHautCoupeNonCuit,
    PainBasCoupeNonCuit,
    PainNonCoupe,
    ViandeNonCuite,
    ViandeCuite,
    TomateCoupe,
    TomateNonCoupe,
    Convoyor,
    ButtonMenu
}

[CreateAssetMenu(fileName = "Vierge", menuName = "Items/Vierge")]
public class FC_ItemSo : ScriptableObject, FC_Iitem
{
    [SerializeField] protected string _title;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected int _quantities;
    [SerializeField] protected int _maxAmount;
    [SerializeField] protected bool _isStackable;

    public Type type;


    public string title { get => _title; set => _title = value; }
    public string description { get => _description; set => _description = value; }
    public Sprite icon { get => _icon; set => _icon = value; }
    public int quantities { get => _quantities; set => _quantities = value; }
    public int maxAmount { get => _maxAmount; set => _maxAmount = value; }
    public bool isStackable { get => _isStackable; set => _isStackable = value; }
    public FC_ItemSo()
    {
        if (type != Type.None)
        {
            title = type.ToString();
        }
    }
}

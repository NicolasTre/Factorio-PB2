using UnityEngine;

[CreateAssetMenu(fileName = "Vierge", menuName = "Items/Vierge")]
public class FC_ItemSo : ScriptableObject, FC_Iitem
{
    [SerializeField] protected string _title;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected int _quantities;
    [SerializeField] protected int _maxAmount;
    [SerializeField] protected bool _isStackable;
    [SerializeField] protected Type _type;



    public string title { get => _title; set => _title = value; }
    public string description { get => _description; set => _description = value; }
    public Sprite icon { get => _icon; set => _icon = value; }
    public int quantities { get => _quantities; set => _quantities = value; }
    public int maxAmount { get => _maxAmount; set => _maxAmount = value; }
    public bool isStackable { get => _isStackable; set => _isStackable = value; }
    public Type type { get => _type; set => _type = value; }
}

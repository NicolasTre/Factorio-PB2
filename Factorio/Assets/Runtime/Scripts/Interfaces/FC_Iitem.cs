using UnityEngine;

public interface FC_Iitem
{
    string title { get; set; }
    string description { get; set; }
    Sprite icon { get; set; }
    int quantities { get; set; }
    int maxAmount { get; set; }
    bool isStackable { get; set; }
    Type type { get; set; }
}

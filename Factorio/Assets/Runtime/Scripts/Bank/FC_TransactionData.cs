using System;
using UnityEngine;

public class FC_TransactionData
{
    public string name { get; set; }
    public float amount { get; set; }
    public DateTime timestamp { get; set; }
    public Sprite image { get; set; }
    public FC_ItemType type { get; set; }

    public FC_TransactionData(string name, float amount, DateTime timestamp, Sprite image, FC_ItemType type)
    {
        this.name = name;
        this.amount = amount;
        this.timestamp = timestamp;
        this.image = image;
        this.type = type;
    }
}
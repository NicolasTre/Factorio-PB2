using System;
using UnityEngine;

public class FC_TransactionData
{
    public string name { get; set; }
    public float amount { get; set; }
    public DateTime timestamp { get; set; }
    public Sprite image { get; set; }
    public Type type { get; set; }

    public FC_TransactionData(string name, float amount, DateTime timestamp, Sprite image, Type type)
    {
        this.name = name;
        this.amount = amount;
        this.timestamp = timestamp;
        this.image = image;
        this.type = type;
    }
}
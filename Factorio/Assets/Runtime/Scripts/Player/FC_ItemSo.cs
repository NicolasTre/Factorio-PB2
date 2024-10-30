using System;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObject/Item", order = 1)]
public class FC_ItemSo : ScriptableObject
{
    public string title; 
    public string description;
    public Sprite icon;
    public int amount;
    public bool isStackable;

    [System.Serializable]
    public enum Type
    {
        Tomate, Pain, Viande
    }

    public Type type;

}

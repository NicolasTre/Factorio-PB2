using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class FC_BankDisplayMarket : MonoBehaviour
{
    [SerializeField] private Tile iconImage;
    [SerializeField] private Image imageToChange;

    private FC_BankController bank;
    private FC_ZoneComputer computer;

    private void Awake()
    {
        bank = FindFirstObjectByType<FC_BankController>();
        computer = FindFirstObjectByType<FC_ZoneComputer>();
        imageToChange.sprite = iconImage.sprite;
    }
}
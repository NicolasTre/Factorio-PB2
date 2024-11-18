using TMPro;
using UnityEngine;

public class FC_BankDisplayAmount : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    private FC_BankController bank;
    private FC_ZoneComputer computer;


    private void Awake()
    {
        bank = FindFirstObjectByType<FC_BankController>();
        computer = FindFirstObjectByType<FC_ZoneComputer>();
    }

    private void Update()
    {
        if (!computer.isOpen) return;

        txt.text = bank.userAccount.balance.ToString("0.00");
    }
}
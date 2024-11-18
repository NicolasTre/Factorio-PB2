using System.Collections.Generic;
using UnityEngine;

public class FC_BankController : MonoBehaviour
{
    public FC_BankAccount userAccount { get; private set; }
    public Dictionary<int, FC_BankAccount> accounts { get; private set; } = new();

    private void Start()
    {
        userAccount = new FC_BankAccount(12345, 1005f);
    }
}
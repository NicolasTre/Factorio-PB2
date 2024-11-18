using UnityEngine;
using UnityEngine.UI;

public class FC_ExpenseHistoryController : MonoBehaviour
{
    [SerializeField] private GameObject expensePrefab;
    [SerializeField] private Transform content;

    public void DisplayTransactionHistory(FC_BankAccount bankAccount)
    {
        foreach (FC_TransactionData transaction in bankAccount.transactionHistory)
        {
            AddTransactionToHistory(transaction);
        }
    }

    public void AddTransactionToHistory(FC_TransactionData transaction)
    {
        GameObject expenseItem = Instantiate(expensePrefab, content);

        Text nameText = expenseItem.transform.Find("NameText").GetComponent<Text>();
        Text priceText = expenseItem.transform.Find("PriceText").GetComponent<Text>();
        Text timeText = expenseItem.transform.Find("TimeText").GetComponent<Text>();
        Image iconImage = expenseItem.transform.Find("IconImage").GetComponent<Image>();

        nameText.text = transaction.name;
        priceText.text = transaction.amount.ToString("0.00") + " €";
        timeText.text = transaction.timestamp.ToString("HH:mm");
        iconImage.sprite = transaction.image;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank: MonoBehaviour
{
    [SerializeField] private int startingBalance = 100;
    [SerializeField] private int currentBalance;
    [SerializeField] TextMeshProUGUI balanceText;

    public int CurrentBalance
    {
        get { return currentBalance; }
    }

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateText();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateText();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateText();

        if (currentBalance < 0)
        {
            //Lose the game;
            ReloadScene();
        }
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateText()
    {
        balanceText.text = "Gold:" + currentBalance.ToString();
    }
}

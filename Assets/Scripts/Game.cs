using System;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public const int TimeLimit = 60;

    public static Game Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Game").GetComponent<Game>();
            }

            return _instance;
        }
    }

    public static event Action<int> OnMonthChange;

    public Text monthText;
    private int month = 0;
    private bool isGameOver = false;

    private void Start()
    {
        Invoke(nameof(OnAdvanceToNextMonth), 0.5f);
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Return))
        {
            OnAdvanceToNextMonth();
        }
    }

    public void RanOutOfMoney()
    {
        isGameOver = true;
        UiController.Instance.OpenView(View.RanOutOfMoney);
    }

    private void GameOver()
    {
        isGameOver = true;
        UiController.Instance.OpenView(View.GameOver);
    }

    private void OnAdvanceToNextMonth()
    {
        month++;

        if (month > TimeLimit)
        {
            GameOver();
        }
        else
        {
            OnMonthChange?.Invoke(month);
            monthText.text = $"Month {month}/{TimeLimit}";
        }
    }

    public int GetCurrentMonth()
    {
        return month;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game _instance;

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

    private void Start()
    {
        Invoke(nameof(OnAdvanceToNextMonth), 0.5f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Return))
        {
            OnAdvanceToNextMonth();
        }
    }

    private void OnAdvanceToNextMonth()
    {
        month++;
        OnMonthChange?.Invoke(month);
        monthText.text = $"Month {month}";
    }

    public int GetCurrentMonth()
    {
        return month;
    }
}

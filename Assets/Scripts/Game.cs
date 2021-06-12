using System;
using UnityEngine;

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
    private int month = 0;

    private void Start()
    {
        HeadsUpDisplay.OnAdvanceToNextMonth += OnAdvanceToNextMonth;
        Invoke(nameof(OnAdvanceToNextMonth), 0.5f);
    }

    private void OnAdvanceToNextMonth()
    {
        month++;
        OnMonthChange?.Invoke(month);
    }

    public int GetCurrentMonth()
    {
        return month;
    }
}

using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static event Action<int> OnMonthChange;
    private int month = 0;

    private void Start()
    {
        HeadsUpDisplay.OnAdvanceToNextMonth += OnAdvanceToNextMonth;
        Invoke(nameof(StartGame), 1f);
    }

    private void StartGame()
    {
        print("Starting game now!");
        OnAdvanceToNextMonth();
    }

    private void OnAdvanceToNextMonth()
    {
        month++;
        OnMonthChange?.Invoke(month + 1);
    }
}

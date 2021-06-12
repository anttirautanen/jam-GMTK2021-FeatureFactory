using System;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour
{
    public static event Action OnAdvanceToNextMonth;

    public void OnPressNextMonthButton()
    {
        OnAdvanceToNextMonth?.Invoke();
    }
}

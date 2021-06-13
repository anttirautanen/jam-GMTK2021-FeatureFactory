using System;
using UnityEngine;
using UnityEngine.UI;

public class HeadsUpDisplay : MonoBehaviour
{
    public static event Action OnAdvanceToNextMonth;
    public Text monthText;

    private void Start()
    {
        Game.OnMonthChange += UpdateMonth;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Return))
        {
            OnAdvanceToNextMonth?.Invoke();
        }
    }

    private void UpdateMonth(int month)
    {
        monthText.text = $"Month: {month}";
    }
}

using System;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour
{
    public static event Action OnAdvanceToNextMonth;
    public RectTransform hiringView;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UiController.Instance.OpenView(hiringView);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Return))
        {
            OnAdvanceToNextMonth?.Invoke();
        }
    }
}

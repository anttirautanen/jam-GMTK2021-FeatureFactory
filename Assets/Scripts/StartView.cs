using UnityEngine;

public class StartView : MonoBehaviour
{
    private void Start()
    {
        KeyHelp.Instance.Set(new[] {"(Enter) Start game"});
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UiController.Instance.OpenView(View.Company);
        }
    }
}

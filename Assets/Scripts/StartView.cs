using UnityEngine;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    public Text timeLimitText;

    private void Start()
    {
        timeLimitText.text = $"Make as much money as possible before time runs out in {Game.TimeLimit} months!";
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

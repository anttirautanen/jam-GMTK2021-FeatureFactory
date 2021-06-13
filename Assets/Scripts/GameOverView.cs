using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    public Text endBalanceText;

    private void Start()
    {
        endBalanceText.text = $"{Company.Instance.GetMoney():C0}";
    }
}

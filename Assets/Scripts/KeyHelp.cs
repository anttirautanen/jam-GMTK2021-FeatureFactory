using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class KeyHelp : MonoBehaviour
{
    private static KeyHelp _instance;

    public static KeyHelp Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KeyHelp").GetComponent<KeyHelp>();
            }

            return _instance;
        }
    }

    private Text keyHelpText;

    private void Start()
    {
        keyHelpText = GetComponent<Text>();
    }

    public void Set(IEnumerable<string> texts)
    {
        var sb = new StringBuilder();
        foreach (var text in texts)
        {
            sb.Append($"{text}     ");
        }
        keyHelpText.text = sb.ToString();
    }
}

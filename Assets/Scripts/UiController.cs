using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private static UiController _instance;

    public static UiController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("UiController").GetComponent<UiController>();
            }

            return _instance;
        }
    }

    public Canvas canvas;
    public Transform hudPrefab;
    private readonly Stack<GameObject> uiStack = new Stack<GameObject>();

    private void Start()
    {
        OpenView(hudPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseView();
        }
    }

    public void OpenView(Transform view)
    {
        if (uiStack.Count > 0)
        {
            uiStack.Peek().SetActive(false);
        }

        uiStack.Push(Instantiate(view, canvas.transform).gameObject);
    }

    private void CloseView()
    {
        if (uiStack.Count > 1)
        {
            var viewGameObject = uiStack.Pop();
            uiStack.Peek().SetActive(true);
            Destroy(viewGameObject);
        }
    }
}

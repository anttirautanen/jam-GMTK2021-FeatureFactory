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
    public Transform baseViewPrefab;
    private readonly Stack<GameObject> uiStack = new Stack<GameObject>();

    private void Start()
    {
        OpenView(baseViewPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseView();
        }
    }

    public Transform OpenView(Transform view)
    {
        if (uiStack.Count > 0)
        {
            uiStack.Peek().SetActive(false);
        }

        var transformInstance = Instantiate(view, canvas.transform);
        uiStack.Push(transformInstance.gameObject);
        return transformInstance;
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

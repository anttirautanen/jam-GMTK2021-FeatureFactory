using UnityEngine;

public class BaseView : MonoBehaviour
{
    private Transform currentView;

    public Transform SwitchView(Transform view)
    {
        if (currentView != null)
        {
            Destroy(currentView.gameObject);
        }

        return Instantiate(view, transform);
    }
}

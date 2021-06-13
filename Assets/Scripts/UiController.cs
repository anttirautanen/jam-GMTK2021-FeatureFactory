using System.Collections.Generic;
using UnityEngine;

public enum View
{
    Start,
    Company,
    EditTeam,
    Hire,
    Features,
    RanOutOfMoney,
    GameOver
}

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

    public Transform canvas;
    public Transform baseViewPrefab;
    public Transform startViewPrefab;
    public Transform companyViewPrefab;
    public Transform editTeamViewPrefab;
    public Transform hiringViewPrefab;
    public Transform featuresViewPrefab;
    public Transform outOfMoneyViewPrefab;
    public Transform gameOverViewPrefab;

    private Dictionary<View, Transform> views;
    private BaseView baseView;
    private GameObject currentView;

    private void Start()
    {
        views = new Dictionary<View, Transform>
        {
            {View.Start, startViewPrefab},
            {View.Company, companyViewPrefab},
            {View.EditTeam, editTeamViewPrefab},
            {View.Hire, hiringViewPrefab},
            {View.Features, featuresViewPrefab},
            {View.RanOutOfMoney, outOfMoneyViewPrefab},
            {View.GameOver, gameOverViewPrefab}
        };

        baseView = Instantiate(baseViewPrefab, canvas).GetComponent<BaseView>();
        OpenView(View.Start);
    }

    public Transform OpenView(View view)
    {
        if (currentView != null)
        {
            Destroy(currentView);
        }

        var transformInstance = baseView.SwitchView(views[view]);
        currentView = transformInstance.gameObject;
        return transformInstance;
    }
}

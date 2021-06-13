using UnityEngine;
using UnityEngine.UI;

public class HiringView : MonoBehaviour
{
    public Text headingText;
    private DevTeam team;

    public void Setup(DevTeam team)
    {
        this.team = team;
    }

    private void Start()
    {
        headingText.text = $"Hire into team {team.Feature.Name}";
        KeyHelp.Instance.Set(new[] {"(Esc) Features", "(Enter) Hire"});
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiController.Instance.OpenView(View.Features);
        }
    }
}

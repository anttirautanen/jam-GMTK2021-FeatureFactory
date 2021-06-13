using UnityEngine;

public class DeveloperRow : IRow
{
    private readonly Developer developer;
    private readonly bool isSelected;

    public DeveloperRow(Developer developer, bool isSelected)
    {
        this.developer = developer;
        this.isSelected = isSelected;
    }

    public RowType GetRowType()
    {
        return RowType.Developer;
    }

    public void Instantiate(Transform transformInstance)
    {
        transformInstance.GetComponent<DeveloperRowView>().Setup(developer, isSelected);
    }
}

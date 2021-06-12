using UnityEngine;

public interface IRow
{
    public RowType GetRowType();
    public void Instantiate(Transform transformInstance);
}

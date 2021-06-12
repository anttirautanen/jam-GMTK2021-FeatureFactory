using UnityEngine.UI;

public class MarketLayout : LayoutGroup
{
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        var parentRectTransform = rectTransform.rect;
        var parentWidth = parentRectTransform.width;
        var parentHeight = parentRectTransform.height;
        var columnWidth = parentWidth / 2;

        for (var i = 0; i < rectChildren.Count; i++)
        {
            var xPos = columnWidth * i;
            SetChildAlongAxis(rectChildren[i], 0, xPos, columnWidth);
            SetChildAlongAxis(rectChildren[i], 1, 0, parentHeight);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}

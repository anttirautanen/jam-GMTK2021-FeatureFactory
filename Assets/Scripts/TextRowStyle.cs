public class TextRowStyle
{
    public bool IsHeading;
    public bool IsPositive;
    public bool IsNegative;
    public bool IsSecondary;

    public static readonly TextRowStyle Heading = new TextRowStyle()
    {
        IsHeading = true
    };

    public static readonly TextRowStyle Positive = new TextRowStyle()
    {
        IsPositive = true
    };

    public static readonly TextRowStyle Negative = new TextRowStyle()
    {
        IsNegative = true
    };

    public static readonly TextRowStyle Secondary = new TextRowStyle()
    {
        IsSecondary = true
    };
}

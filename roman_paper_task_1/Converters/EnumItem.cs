namespace roman_paper_task_1.Converters;

public class EnumItem
{
    public object Value { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return Description;
    }
}
namespace xmlcomperator;

public class XmlComparatorController
{
    public XmlComparatorController(string[] args)
    {
        new XmlComparator().CompareByElement(true);
    }
}

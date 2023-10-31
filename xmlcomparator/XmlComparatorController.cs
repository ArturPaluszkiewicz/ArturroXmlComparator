namespace xmlcomperator;

public class XmlComparatorController
{
    public XmlComparatorController(string[] args)
    {
        new XmlComparator().CompareLineByLine(true);
    }
}

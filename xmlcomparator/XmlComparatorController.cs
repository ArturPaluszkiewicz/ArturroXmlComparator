namespace xmlcomperator;

public class XmlComparatorController
{
    public XmlComparatorController(string[] args)
    {
        new XmlComparator("FileToTest3.xml","FileToTest3.xml").CompareLineByLine(true);
    }
}

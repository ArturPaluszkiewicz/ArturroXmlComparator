namespace xmlcomparator;

public interface IXmlComparator
{
    public void CompareByElement(bool withValue);
    public void CompareLineByLine(bool onlyLine);
    public Dictionary<string, string> GetsElementsFromFile(string filePath);
    public void Compare();
    public void ChangeType(ComparatorType type);
    public void SetFilesPath(string path1, string path2);
    public void SetReportPath(string reportPath);
    public void AddError(string errortText);
}

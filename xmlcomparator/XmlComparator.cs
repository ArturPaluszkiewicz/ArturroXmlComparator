using System.Text;
using System.Text.RegularExpressions;
using xmlcomparator;

namespace xmlcomperator;

public class XmlComparator
{
    private string fileOnePath;
    private string fileTwoPath;
    private readonly StringBuilder report;
    private readonly StringBuilder diffrence;
    private readonly StringBuilder errors;
    private ComparatorType type;
    private string reportPath;
    public XmlComparator()
    {
        fileOnePath = "model1.xml";
        fileTwoPath = "model2.xml";
        reportPath = "report.txt";
        type = ComparatorType.Line;
        report = new StringBuilder();
        diffrence = new StringBuilder();
        errors = new StringBuilder();
    }
    public void CompareByElement(bool withValue)
    {
        var file1Elements = GetsElementsFromFile(fileOnePath);
        var file2Elements = GetsElementsFromFile(fileTwoPath);

        report.AppendLine("Algorithm start compare files...");

        foreach(var element in file1Elements)
        {
            if (file2Elements.ContainsKey(element.Key))
            {
                if(!CompareElement(element.Value,file2Elements.GetValueOrDefault(element.Key)??""))
                {
                    diffrence.AppendLine(element.Key);
                    if(withValue == true)
                    {
                        diffrence.AppendLine(element.Value);
                    }
                }
                file2Elements.Remove(element.Key);
            }
            else
            {
                diffrence.AppendLine(element.Key);
                if(withValue == true)
                {
                    diffrence.AppendLine(element.Value);
                }
            }
        }
        report.AppendLine("Algorithm end comparing files corectly. \n");
        SaveToFile();
    }
    public void CompareLineByLine(bool onlyLine)
    {
        report.AppendLine("Algorithm start compare files...");
        try
        {
            var lineNumber = 1;
            var sr1 = new StreamReader(fileOnePath);
            var sr2 = new StreamReader(fileTwoPath);
            var line1 = sr1.ReadLine();
            var line2 = sr2.ReadLine();

            while(line1 != null || line2 != null)
            {
                if(line1!=line2)
                {
                    diffrence.AppendLine("Difference in line: "+lineNumber);
                    if(onlyLine==false)
                    {
                    diffrence.AppendLine("File1: "+line1);
                    diffrence.AppendLine("File2: "+line2);
                    diffrence.AppendLine();
                    }
                }
                lineNumber++;
                line1 = sr1.ReadLine();
                line2 = sr2.ReadLine();
                
            }

            sr1.Close();
            sr2.Close();
            report.AppendLine("Algorithm end comparing files corectly. \n");
        }catch(Exception e)
        {
            report.AppendLine("Algorithm fail during comparing files...");
            errors.AppendLine("LineByLine Comparator:");
            errors.AppendLine("-Could not find files.");
        }
        SaveToFile();
    }
    public Dictionary<string, string> GetsElementsFromFile(string filePath)
    {

        string pattern = @"<Element";
        string exitPattern = @"</Element>";
        Dictionary<string, string> elements = new();
        try
        {
            var sr = new StreamReader(filePath);
            var line = sr.ReadLine();
            bool temp = true;
            while(line != null)
            {  
                if(Regex.IsMatch(line, pattern))
                {   
                    string key = CheckIfElementAlredyAdded(elements, line);
                    StringBuilder value = new();
                    int deap = 1;
                    line = sr.ReadLine();
                    while(deap != 0 )
                    {
                        if(Regex.IsMatch(line??"", pattern))
                        {
                            deap++;
                        }
                        if(Regex.IsMatch(line??"", exitPattern))
                        {
                            deap--;
                        }
                        value.AppendLine(line);
                        line = sr.ReadLine();
                    }
                    elements.Add(key,value.ToString());
                    temp = false;
                }
                if(temp == true)
                {
                    line = sr.ReadLine();
                }
                else
                {
                    temp = true;
                }
            }
        }catch(Exception e)
        {
            Console.WriteLine("Something go terible wrongs");
            Console.WriteLine(e.Message);
        }
        return elements;
    }
    public void Compare()
    {
        if(type == ComparatorType.Element)
        {
            CompareByElement(false);
        }
        if(type == ComparatorType.Line)
        {
            CompareLineByLine(false);
        }
        if(type == ComparatorType.ElementWithValue)
        {
            CompareByElement(true);
        }
        if(type == ComparatorType.OnlyLine)
        {
            CompareLineByLine(true);
        }
    }
    private void SaveToFile()
    {
        var differenceString = diffrence.ToString();
        var errorsString = errors.ToString(); 
        if(errorsString != "")
        {
            report.AppendLine("\n*****Errors:*****\n");
            report.AppendLine(errorsString);
        }
        else if(differenceString != "")
        {
            report.AppendLine("Files doesnt match !!!\n");
            report.AppendLine("\n*****Differences:*****\n");
            report.AppendLine(differenceString);
        }
        else
        {
            report.AppendLine("Files are the same !!!");
        }
        try
        {
            var sw = new StreamWriter("report.txt");
            sw.Write(report.ToString());
            sw.Close();
        }catch(Exception e)
        {
            Console.WriteLine("Something go terible wrong");
            Console.WriteLine(e.Message);
        }
    }
    private static bool CompareElement(string elementOne, string elementTwo)
    {
        if(elementOne==elementTwo)
        {
            return true;
        }
        return false;
    }
    private static string CheckIfElementAlredyAdded(Dictionary<string, string> listOfElements, string keyToCheck)
    {
        int version = 0;
        while(listOfElements.ContainsKey(keyToCheck))
        {
            if(version == 0)
            {
                keyToCheck = keyToCheck+" v"+(version+1).ToString();
            }
            else
            {
                keyToCheck = keyToCheck.Remove(keyToCheck.Length - 1);
                keyToCheck = keyToCheck+version.ToString();
            }
            version++;
        }
        return keyToCheck;
    }
    public void ChangeType(ComparatorType type)
    {
        this.type = type;
    }
    public void SetFilesPath(string path1, string path2)
    {
        this.fileOnePath = path1;
        this.fileTwoPath = path2;
    }
    public void SetReportPath(string reportPath)
    {
        this.reportPath = reportPath;
    }
    public void AddError(string errortText)
    {
        errors.AppendLine(errortText);
    }
}

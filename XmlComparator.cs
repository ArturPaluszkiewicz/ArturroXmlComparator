using System.Text;
using System.Text.RegularExpressions;

namespace xmlcomperator;

public class XmlComparator
{
    private readonly string _fileOnePath;
    private readonly string _fileTwoPath;
    private readonly StringBuilder report;
    private readonly StringBuilder diffrence;
    public XmlComparator(string fileOnePath, string fileTwoPath)
    {
        _fileOnePath = fileOnePath;
        _fileTwoPath = fileTwoPath;
        report = new StringBuilder();
        diffrence = new StringBuilder();

    }
    public XmlComparator()
    {
        _fileOnePath = "model1.xml";
        _fileTwoPath = "model2.xml";
        report = new StringBuilder();
        diffrence = new StringBuilder();
    }
    public void CompareByElement(Boolean withValue)
    {
        var file1Elements = GetsElementsFromFile(_fileOnePath);
        var file2Elements = GetsElementsFromFile(_fileTwoPath);

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

    public bool CompareLineByLine()
    {
        var report = new StringBuilder();
        bool difFile = false;
        try
        {
            var lineNumber = 1;
            var sr1 = new StreamReader(_fileOnePath);
            var sr2 = new StreamReader(_fileTwoPath);
            var line1 = sr1.ReadLine();
            var line2 = sr2.ReadLine();

            while(line1 != null || line2 != null)
            {
                if(line1!=line2)
                {
                    report.AppendLine("Difference in line: "+lineNumber);
                    report.AppendLine("File1: "+line1);
                    report.AppendLine("File2: "+line2);
                    report.AppendLine();
                    difFile = true;
                }
                lineNumber++;
                line1 = sr1.ReadLine();
                line2 = sr2.ReadLine();
                
            }

            sr1.Close();
            sr2.Close();

            if(!difFile)
            {
                report.AppendLine("File are the same");
            }
        }catch(Exception e)
        {
            report.AppendLine("Exception: " + e.Message);
        }
        //Save report to file

        report.AppendLine("Algorithm end comparing files corectly. \n");
        SaveToFile();

        return difFile;
    }
    private Dictionary<string, string> GetsElementsFromFile(string filePath)
    {

        string pattern = @"<Element ";
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
                    string key;
                    if (elements.ContainsKey(line))
                    {
                        key = line + " v2";
                    }
                    else
                    {
                        key = line;
                    }
                    //string key = checkIfElementAlredyAdded(ref elements,line,0);
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
                    //dodaj do slownika
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

    private void SaveToFile()
    {
        var differenceString = diffrence.ToString();
        if(differenceString != "")
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
    private static Boolean CompareElement(string elementOne, string elementTwo)
    {
        if(elementOne==elementTwo)
        {
            return true;
        }
        return false;
    }
    private string CheckIfElementAlredyAdded(ref Dictionary<string, string> listOfElements, string keyToCheck, int version)
    {
        if(version != 0)
        {
            keyToCheck = keyToCheck+" v "+version;
        }
        if(listOfElements.ContainsKey(keyToCheck))
        {
            CheckIfElementAlredyAdded(ref listOfElements,keyToCheck,++version);
        }
        return keyToCheck;
    }
}

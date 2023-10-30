using System.Text;
using System.Text.RegularExpressions;

namespace xmlcomperator;

public class XmlComparator
{
    private readonly string _fileOnePath;
    private readonly string _fileTwoPath;
    public XmlComparator(string fileOnePath, string fileTwoPath)
    {
        _fileOnePath = fileOnePath;
        _fileTwoPath = fileTwoPath;
    }
    public XmlComparator()
    {
        _fileOnePath = "model1.xml";
        _fileTwoPath = "model2.xml";
    }
    public bool CompareByElement()
    {
        var file1Elements = GetsElementsFromFile(_fileOnePath);
        var file2Elements = GetsElementsFromFile(_fileTwoPath);

        var report = new StringBuilder();
        var diffrence = new StringBuilder();
        bool withValue = true;
        report.AppendLine("Algorytm rozpoczol porownywanie plikow");

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
        report.AppendLine(diffrence.ToString());
        bool difFile = false;

        saveToFile(report.ToString());
        return difFile;
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
        saveToFile(report.ToString());

        return difFile;
    }
    private Dictionary<string, string> GetsElementsFromFile(string filePath){
        //SPrwadz czy linia zwiera w sobie "<Element "
        //jesli tak to wez ta linie i zapisz jako klucz"
        //Nastepnie pobieraj wszystkie linie az do </Element>
        //Ale jesli linia ma w sobie inny <Element to powieksz zmienna deap o 1
        //Jesli deap bedzie rowne 0 to zakoncz pobieranie lini dla tego 
        //Zapisz to do kolekcji file1Elements 

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

    private void saveToFile(string report)
    {
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
    private Boolean CompareElement(string elementOne, string elementTwo)
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

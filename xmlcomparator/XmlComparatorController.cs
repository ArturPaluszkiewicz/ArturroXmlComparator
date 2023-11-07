using System.Text.RegularExpressions;

namespace xmlcomperator;

public class XmlComparatorController
{
    public XmlComparatorController(string[] args)
    {
        XmlComparator xmlComparator = new();
       if (args.Length > 0)
       {
            var whichArg = 0;
            foreach(var arg in args)
            {   
                if(arg == "-p" || arg == "--path")
                {
                    if(!CheckIfFileExist(args[whichArg+1])
                        || !CheckIfFileExist(args[whichArg+2])
                        || !CheckIfFileIsXml(args[whichArg+1])
                        || !CheckIfFileIsXml(args[whichArg+2])
                    )
                    {
                        xmlComparator.AddError("Checked file doesnt exist or is not xml file");
                    }
                    else
                    {
                        xmlComparator.SetFilesPath(args[whichArg+1],args[whichArg+2]);
                    }
                }
                if(arg == "-r" || arg == "--report")
                {
                        xmlComparator.SetReportPath(args[whichArg+1]);
                }
                if(arg == "-t" || arg == "--type")
                {
                    if(args[whichArg+1] == "line" || args[whichArg+1] == "l")
                    {
                        xmlComparator.ChangeType(xmlcomparator.ComparatorType.Line);
                    }
                    if(args[whichArg+1] == "onlyline" || args[whichArg+1] == "ol")
                    {
                        xmlComparator.ChangeType(xmlcomparator.ComparatorType.OnlyLine);
                    }
                    if(args[whichArg+1] == "elements" || args[whichArg+1] == "e")
                    {
                        xmlComparator.ChangeType(xmlcomparator.ComparatorType.Element);
                    }
                    if(args[whichArg+1] == "elementswithvalue" || args[whichArg+1] == "ewv")
                    {
                        xmlComparator.ChangeType(xmlcomparator.ComparatorType.ElementWithValue);
                    }
                    else
                    {
                        xmlComparator.AddError("Type argument has bad options, use -h or --help to check correct format");
                    }
                }
                whichArg++;
            }
        }
        xmlComparator.Compare();
    }

    public static bool CheckIfFileExist(string path)
    {
        return File.Exists(path);
    }
    public static bool CheckIfFileIsXml(string path)
    {
        string pattern = @"\w+\.xml";
        return Regex.IsMatch(path, pattern);
    }
}

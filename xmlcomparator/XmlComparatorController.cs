using System.Text.RegularExpressions;
using xmlcomparator;

namespace xmlcomperator;

public class XmlComparatorController
{
    private readonly IXmlComparator _xmlComparator;
    private readonly string[] _args;
    public XmlComparatorController(string[] args,IXmlComparator xmlComparator)
    {
       _xmlComparator = xmlComparator;
       _args = args;
    }
    public void Start(){
        if (_args.Length > 0)
       {
            var whichArg = 0;
            foreach(var arg in _args)
            {   
                if(arg == "-p" || arg == "--path")
                {
                    if(_args.Length>whichArg+1 && _args.Length>whichArg+2)
                    {
                        if(!CheckIfFileExist(_args[whichArg+1])
                        || !CheckIfFileExist(_args[whichArg+2])
                        || !CheckIfFileIsXml(_args[whichArg+1])
                        || !CheckIfFileIsXml(_args[whichArg+2])
                        )
                        {
                            _xmlComparator.AddError("Checked file doesnt exist or is not xml file");
                        }
                        else
                        {
                            _xmlComparator.SetFilesPath(_args[whichArg+1],_args[whichArg+2]);
                        }
                    }else
                    {
                        _xmlComparator.AddError("Bad path arguments");
                    }
                }
                if(arg == "-r" || arg == "--report")
                {
                    if(_args.Length>whichArg+1)
                    {
                        if(CheckIfFileIsTxt(_args[whichArg+1]))
                        {
                            _xmlComparator.SetReportPath(_args[whichArg+1]);
                        }
                        else
                        {
                            _xmlComparator.SetReportPath(_args[whichArg+1] + ".txt");
                        }
                    }
                    else
                    {
                        _xmlComparator.AddError("Bad report argument");
                    }    
                }
                if(arg == "-t" || arg == "--type")
                {
                    if(_args[whichArg+1] == "line" || _args[whichArg+1] == "l")
                    {
                        _xmlComparator.ChangeType(xmlcomparator.ComparatorType.Line);
                    }
                    if(_args[whichArg+1] == "onlyline" || _args[whichArg+1] == "ol")
                    {
                        _xmlComparator.ChangeType(xmlcomparator.ComparatorType.OnlyLine);
                    }
                    if(_args[whichArg+1] == "elements" || _args[whichArg+1] == "e")
                    {
                        _xmlComparator.ChangeType(xmlcomparator.ComparatorType.Element);
                    }
                    if(_args[whichArg+1] == "elementswithvalue" || _args[whichArg+1] == "ewv")
                    {
                        _xmlComparator.ChangeType(xmlcomparator.ComparatorType.ElementWithValue);
                    }
                    else
                    {
                        _xmlComparator.AddError("Type argument has bad options, use -h or --help to check correct format");
                    }
                }
                whichArg++;
            }
        }
        _xmlComparator.Compare();
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
    public static bool CheckIfFileIsTxt(string path)
    {
        string pattern = @"\w+\.txt";
        return Regex.IsMatch(path, pattern);
    }
}

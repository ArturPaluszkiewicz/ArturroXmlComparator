namespace xmlcomperator;

public class XmlComparatorController
{
    public XmlComparatorController(string[] args)
    {
        XmlComparator xmlComparator = new();
       if (args.Length == 0)
       {
            new XmlComparator();
       }
       else
       {
            var whichArg = 0;
            foreach(var arg in args)
            {   
                if(arg == "-p" || arg == "--path")
                {
                    xmlComparator.SetFilesPath(arg);
                }
                if(arg == "-r" || arg == "--report")
                {
                    xmlComparator.SetReportPath();
                }
                if(arg == "-t" || arg == "--type")
                {
                    if(args[whichArg+1] == "line")
                    {
                        xmlComparator.ChangeType(xmlcomparator.ComparatorType.Line);
                    }
                    if(args[whichArg+1] == "elements")
                    {
                        xmlComparator.ChangeType(xmlcomparator.ComparatorType.Element);
                    }
                }
                whichArg++;
            }
        }

    }
}

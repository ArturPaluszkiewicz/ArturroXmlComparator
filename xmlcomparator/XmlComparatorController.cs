namespace xmlcomperator;

public class XmlComparatorController
{
    public XmlComparatorController(string[] args)
    {
       if (args.Length == 0)
       {
            new XmlComparator("FileToTest3.xml","FileToTest2.xml").CompareByElement(false);
            
       }else {
            try{
                if(args[0] == "-t" || args[0] == "--type")
                {
                    if(args[1] == "line")
                    {
                        new XmlComparator().CompareLineByLine(false);
                    }
                    if(args[1] == "elements")
                    {
                        new XmlComparator().CompareByElement(false);
                    }
                }
            }catch(IndexOutOfRangeException e)
            {
                Console.Write("Wrongs comends, use -h or --help to see correct format");
            }
       }

    }
}

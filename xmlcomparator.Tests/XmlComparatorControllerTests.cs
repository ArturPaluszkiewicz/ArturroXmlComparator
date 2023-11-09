using System.Runtime.InteropServices;
using FluentAssertions;
using Moq;
using xmlcomperator;

namespace xmlcomparator.Tests;

public class XmlComparatorControllerTests
{
    [Theory]
    [InlineData("--path","asd","asd","asd")]
    [InlineData("-p","model.xml","asd","model.xml")]
    [InlineData("-p","asd","model.xml","model.xml")]
    [InlineData("-p","model","model","model.xml")]
    [InlineData("-p","model.xml","modelq","asd")]
    public void Start_WhenGivenPathArgumentIsNotValid_ShouldUseAddErrorFunction(string arg1, string arg2, string arg3, string arg4)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3,arg4};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.AddError("Checked file doesnt exist or is not xml file"), Times.Once);
    }

    [Theory]
    [InlineData("--path","model1.xml","model2.xml","")]
    [InlineData("-p","model1.xml","model2.xml","model.xml")]
    [InlineData("-p","-p","model1.xml","model2.xml")]
    [InlineData("asd","-p","model1.xml","model2.xml")]
    public void Start_WhenGivenPathArgumentIsValid_ShouldUseSetFilesPathFunction(string arg1, string arg2, string arg3, string arg4)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3,arg4};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        CreateFilesToTests();
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.SetFilesPath("model1.xml","model2.xml"),Times.Once);
    }

    [Theory]
    [InlineData("-path","model1.xml","model2.xml","-p")]
    [InlineData("-pat","model1.xml","model2.xml","--path")]
    [InlineData("-p","-p","-p","model2.xml")]
    [InlineData("asd","-p","--path","model2.xml")]
    public void Start_WhenNotGivePathArguments_ShouldUseAddErrorFunction(string arg1, string arg2, string arg3, string arg4)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3,arg4};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        CreateFilesToTests();
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.AddError("Bad path arguments"),Times.Once);
    }

    [Theory]
    [InlineData("--path","model1.xml","-r","report.txt")]
    [InlineData("--report","report","model2.xml","model.xml")]
    [InlineData("-repo","-r","report","model2.xml")]
    [InlineData("asd","--report","report.txt","model2.xml")]
    public void Start_WhenGivenReportArgument_ShouldUseSetReportPathFunction(string arg1, string arg2, string arg3, string arg4)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3,arg4};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.SetReportPath("report.txt"), Times.Once);
    }

    [Theory]
    [InlineData("-repo","-rara","report","-r")]
    [InlineData("asd","-report","report.txt","--report")]
    public void Start_WhenNotGiveReportArguments_ShouldUseAddReportFunction(string arg1, string arg2, string arg3, string arg4)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3,arg4};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.AddError("Bad report argument"), Times.Once);
    }

    [Theory]
    [InlineData("--type","onlyline","-raport",ComparatorType.OnlyLine)]
    [InlineData("-type","--type","line",ComparatorType.Line)]
    [InlineData("-repo","-t","elements",ComparatorType.Element)]
    [InlineData("asd","-t","elementswithvalue",ComparatorType.ElementWithValue)]
    [InlineData("--type","ol","asd",ComparatorType.OnlyLine)]
    [InlineData("asd","--type","l",ComparatorType.Line)]
    [InlineData("-t","e","report",ComparatorType.Element)]
    [InlineData("asd","-t","ewv",ComparatorType.ElementWithValue)]
    public void Start_WhenGivenValidTypeArgument_ShouldUseChangeTypeFunction(string arg1, string arg2, string arg3, ComparatorType type)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.ChangeType(type), Times.Once);
    }

    [Theory]
    [InlineData("-type","--type","-r","report.txt")]
    [InlineData("--type","report","elementswithvalue","model.xml")]
    [InlineData("-t","emv","-type","asd")]
    [InlineData("asd","--report","-t","")]
    public void Start_WhenGivenNotValidTypeArgument_ShouldUseAddError(string arg1, string arg2, string arg3, string arg4)
    {
        //Arrange
        var mocXmlComperator = new Mock<IXmlComparator>();
        string[] args = new string[] {arg1,arg2,arg3,arg4};
        var xmlController = new XmlComparatorController(args, mocXmlComperator.Object);
        //Act
        xmlController.Start();
        //Assert
        mocXmlComperator.Verify(x => x.AddError("Type argument has bad options, use -h or --help to check correct format"), Times.Once);
    }

    private static void CreateFilesToTests()
    {
        if(!File.Exists("model1.xml")){
            File.Create("model1.xml");
        }
        if(!File.Exists("model2.xml")){
            File.Create("model2.xml");
        }
    }
}

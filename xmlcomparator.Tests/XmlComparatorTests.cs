using xmlcomperator;

namespace xmlcomparator.Tests;

public class XmlComparatorTests
{
    [Fact]
    public void CompareLineByLine_WhenFileAreTheSame_ShouldDrawFilesMatchReport()
    {
        //Arrange
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest1.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLineCorrectResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act
        comperator.CompareLineByLine(false);
        var result = ReadFile("report.txt");
        //Assert
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFileAreTheSame_UsedWithOnlyLinesOptions_ShouldDrawFilesMatchReport()
    {
        //Arrange
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest1.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLineCorrectResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act
        comperator.CompareLineByLine(true);
        var result = ReadFile("report.txt");
        //Assert
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFileAreDiffrent_ShouldDrawFilesNotMatchReport()
    {
        //Arrange
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest2.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLine24DifferencesResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act
        comperator.CompareLineByLine(false);
        var result = ReadFile("report.txt");
        //Assert
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFileAreDifferent_UsedWithOnlyLinesOptions_ShouldDrawFilesNotMatchReport()
    {
        //Arrange
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest2.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLine24DifferencesResultOnlyLine.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act
        comperator.CompareLineByLine(true);
        var result = ReadFile("report.txt");
        //Assert
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFilesAreNotCorrect_ShouldDrawErrorReport()
    {
        //Arrange
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest3.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLineErrorResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act
        comperator.CompareLineByLine(true);
        var result = ReadFile("report.txt");
        //Assert
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void SaveToFile_WhenErrorNotEmpty_ShouldDrawErrorReport()
    {

    }

    [Fact]
    public void SaveToFile_WhenDifferencesNotEmpty_ShouldDrawNotMatchReport()
    {
        
    }

    [Fact]
    public void SaveToFile_WhenDifferencesAndErrorsAreEmpty_ShouldDrawMatchReport()
    {
        
    }

    private static string ReadFile(string pathToFile)
    {
        var sr = new StreamReader(pathToFile);
        string readedFile = "";
        try
        {
            readedFile = sr.ReadToEnd();
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Readed tests file failed");
            Console.WriteLine(e);
        }
        return readedFile;
    }
}
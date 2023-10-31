using xmlcomperator;

namespace xmlcomparator.Tests;

public class XmlComparatorTests
{
    [Fact]
    public void CompareLineByLine_WhenFileAreTheSame_ShouldDrawCorrectReport()
    {
        //Arrange - Zaladowac plik ze spodziewanym wynikiem testu, przygotowac pliki do porownania
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest1.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLineCorrectResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act - uzyc funkcji
        comperator.CompareLineByLine(false);
        var result = ReadFile("report.txt");
        //Assert - sprawdzic czy nowo utworzony plik report jest taki sam jak wczesniej przygotowany plik ze spodziewanym wynikiem
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFileAreTheSame_AndUsedWithOnlyLinesOptions_ShouldDrawCorrectReport()
    {
        //Arrange - Zaladowac plik ze spodziewanym wynikiem testu, przygotowac pliki do porownania
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest1.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLineCorrectResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act - uzyc funkcji
        comperator.CompareLineByLine(true);
        var result = ReadFile("report.txt");
        //Assert - sprawdzic czy nowo utworzony plik report jest taki sam jak wczesniej przygotowany plik ze spodziewanym wynikiem
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFileAreDiffrent_ShouldDrawCorrectReport()
    {
        //Arrange - Zaladowac plik ze spodziewanym wynikiem testu, przygotowac pliki do porownania
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest2.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLine24DifferencesResult.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act - uzyc funkcji
        comperator.CompareLineByLine(false);
        var result = ReadFile("report.txt");
        //Assert - sprawdzic czy nowo utworzony plik report jest taki sam jak wczesniej przygotowany plik ze spodziewanym wynikiem
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFileAreTheSameAndMethodUsedWithOnlyLinesOptions_ShouldDrawCorrectReport()
    {
        //Arrange - Zaladowac plik ze spodziewanym wynikiem testu, przygotowac pliki do porownania
        string fileOnePath = "../../../Tests_Resources/FileToTest1.xml";
        string fileTwoPath = "../../../Tests_Resources/FileToTest2.xml";
        string expectedResultFilePath = "../../../Tests_Resources/LineByLine24DifferencesResultOnlyLine.txt";
        var expectedResult = ReadFile(expectedResultFilePath);
        var comperator = new XmlComparator(fileOnePath,fileTwoPath);
        //Act - uzyc funkcji
        comperator.CompareLineByLine(true);
        var result = ReadFile("report.txt");
        //Assert - sprawdzic czy nowo utworzony plik report jest taki sam jak wczesniej przygotowany plik ze spodziewanym wynikiem
        Assert.Equal(expectedResult,result);
    }

    [Fact]
    public void CompareLineByLine_WhenFilesAreNotCorrect_ShouldDrawFileReport()
    {
        //Arrange
        //Act
        //Assert
    }

    private static string ReadFile(string pathToFile)
    {
        var sr = new StreamReader(pathToFile);
        string readedFile = "";
        try
        {
            readedFile = sr.ReadToEnd();
        }
        catch(Exception e)
        {
            Console.WriteLine("Readed tests file failed");
            Console.WriteLine(e);
        }
        return readedFile;
    }
}
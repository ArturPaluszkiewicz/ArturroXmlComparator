
# XmlComparator

Xml Comparator created especially for comparing dacpac files.

## Requirements

- [.NET Runtime 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Git](https://git-scm.com/downloads)

## How to use it:

1. Clone the selected version from the GitHub:
- In the command line, navigate to the folder to where you want to download files.
- Use the following git clone command:
    ```
    git clone https://github.com/ArturPaluszkiewicz/ArturroXmlComparator
    ```
- Switch to the version you want to use:
    ```
    git checkout version1.0
    ```
2. Set files to compare:
- Copy your files to this folder and rename them to "model1.xml" and "model2.xml".
- Alternatively, use the --path argument with the path to both files to compare, as shown in the example below.
3. Run the program.
- Type in the command prompt either "xmlcomparator.exe" or "dotnet xmlcomparator.dll".
4. Check your result. 
- Your result will be in the newly created "report.txt" file.
- Alternatively, you can specify the report file by using the --report argument.

## Exmaple
```
xmlcomparator --path pathToFile1.xml pathToFile2.xml --report MyResult --type ewv
```

## Arguments
- "--report" / "-r"  - Specify the file in which the program will print the result of comparing. The value for this argument is the path to the file; if the file doesn't exist, the program will create it.
- "--path" / "-p"  - Specify the files that will be compared. Two path values with existing .xml files are needed.
- "--type" / "-t"  - Specify how the program will be compare files. The values for this are:
  - "line" / "l" - The program will compare line by line. For example, if the second line in file one is different from the second line in file two, the program will print this line in the "Difference" section of the report.
  - "onlyline" / "ol" - Similar to the above, but the report will only include the number of the line where differences occur.
  - "elements" / "e" - The program will group "<element>" markup of XML files and check if any of this markup doesn't exist in seccond file or if exist but is diffrent. In report will be printed markup names in which differences occur.
  - "elementswithvalue" / "ewv" - Similar to the above, but the report will include markup names and the values of these markup.




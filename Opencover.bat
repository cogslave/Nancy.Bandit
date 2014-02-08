REM Run unit tests through OpenCover
REM This allows OpenCover to gather code coverage results
.\packages\OpenCover.4.5.2316\OpenCover.Console.exe^
 -target:".\packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe"^
 -targetargs:".\src\Nancy.Bandit.Tests\bin\Debug\Nancy.Bandit.Tests.dll /noshadow"^
 -filter:"+[*]* -[*]Guard -[Nancy.Bandit.Tests]*"^
 -register:user^
 -output:.\report\output.xml

REM Generate the report
.\packages\ReportGenerator.1.9.1.0\ReportGenerator.exe^
 -reports:.\report\output.xml^
 -targetdir:.\report^
 -reporttypes:Html,HtmlSummary^
 -filters:-MonitoredUndoTests*

REM Open the report
start .\report\index.htm

pause
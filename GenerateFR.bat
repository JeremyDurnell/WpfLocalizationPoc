SET locbaml="%~dp0\tools\locbaml.exe"

powershell.exe ".\Create-PseudoLocalization.ps1 .\WpfApplication1\bin\Release\en-US.csv .\fr.csv"

copy "%~dp0\fr.csv" "%~dp0\WpfApplication1\bin\Release" /y

pushd "%~dp0\WpfApplication1\bin\Release"

rd fr /s /q
md fr
copy %locbaml% . /y

locbaml /generate en-US/WpfApplication1.resources.dll /trans:fr.csv /out:fr /cul:fr

popd

pause
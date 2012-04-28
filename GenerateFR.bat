SET locbaml="%~dp0\tools\locbaml.exe"

powershell.exe ".\Create-PseudoLocalization.ps1 .\WpfApplication1\bin\Release\en-US.csv .\fr.csv"
powershell.exe ".\Merge-LocBamls.ps1 .\fr.csv.old .\fr.csv .\fr.merge.csv"

copy "%~dp0\fr.merge.csv" "%~dp0\WpfApplication1\bin\Release" /y

pushd "%~dp0\WpfApplication1\bin\Release"

rd fr /s /q
md fr
copy %locbaml% . /y

locbaml /generate en-US/WpfApplication1.resources.dll /trans:fr.merge.csv /out:fr /cul:fr

popd

pause
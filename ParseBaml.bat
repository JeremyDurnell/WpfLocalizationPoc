SET locbaml="%~dp0\tools\locbaml.exe"

pushd "%~dp0\WpfApplication1\bin\Release"

copy %locbaml% . /y

locbaml /parse en-US/WpfApplication1.resources.dll /out:en-US.csv

popd

pause
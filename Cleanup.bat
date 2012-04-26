pushd "%~dp0\WpfApplication1\bin\Release"
erase locbaml.exe 
erase *.csv /q
popd

pushd "%~dp0\WpfApplication1
rd obj /s /q
popd
set WORKSPACE=..

set GEN_CLIENT=%WORKSPACE%\Luban\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Luban\MiniDesignerConfigsTemplate

%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\Datas ^
 --output_code_dir Gen ^
 --output_data_dir ..\GenerateDatas\proto2 ^
 --gen_types code_protobuf2,data_protobuf_bin ^
 --output:code:monolithic_file test.proto ^
 -s all 

pause
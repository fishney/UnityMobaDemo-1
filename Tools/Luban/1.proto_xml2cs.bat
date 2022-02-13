set WORKSPACE=..

set GEN_CLIENT=%WORKSPACE%\Luban\Luban.ClientServer\Luban.ClientServer.exe
set PROTO_ROOT=%WORKSPACE%\Luban\MiniDesignerConfigsTemplate\ProtoDefines


%GEN_CLIENT% -j proto --^
 -d %PROTO_ROOT%\__root__.xml ^
 --output_code_dir %WORKSPACE%\Assets\Gen ^
 --gen_type cs ^
 --cs:use_unity_vector ^
 -s all 

pause
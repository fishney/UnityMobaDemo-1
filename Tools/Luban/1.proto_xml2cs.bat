set WORKSPACE=..

set GEN_CLIENT=%WORKSPACE%\Luban\Luban.ClientServer\Luban.ClientServer.exe
set PROTO_ROOT=%WORKSPACE%\Luban\MiniDesignerConfigsTemplate\ProtoDefines


%GEN_CLIENT% -j proto --^
 -d %PROTO_ROOT%\__root__.xml ^
 --output_code_dir %WORKSPACE%\Luban\Gen ^
 --naming_convention:module none  ^
 --naming_convention:bean_member none  ^
 --naming_convention:enum_member none  ^
 --gen_type cs ^
 --cs:use_unity_vector ^
 -s all 

pause
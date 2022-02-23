set WORKSPACE=..

set GEN_CLIENT=%WORKSPACE%\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Editor_Luban\Json2BinTemplate

%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\Datas ^
 --naming_convention:module none  ^
 --naming_convention:bean_member none  ^
 --naming_convention:enum_member none  ^
 --output_code_dir %WORKSPACE%\Gen\Cfg_Code ^
 --output_data_dir %WORKSPACE%\Gen\Cfg_Data ^
 --gen_types code_cs_bin,data_bin ^
 -s all 

pause
set WORKSPACE=..
set UnityBase=..\..

set GEN_CLIENT=%WORKSPACE%\Luban\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Luban\MiniDesignerConfigsTemplate

%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\Datas ^
 --naming_convention:module none  ^
 --naming_convention:bean_member none  ^
 --naming_convention:enum_member none  ^
 --output_code_dir %UnityBase%\Server\Server\99.Models\Gen_Cfg ^
 --output_data_dir %UnityBase%\Server\Datas\ResCfg ^
 --gen_types code_cs_bin,data_bin ^
 -s all 

pause
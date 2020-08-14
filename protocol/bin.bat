@echo off
echo 1234

::协议文件路径, 最后不要跟“\”符号
::set SOURCE_FOLDER=.\proto

::C#编译器路径
set CS_COMPILER_PATH=.\protoc.exe
::C#文件生成路径, 最后不要跟“\”符号
set CS_TARGET_PATH=.\

::Java编译器路径
::set JAVA_COMPILER_PATH=..\Tools\protoc\protoc.exe
::Java文件生成路径, 最后不要跟“\”符号
::set JAVA_TARGET_PATH=..\Server\GameServer\src

::删除之前创建的文件
::del %CS_TARGET_PATH%\*.* /f /s /q
::del %JAVA_TARGET_PATH%\*.* /f /s /q

::遍历所有文件
for %%i in (.\proto\*.proto) do (
	echo %CS_COMPILER_PATH% --csharp_out=%CS_TARGET_PATH%\ -I proto %%i 
	%CS_COMPILER_PATH% --csharp_out=%CS_TARGET_PATH%\ -I proto %%i 
)

  ::for %%i in (.\proto\*.proto) do (
  ::	echo %%i
    ::生成 C# 代码
    
	::echo %CS_COMPILER_PATH% -i:%%i -o:%CS_TARGET_PATH%\%%~ni.cs
    ::%CS_COMPILER_PATH% -i:%%i -o:%CS_TARGET_PATH%\%%~ni.cs
    
    ::生成 Java 代码
    ::echo %JAVA_COMPILER_PATH% --java_out=%JAVA_TARGET_PATH% %%i
    ::%JAVA_COMPILER_PATH% --java_out=%JAVA_TARGET_PATH% %%i
    
	  ::)
echo 协议生成完毕。
pause
@echo off
:: Compiles all Sass templates

:: `%~dp0` is the directory containing this file

setlocal

:: CONFIGURATION - update these variables with version changes

:: actual version
set requiredSassVersion=3.4.22
:: full command to install required gem
set installCommand=gem install sass -v '3.4.22' --pre
:: name output with `sass --version`
set requiredSassVersionPrintedName=Sass 3.4.22 (Selective Steve)

:: check for existence of gem command
call gem --version >nul 2>&1
if ERRORLEVEL 1 goto RubyNotInstalled

:: check for existence of sass commmand
call sass --version >nul 2>&1
if ERRORLEVEL 1 goto SassNotInstalled

:SassVersionCheck

:: runs sass version check again, this time to confirm proper version
for /F "tokens=*" %%i in ('sass --version') do set sassVersion=%%i

if not "%sassVersion%" == "%requiredSassVersionPrintedName%" goto SassWrongVersion

set command=update

if not "%watchSass%" == "" (
	set command=watch
)

:: not using this in the sass arguments because it doesn't like the format
cd %~dp0

:: Using `call` so that on error we can retrieve the error level and print the additional help text (instead of returning early)
@echo on
call sass --%command% --scss --sourcemap .:css-build
@echo off

IF ERRORLEVEL 1 goto CompileError

:: give some feedback on successful manual build
if "%command%" == "update" (
	if "%automaticBuild%" == "" (
		echo Sass compilation successful.
	)
)

:: no errors
goto Exit

:RubyNotInstalled
echo ------------------
echo Ruby does not appear to be installed, but is required to run the Sass gem used for compiling. Install Ruby and then run this script again.
echo.
echo -- Note: this warning will also display if Ruby is installed, but not exposed globally via the PATH variable. For details, see the "Adding Ruby to your PATH" section of the install instructions. --
if "%automaticBuild%" == "1" goto Exit
echo.
:: [Y/N]
choice /M "Would you like to launch installation instructions"
if "%errorlevel%" == "1" (
	echo If the instructions don't open in your browser automatically, go to https://github.com/hudl/hudl/wiki/Locally-Compiling-Front-end-Resources#installing-ruby
	start "" "https://github.com/hudl/hudl/wiki/Locally-Compiling-Front-end-Resources#installing-ruby"
)
goto Exit


:SassNotInstalled
echo ------------------
echo Sass does not appear to be installed.
goto PromptInstallSassAndTryAgain


:SassWrongVersion
echo ------------------
echo An incompatible version (%sassVersion%) of Sass is installed. Required version is (%requiredSassVersionPrintedName%).
goto PromptInstallSassAndTryAgain


:PromptInstallSassAndTryAgain
:: don't attempt install on build servers
if "%automaticBuild%" == "1" goto Exit
:: [Y/N]
choice /M  "Would you like to install it now"
if "%errorlevel%" == "1" (
	:: user answered "Y" - install sass
	echo Installing Sass gem...
	@echo on
	call %installCommand%
	@echo off

	IF ERRORLEVEL 0 (
		:: install success; try again
		echo Sass installed. Trying compilation again...
		goto SassVersionCheck
	)

	:: there was an install error
	echo ------------------
	echo There was an error installing Sass. Please try installing it manually or resolve any issues that were logged above and try this script again.
	echo ------------------
) else (
	echo Sass not installed. Please install manually using `%installCommand%` or run this script again.
)
goto Exit


:CompileError
echo ------------------
echo ERROR: There were Sass compilation errors.


:Exit
:: pause only when not runnning the full build
if "%automaticBuild%" == "" (
	pause
)

endlocal

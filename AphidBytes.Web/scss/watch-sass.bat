@echo off
:: Compiles all Sass templates, watching for modifications and updating

:: don't want this to persist between calls, just within the build-sass call
setlocal
set watchSass=1

%~dp0build-sass

endlocal

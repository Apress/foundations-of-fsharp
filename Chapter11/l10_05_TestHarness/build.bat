@if "%_echo%"=="" echo off

ECHO Build the sample...

fslex -o lex.fs lex.fsl
if ERRORLEVEL 1 goto Exit

fsyacc -o pars.fs pars.fsy
if ERRORLEVEL 1 goto Exit

:Exit

exit /b %ERRORLEVEL%


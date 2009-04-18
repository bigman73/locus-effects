@ECHO OFF
ECHO ---     Running NDoc...
"%ProgramFiles%\NDoc 1.3\bin\net\1.1\NDocConsole.exe" -project=LocusEffects.ndoc

copy doc\LocusEffects.chm .\

ECHO+
ECHO ---     Success: HTML Help (.chm) file created
pause


# cputempfanarduinothing

Request higher execution level
https://stackoverflow.com/a/37892024

Add a reference to the OpenHardwareMonitor dll
https://stackoverflow.com/a/12992312

After building/debugging

Use ILmerge to merge dll and exe
https://stackoverflow.com/questions/10137937/merge-dll-into-exe
Use ILmergeGUI From here
https://github.com/jpdillingham/ILMergeGUI/releases

Get rid of UAC 
https://www.sevenforums.com/tutorials/11949-elevated-program-shortcut-without-uac-prompt-create.html
(Not sure about this) When creating the task just set it to run at startup (i think that should work but i usually create the shortcut and put it in the startup folder anyways)

Just dropped the merged exe into C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp


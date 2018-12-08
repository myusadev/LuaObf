# LuaObf
An Obfuscator for Basic Lua Code In C#
All credits go to 3wayhimself/0x59, just wanted to commit stuff that's "being worked on"
https://i.imgur.com/pt9oNLy.png



Uh, stuff:
[0] Added ol' char-layout (e.g: l11ll1111l1 (I'll add an parameter to change char-layout l8r))
[1] Added string obfuscation (cough string.dump cough (cough char to int, int to string cough) ((cough buggy cough))


So, about this bug, https://vgy.me/mt9W2d.png, It happens when a variable is named as a char or a common thing.
e.g 
local n --> local Illll1IIl1
pri"n"t() --> priIllll1IIl1t()
very sad, can we play despacito?

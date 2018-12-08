# LuaObf<br />
An Obfuscator for Basic Lua Code In C#<br />
All credits go to 3wayhimself/0x59, just wanted to commit stuff that's "being worked on"<br />
https://i.imgur.com/pt9oNLy.png<br />
<br />
<br />
<br />
Uh, stuff:
[0] Added ol' char-layout (e.g: l11ll1111l1 (I'll add an parameter to change char-layout l8r))<br />
[1] Added string obfuscation (cough string.dump cough (cough char to int, int to string cough) ((cough buggy cough))<br />


So, about this bug, https://vgy.me/mt9W2d.png, It happens when a variable is named as a char or a common thing.<br />
e.g:<br />
local n --> local Illll1IIl1<br />
pri"n"t() --> priIllll1IIl1t()<br />
very sad, can we play despacito?<br />


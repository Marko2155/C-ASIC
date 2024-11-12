# ALERT!
A complete rewrite of C#ASIC is happening (will update once complete), head over to the rewrite branch for any new changes, eventually i'll just rename main to old and rewrite to main.
It is still based on Mono, so you won't need to do any installing to compile the rewrite too. Wiki for the rewrite will also be different, as it has a different system of error codes (now ERR codes) and a different language system, for example: In the normal C#ASIC: ```echo Hello World!```. In the rewrite of C#ASIc: ```PNT "Hello World!"```
See ya in the rewrite!

# C#ASIC
C#ASIC (had to remove the # from title because of GitHub) is a program where you can write and run code in a BASIC-like programming language (fittingly) named C#ASIC (pronounced CBASIC).

# How it works
C#ASIC is not a real language, it is written all in software to point to standard C# functions (hence the name C#ASIC). C#ASIC will halt if it finds a command that is not programmed in yet.
C#ASIC works almost excatly like my other CLI program, .OS, but has some changes.

# How to install
## Windows
Download the executable from the latest release, then move it to C:\Windows.
## Linux/Mac OS X
Download the UNIX executable (without the .exe part) to a place in the UNIX path that is not protected by the system, like `/usr/bin` or `/usr/local/bin`.
Or, just create a folder, move the cb executable to that folder then add it to path.

# How to compile
## Windows
First, install WSL and Ubuntu. Then, Install Mono and Git by typing ```sudo apt install mono-complete git```. Clone the repo using ```git clone https://github.com/Marko2155/C-ASIC.git```. Now, ```cd``` into the C#ASIC folder. Type ```mcs cb.cs``` Then find out how to transfer the cb.exe from the WSL filesystem to your host Windows filesystem.
## Linux
Install Mono and Git by typing ```sudo apt install mono-complete git```. Clone the repo using ```git clone https://github.com/Marko2155/C-ASIC.git```. Now, ```cd``` into the C#ASIC folder. Type ```mcs cb.cs```. Type ```mkbundle -o cb --cross default cb.exe```. If mkbundle isn't installed, find out how to install it.
## Mac OS X
Install Mono and Git by typing ```brew install mono``` and ```brew install git```. Clone the repo using ```git clone https://github.com/Marko2155/C-ASIC.git```. Now, ```cd``` into the C#ASIC folder. Type ```mcs cb.cs```. Type ```mkbundle -o cb --cross default cb.exe```. If mkbundle isn't installed, find out how to install it.

# Documentation
https://github.com/Marko2155/C-ASIC/wiki

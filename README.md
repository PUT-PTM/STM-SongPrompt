# STM-SongPrompt
## Overview
Display for Spotify, that shows on LED display information about track 
that we are listening in at the moment (auhor, title, time).

## Descritpion
The vital component of the project is STM32F407VG microcontroller and desktop 
application written in C# which is getting data from Spotify using its API. 
Our application runs in tray, first you need to connect with board by bluetooth, then it 
automatically connect to Spotify. Data which is received from Spotify is: author, title, time.
Then data is transfered through bluetooth to board and it is shown on LED display (2x16).
First row shows track title, in second row displayed are author of track and current time.

Data is send in prepared format:
`{author;title;time}

How are board and LED display and bluetooth module connected:
`(TO DO)`

## Tools
IDE: CoIDE v1.7.8 (for STM), Visual Studio 2015(for desktop application)

Languages: C (STM), C#(desktop application)


## How to run
To run you need to wire up everything, paste code for STM32 to your IDE (with all libraries), 
compile and flash to board. In case of desktop application, you can download compiled 
program (.exe file) and just run it or copy code and compile it using your IDE 
(with all libraries).

## How to compile
Best way is using CoIDE to compile code for STM board and to flash it on it.
To compile desktop application, use Microsoft Visual Studio, to your project you need to add reference to SpotifyAPI library. You can do it using:
Nuget(komenda)

## Future improvements
- add button feature that we can change LED display state.

## Attributions
### Used libraries:

Library used for getting data from Spotify: [SpotifyAPI-NET](https://github.com/JohnnyCrazy/SpotifyAPI-NET)

[LED Display library](https://stm32f4-discovery.net/2014/06/library-16-interfacing-hd44780-lcd-controller-with-stm32f4/)
### Used hardware:
LED Display HD44780
Bluetooth module

## License
Distrubted under MIT license.

## Credits
Rafał Jendraszak, Jonasz Świtała

The project was conducted during the Microprocessor Lab course held by the Institute of Control and Information Engineering, Poznan University of Technology.
Supervisor: Marek Kraft/Michał Fularz/Tomasz Mańkowski/Adam Bondyra

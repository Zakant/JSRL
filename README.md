# JSRL
A JavaScript robotic language designed for the LEGO EV3.

## Content
- [What is this] (https://github.com/Zakant/JSRL#what-is-this)
- [Getting Started] (https://github.com/Zakant/JSRL#getting-started)
- [Who should use] (https://github.com/Zakant/JSRL#who-shold-use)
- [Features] (https://github.com/Zakant/JSRL#features)
- [Roadmap] (https://github.com/Zakant/JSRL#roadmap)
- [Samples] (https://github.com/Zakant/JSRL#samples)

## What is this
This is a programming language that is easy-to-learn yet powerfull and designed for the the LEGO EV3, that offers more options then the standard "block based" language but is way easier to lern then JAVA or a .NET language.
To achive this, JSRL is based upon JavaScript extended with many objects and functions you need to programm a roboter. This whole project is mainly based on the JavaScript Interpreter [JInt](https://github.com/sebastienros/jint) and the [MonoBrick](https://github.com/Larsjep/monoev3) project. As a json parser a custom version of [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) is used. Make sure you check them out.
However even if the programms are written in JavaScript, they are highly extensible by using the interoperability provied by JInt.

## Getting started
At the moment of writting this, JSRL is in an early stage of development. Setting it up requires a bit of time and patient.
These are the steps you need to perform to run JSRL on your EV3:
1. Setup monobrick as described [here](http://www.monobrick.dk/software/ev3firmware/) (Under the "Getting started" category).
2. Make sure your EV3 has booted Monobrick correctly and you have a connection established to your computer (either by WLAN or by USB).
3. Dowload the latest version of JSRL. By now you have to compile it everytime you change your javascript code. This is topic to change in further releases! Check the [Roadmap]() below.
4. Transfer the programm to the EV3 and run it.
I know that by now this process is really frustrating and not as fluent as it should be. As mentioned this project is currently in a very early development stage - lacking many important features!


## Who shold use
Although this project was designed to fit most purpose, it will not fit every!
You should use this project if:
- you are just starting to explore the world of robotics and the block based language becomes way to narrow and limited.
- you just need a rapid prototype language. ( Type less => get more)
- you want to explorer what is possible which robotos.

You should probably shoudn't use this if:
- your application needs a top edege performance.
- you have tons of code already written in JAVA and don't want to port them

Even if there are reasons why this might not be the project you are looking for, i would kindly ask to give it a try. Maybe you get conviced.

## Features
Currently implemented features:
- Motor controll
- EV3, NXT and a few HiTechnic sensors
- LCD logging
- Multithreading

## Roadmap
Their are allways things to improve or create. Such would be:
- Macking JSRL an independant programm on the EV3. Allowing the user to choose the script they want to run and to upload scripts directly to the JSRL runtime.
- Add many classes from MonoBrick as wrappers to JSRL. 
- Add networking to JSRL.
- Add debbuging to JSRL as well as creating a debug application running on your computer.
- Creating a IDE to develop and deploy (and debug ?) your programms.

## Samples
### Simple movement
```javascript
MotorA.On(100);
MotorB.On(100);

delay(2000);

MotorA.Off(100);
MotorB.Off(100);
```

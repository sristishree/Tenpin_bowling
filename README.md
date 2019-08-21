# Tenpin_bowling
Unity3D - XBox Kinect based game for tenpin bowling.

There are 3 variables (in X direction) that are being controlled by user's gestures-
1) Speed settings
2) Position settings
3) Spin settings

## System Requirements
- Microsoft Kinect device
- MS Kinect Studio 2.0
- Unity Engine 5.6 (64 bit)
- Visual Studio 2015

## Building Game_Sample
Open the Game_Sample.sln solution file in Visual Studio and rebuild the project. 
If you want to directly run the game, open Ten_pin_rough.exe.

## Demo
Here is a demo on how to [play](https://www.behance.net/gallery/70237777/AR-based-Tenpin-Bowling).


## Description of Scripts
- BodySourceView_buttons.cs: It is used for interfacing between Hardware(Kinect) and software. It receives and manipulates the data obtained from the bodies tracked by Kinect. It has been attached with the BodyView object of the main page (Page_0) and uses the body data to perform appropriate hover and click operations on the buttons. The right hand's Y-direction movement can decide which buttons we are hovering on, and swipe gesture of the same hand simulates click on that button.
- BodySourceView.cs: Similar to the previous script, it receives data obtained from the bodies tracked by Kinect. It has been attached with the BodyView object of Speed settings page (Page_1) and uses the body data to perform sliding operation and setting the final speed according to hand movements. Slider value can be changed using right hand movement in Y-direction and final slider value can be fixed by using left hand (Stop gesture).
- BodySourceView_pos.cs: It has been attached with the BodyView object of Position settings page (Page_2) and uses the body data to perform sliding operation and setting the final X position of the ball according to left hand movements. Slider value can be changed using right hand movement in X-direction and final slider value can be fixed by using left hand (Stop gesture).

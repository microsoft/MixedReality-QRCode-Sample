---
page_type: sample
languages:
- csharp
products:
- windows
- mixed-reality
- hololens
---

# QR code tracking in Unity 

![License](https://img.shields.io/badge/license-MIT-green.svg)

This sample shows you how to use QR Codes in Unity projects using a HoloLens or Windows Mixed Reality immersive headset. Covered features include displaying a holographic square over QR codes and associated data such as:
* GUID
* Physical size
* Timestamp
* Decoded data

## Contents

| File/folder | Description |
|-------------|-------------|
| `Assets` | Unity assets, scenes, prefabs, and scripts. |
| `Packages` | Project manifest and packages list. |
| `ProjectSettings` | Unity asset setting files. |
| `UserSettings` | Generated user settings from Unity. |
| `.gitignore` | Define what to ignore at commit time. |
| `README.md` | This README file. |
| `LICENSE`   | The license for the sample. |

## Prerequisites

* Install the [latest Mixed Reality tools](https://docs.microsoft.com/windows/mixed-reality/develop/install-the-tools?tabs=unity)
* Install the [recommended Unity version](https://docs.microsoft.com/windows/mixed-reality/develop/install-the-tools?tabs=unity#install-your-engine-of-choice) 
* Download the [QR code NuGet package](https://www.nuget.org/Packages/Microsoft.MixedReality.QR)
* **For Windows Mixed Reality headsets**: QR code tracking on desktop PCs is only supported on Windows 10 Version 2004 and higher.

<!-- Unity 2020 with XR plugin -->

## Setup

1. Clone or download this sample repository.
2. Go to **Assets > Scenes** and open **QRCodesSample.unity**

<!-- Open SampleQRCodes folder in Unity Hub and load the default scene -->

## Running the sample

1. Hit **Play** in the Unity editor
2. In the HoloLens connected to Unity or with Remoting, a QR pops up in the scene in front of the user
3. When the QR code is read, a rectangle object is placed at the QR code coordinates 

## Key concepts

You can find the [full article on QR code tracking](https://docs.microsoft.com/windows/mixed-reality/develop/platform-capabilities-and-apis/qr-code-tracking) on Microsoft Docs. 

QRCode.cs - Object for each of the QR codes. Reads the QR code, class that represents the QR code. This script is attached to the QR code object and properties are filled on Start().

QRCodesManager.cs - Main class that handles the QR SDK. Loads the plugin, adding the event listeners, and waiting for the events to fire and uses the callback functions. Also has start and stop functions for tracking QR codes. Maintains a local list of QR codes. Enables Webcam functionality. App manifest for the build, UWP enable webcam capability when you build the app for Windows. All other setup is in the Start method.

QRCodesSetup.cs - Kicks off the QR code manager tracking functionality.

QRCodesVisualizer.cs - Does all the visualizing of the QR code in the scene, instantiates all QR codes in the local list and spawns them in the scene.

SpatialGraphCoordinateSystem.cs - Main script for transforming real-world QR code coordinates into Unity coordinate system and places the virtual QR code in the scene at that location. Must be attached to the QR code object.

## Best practices 

* **Quiet zones**: To be read correctly, QR codes require a margin around all sides of the code. This margin must not contain any printed content and should be four modules (a single black square in the code) wide. 
    * The [QR spec](https://www.qrcode.com/howto/code.html) contains more information about quiet zones.

* **Lighting and backdrop**: QR code detection quality is susceptible to varying illumination and backdrop. In a scene with bright lighting, print a code that is black on a gray background. Otherwise, print a black QR code on a white background. 
    * If the backdrop to the code is dark, try a black on gray code if your detection rate is low. If the backdrop is relatively light, a regular code should work fine.

* **Size**: Windows Mixed Reality devices don't work with QR codes with sides smaller than 5 cm each. For QR codes between 5 cm and 10-cm length sides, you must be fairly close to detect the code. It will also take longer to detect codes at this size. 
    * The exact time to detect codes depends not only on the size of the QR codes, but how far you're away from the code. Moving closer to the code will help offset issues with size.

* **Distance and angular position**: The tracking cameras can only detect a certain level of detail. For small codes - < 10 cm along the sides - you must be fairly close. For a version 1 QR code varying from 10 cm to 25 cm wide, the minimum detection distance ranges from 0.15 meters to 0.5 meters.
    * The detection distance for size increases linearly, but also depends on QR version or module size. The higher the version, the smaller the modules, which can only be detected from a closer position. You can also try micro QR codes if you want the distance of detection to be longer. QR detection works with a range of angles += 45 deg to ensure we have proper resolution to detect the code.

## Next steps

* [QR code tracking](https://docs.microsoft.com/windows/mixed-reality/develop/platform-capabilities-and-apis/qr-code-tracking#quiet-zones-around-qr-codes)
* [World locking and spatial anchors](https://docs.microsoft.com/windows/mixed-reality/design/spatial-anchors-in-unity)
* [HoloLens environment considerations](https://docs.microsoft.com/hololens/hololens-environment-considerations)

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.


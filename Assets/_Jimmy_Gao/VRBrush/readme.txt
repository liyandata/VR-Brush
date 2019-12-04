About VR Brush 1.7
This is a VR Brush system, it's easy to use and you can put it to your project. it's bases on SteamVR. 

steps:
1. import the package
2. make sure you set the VR support in project setting.
3. check the demo scene.
4. drag the BrushPlayer prefab to you scene
5. drag the BrushManager prefab to your Scene
6. drag the LineInit prefab to your Scene
7. check the BrushManager, set the line color and Hand obj, and lineInit obj, the same as demo scene.
8. Then you can play and sraw , that's all. enjoy!

SteamVR plugin is required
Please download it from: https://www.assetstore.unity3d.com/en/#!/content/32647
And you need a steamVR compatible HMD and controller (eg. HTC Vive)

The  Environment in video demo is from AxeyWorks, it's not included in this package,  it's free you can get it from: https://www.assetstore.unity3d.com/en/#!/content/58821


New Features in VR Brush 1.6:
1. New UI interface with laser beam pointer
2. Add Undo function
3. Add Clear function
4. Add Load and Save


If you want to access old 1.0 version , you can import the unity package in \_jimmy_gao\Old Version 

New Features in VR Brush 1.7
Support VRTK plugin
you need import both VRTK plugin and steamVR plugin first
VRTK url: https://assetstore.unity.com/packages/tools/vrtk-virtual-reality-toolkit-vr-toolkit-64131
Please check scene: \_Jimmy_Gao\VRBrush\VRTK\VRBrushWithVRTK.unity 
VRTK scene will add "None SDK" setting in XR setting. if you found you are not in VR mode , please remove NONE in: project setting -> Player-> XR Setting -> virtual reality SDK
and you turn off "Auto Manager VR Setting" in VRTK_SDKManager
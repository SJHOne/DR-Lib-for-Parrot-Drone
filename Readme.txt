-----------------------------------------------------------------
ARDrone.Net library for the Parrot AR Drone
Stephen Hobley, Thomas Endres and Julien Vinel November 2010
-----------------------------------------------------------------
The first thing to do is download the SDK from the Parrot development site - this will 
require that you install the DirectX library and the SDL library. 

http://ardrone.parrot.com/parrot-ar-drone/dev/developers

To compile this solution you must update the three path variables contained in the 
ArDroneProperties.props file.

    <WinSDKDir></WinSDKDir>
    <SDLDir></SDLDir>
    <DXSDKDir></DXSDKDir>

These should point to the respective folders on your harddrive. 

Once you have set these the solution should compile.
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;

#if !FRB_MDX
using System.Linq;
#endif

namespace LudumDare29
{
	internal static class CameraSetup
	{
			internal static void SetupCamera (Camera cameraToSetUp, GraphicsDeviceManager graphicsDeviceManager)
			{
				cameraToSetUp.UsePixelCoordinates(false, 550, 350);
			}
			internal static void ResetCamera (Camera cameraToReset)
			{
				cameraToReset.X = 0;
				cameraToReset.Y = 0;
				cameraToReset.XVelocity = 0;
				cameraToReset.YVelocity = 0;
			}

	}
}

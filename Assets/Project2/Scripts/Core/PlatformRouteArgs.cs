using System;
using UnityEngine;

public class PlatformRouteArgs : EventArgs
{
   public  readonly Vector3 Position;
   public readonly float TimeForRouteEnd;

   public PlatformRouteArgs(Vector3 position, float timeForRouteEnd)
   {
      Position = position;
      TimeForRouteEnd = timeForRouteEnd;
   }
}

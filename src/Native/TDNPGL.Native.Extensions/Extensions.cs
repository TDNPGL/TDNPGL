using System;
using System.Runtime.InteropServices;
using TDNPGL.Core.Math;

namespace TDNPGL.Native.Extensions
{
    public static class Extensions
    {
        public static bool IsPointOverNative(this AABB aabb, Vec2f point)
        {
            switch (Environment.OSVersion.Platform)
            {
                //Win
                case PlatformID.Win32S:
                    return NativeManager.IsPointOverNative<NativeWin32>(aabb, point);
                case PlatformID.WinCE:
                    goto case PlatformID.Win32S;
                case PlatformID.Win32Windows:
                    goto case PlatformID.Win32S;
                case PlatformID.Win32NT:
                    goto case PlatformID.Win32S;
                //Linux
                case PlatformID.Unix:
                    Architecture processArchitecture = 
                        RuntimeInformation.ProcessArchitecture;
                    switch (processArchitecture)
                    {
                        case Architecture.Arm64:
                            return NativeManager.IsPointOverNative<NativeLinuxARM64>(aabb, point);
                        case Architecture.X64:
                            return NativeManager.IsPointOverNative<NativeLinuxAMD64>(aabb, point);
                        case Architecture.X86:
                            return NativeManager.IsPointOverNative<NativeLinuxX86>(aabb, point);
                        default:
                            throw new PlatformNotSupportedException();
                    }
                default:
                    throw new PlatformNotSupportedException();
            }
        }
    }
}

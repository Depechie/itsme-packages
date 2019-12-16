using System.Runtime.InteropServices;
using System;

namespace Itsme
{
    internal class Wrapper
    {
        internal const string ITSME_LIB = "itsme_lib.dll";

        [DllImport(ITSME_LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Init(string settings);

        [DllImport(ITSME_LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Response GetAuthenticationURL(string config);

        [DllImport(ITSME_LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Response GetUserDetails(string redirectData);

        [DllImport(ITSME_LIB, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Response CreateRequestURIPayload(string config);
    }
}

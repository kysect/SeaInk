using System;
using SeaInk.Core.Entities;

namespace SeaInk.Core.Services
{
    public static class AuthService
    {
        public static readonly UniversitySystemUser CurrentUser = null;

        public static bool HasDriveAccess()
        {
            //TODO: Resolve safety cringe
            return true;
        }
        
        public static void AuthWithUniversitySystem()
        {
            throw new NotImplementedException();
        }

        public static void AuthWithGoogle()
        {
            throw new NotImplementedException();
        }
    }
}
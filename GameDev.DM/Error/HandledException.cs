using System;

namespace GameDev.DM.Error
{
    public class HandledException : Exception
    {
        public HandledException(string message) : base(message)
        {
        }
    }
}
using System;

namespace Events
{
    /// <summary>
    /// Global event definitions for decoupled communication between systems.
    /// </summary>
    public static class GameEvents
    {
        public static Action OnPlayPressed;
        public static Action OnBackPressed;
        public static Action OnRetryPressed;
    }
}
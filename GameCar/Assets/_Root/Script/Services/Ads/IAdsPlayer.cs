using System;

namespace Services.Ads
{
    internal interface IAdsPlayer
    {
        event Action Started;
        event Action Finished;
        event Action Failed;
        event Action Skiped;
        event Action BecomeReady;
        void Play();
    }
}

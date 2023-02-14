using System;

namespace General
{
    public interface IProgressProvider
    {
        event Action<ProgressChangedArgs> OnProgressChanged;
    }

    public struct ProgressChangedArgs
    {
        public float progressNormalized;
    }
}
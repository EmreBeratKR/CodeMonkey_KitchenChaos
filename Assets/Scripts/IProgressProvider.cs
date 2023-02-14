using System;

public interface IProgressProvider
{ 
    Action<float> OnProgressChanged { get; set; }
}
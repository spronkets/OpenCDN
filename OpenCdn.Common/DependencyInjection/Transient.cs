using System;

namespace OpenCdn.Common.DependencyInjection
{
    /// <summary>
    /// Use to always create a new instance when injected.
    /// </summary>
    public class Transient : Attribute
    {
    }
}

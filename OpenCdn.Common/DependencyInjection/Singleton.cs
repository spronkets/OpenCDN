using System;

namespace OpenCdn.Common.DependencyInjection
{
    /// <summary>
    /// Use to only create a single instance per lifetime scope when injected.
    /// </summary>
    public class Singleton : Attribute
    {
    }
}

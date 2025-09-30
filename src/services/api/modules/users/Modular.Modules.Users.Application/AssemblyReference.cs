using System.Reflection;

namespace Modular.Modules.Users.Application;

/// <summary>
///     Serves as a 'marker' class for the assembly.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    ///     Static reference to the assembly which contains this class.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
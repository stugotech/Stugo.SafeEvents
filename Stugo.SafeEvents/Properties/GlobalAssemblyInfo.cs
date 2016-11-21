using System.Reflection;

[assembly: AssemblyDescription("Provides an event architecture which uses WeakReferences to avoid memory leaks")]
[assembly: AssemblyCompany("Stugo Ltd")]
[assembly: AssemblyProduct("Stugo.SafeEvents")]
[assembly: AssemblyCopyright("Copyright © Stugo Ltd 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif

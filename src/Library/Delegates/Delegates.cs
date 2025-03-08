using System.ComponentModel;

namespace DigitalProduction.Delegates;

#region Delegates

/// <summary>
/// General call back delegate.  Can be used to update the progress bar, close the form, et cetera via a call back function from another thread.
/// </summary>
[Obsolete("CallBack is depricated, use Action instead.")]
public delegate void CallBack();

/// <summary>
/// A generic delegate for events that do not have arguments.
/// </summary>
[Obsolete("NoArgumentsEventHandler is depricated, use Action instead.")]
public delegate void NoArgumentsEventHandler();

/// <summary>
/// A delegate for when errors occur and notification is required.
/// </summary>
[Obsolete("OnErrorEventHandler is depricated, use Action<string> instead.")]
public delegate void OnErrorEventHandler(string message);

#endregion
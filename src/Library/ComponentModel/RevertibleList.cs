namespace DigitalProduction.ComponentModel;

/// <summary>
/// Provides and interface for an list of objects that are revertible.
/// </summary>
public class RevertibleList<T> : List<T> where T : IRevertible
{
	#region Construction

	/// <summary>
	/// Constructor.
	/// </summary>
	public RevertibleList() {}

	#endregion

	#region Begin/Reject/Accept Changes

	/// <summary>
	/// Beginning of the edit.  Current state should be saved.
	/// </summary>
	public void BeginEdit()
	{
		int size = Count;
		for (int i = 0; i < size; i++)
		{
			this[i].BeginEdit();
		}
	}

	/// <summary>
	/// Reject the changes made since the last BeginEdit.
	/// </summary>
	public void RejectChanges()
	{
		int size = Count;
		for (int i = 0; i < size; i++)
		{
			this[i].RejectChanges();
		}
	}

	/// <summary>
	/// Accept (commit) the changes made since the last BeginEdit.
	/// </summary>
	public void AcceptChanges()
	{
		int size = Count;
		for (int i = 0; i < size; i++)
		{
			this[i].AcceptChanges();
		}
	}

	#endregion

} // End class.
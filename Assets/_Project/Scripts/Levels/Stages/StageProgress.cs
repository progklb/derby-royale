namespace DerbyRoyale.Levels
{
	/// <summary>
	/// Represents various important points in a <see cref="Stage"/> progress.
	/// </summary>
	public enum StageProgress
	{
		/// Indicates the start of a stage
		Begin,
		/// Indicates beginning of a completion of a stage.
		CompletedBegin,
		/// Indicates end of a completion of a stage.
		CompletedEnd,
		/// Indicates the destruction of a stage.
		Destroyed
	}
}
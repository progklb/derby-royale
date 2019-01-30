using System.ComponentModel;

namespace DerbyRoyale.Input
{ 
	/// <summary>
	/// The types of input available in the game, with each having an attach <see cref="DescriptionAttribute"/> that corresponds to the input string name.
	/// </summary>
	public enum InputType
	{
		[Description("Horizonal")] Horizontal,
		[Description("Vertical")] Vertical,
		[Description("Recover")] Recover,
		[Description("Fire")] Fire
	}

	/// <summary>
	/// The mapping for each full set of input axes (per player).
	/// Each description is to be appended to <see cref="InputType"/> descriptions to get the final input string for the player.
	/// Note that each enum value uniquely represents the set of input.
	/// </summary>
	public enum InputSet
	{
		[Description("_K1")] KeyboardP1 = 1,
		[Description("_K2")] KeyboardP2 = 2
	}
}

using System.ComponentModel;
using System;
using System.Reflection;

namespace DerbyRoyale.Utilities
{
	/// <summary>
	/// Extensions for enums.
	/// </summary>
	public static class EnumUtility
	{
		#region PUBLIC API
		/// <summary>
		/// Returns the description of an enum with an attach <see cref="DescriptionAttribute"/>.
		/// </summary>
		/// <returns>The description attach to the enum.</returns>
		/// <param name="enumValue">Enum.</param>
		public static string GetDescription(this Enum enumValue)
		{
			var type = enumValue.GetType();
			string name = Enum.GetName(type, enumValue);

			if (name != null)
			{
				FieldInfo field = type.GetField(name);

				if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
				{
					return attr.Description;
				}
			}
			return null;
		}
		#endregion
	}
}

using System.Collections.Generic;
using System.Globalization;

namespace Captcha.Samples.Shared.Web
{
	public interface ICultureContext
	{
		#region Properties

		CultureInfo Culture { get; }
		string CultureParameterName { get; }
		IDictionary<CultureInfo, bool> Cultures { get; }
		bool Initialized { get; }
		CultureInfo PreferredCulture { get; }
		CultureInfo UiCulture { get; }

		#endregion

		#region Methods

		void Initialize();
		void SetCulture(string preferredCultureName);

		#endregion
	}
}
using System;
using System.Web.UI;
using Captcha.Samples.Application.Models;

namespace Captcha.Samples.Application.Views.Shared
{
	public interface ILayoutView : IView<LayoutModel>
	{
		#region Events

		event EventHandler PreRender;

		#endregion

		#region Properties

		Control CultureControl { get; }
		Control HtmlControl { get; }
		Control InformationControl { get; }
		Control NavigationControl { get; }

		#endregion
	}
}
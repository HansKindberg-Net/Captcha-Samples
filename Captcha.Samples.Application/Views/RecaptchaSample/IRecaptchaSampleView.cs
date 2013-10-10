using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Views.Shared;

namespace Captcha.Samples.Application.Views.RecaptchaSample
{
	public interface IRecaptchaSampleView : IView<RecaptchaSampleModel>
	{
		#region Events

		event EventHandler PreRender;
		event EventHandler Submit;

		#endregion

		#region Properties

		Control ConfirmationControl { get; }
		bool IsPostBack { get; }
		IRecaptchaView RecaptchaView { get; }
		ListControl ThemeControl { get; }
		Control ValidationResultControl { get; }

		#endregion
	}
}
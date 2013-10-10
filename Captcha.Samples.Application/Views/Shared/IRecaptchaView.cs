using Captcha.Samples.Application.Business.Recaptcha;
using Captcha.Samples.Application.Models;

namespace Captcha.Samples.Application.Views.Shared
{
	public interface IRecaptchaView : IView<RecaptchaModel>
	{
		#region Properties

		bool IsPostBack { get; }
		Theme Theme { get; set; }

		#endregion

		#region Methods

		void DataBind();

		#endregion
	}
}
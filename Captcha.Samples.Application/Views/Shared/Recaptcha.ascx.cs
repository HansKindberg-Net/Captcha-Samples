using Captcha.Samples.Application.Business.Recaptcha;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Shared.Web.Mvp.UI.Views;

namespace Captcha.Samples.Application.Views.Shared
{
	public partial class Recaptcha : UserControl<RecaptchaModel>, IRecaptchaView
	{
		#region Properties

		public virtual Theme Theme { get; set; }

		#endregion
	}
}
using System;
using Captcha.Samples.Application.Business.Recaptcha;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Views.RecaptchaSample;
using Captcha.Samples.Shared.Mvp.Models;
using Captcha.Samples.Shared.Mvp.Presenters;

namespace Captcha.Samples.Application.Presenters
{
	public class RecaptchaSamplePresenter : Presenter<IRecaptchaSampleView>
	{
		#region Constructors

		public RecaptchaSamplePresenter(IRecaptchaSampleView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
			this.View.PreRender += this.OnViewPreRender;
			this.View.Submit += this.OnViewSubmit;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<RecaptchaSampleModel>();

			if(this.View.IsPostBack)
			{
				this.View.RecaptchaView.Theme = (Theme) Enum.Parse(typeof(Theme), this.View.ThemeControl.SelectedValue, true);
				return;
			}

			this.View.ThemeControl.SelectedValue = RecaptchaModel.DefaultTheme.ToString().ToLowerInvariant();
			this.View.ThemeControl.DataBind();
		}

		protected internal virtual void OnViewPreRender(object sender, EventArgs e)
		{
			this.View.ConfirmationControl.DataBind();
			this.View.ValidationResultControl.DataBind();
		}

		protected internal virtual void OnViewSubmit(object sender, EventArgs e)
		{
			this.View.Model.Validate = true;
		}

		#endregion
	}
}
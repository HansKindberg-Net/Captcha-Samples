using System;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Views.Shared;
using Captcha.Samples.Shared.Mvp.Models;
using Captcha.Samples.Shared.Mvp.Presenters;

namespace Captcha.Samples.Application.Presenters
{
	public class RecaptchaPresenter : Presenter<IRecaptchaView>
	{
		#region Constructors

		public RecaptchaPresenter(IRecaptchaView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<RecaptchaModel>();

			if(this.View.IsPostBack)
				this.View.Model.Theme = this.View.Theme;

			this.View.DataBind();
		}

		#endregion
	}
}
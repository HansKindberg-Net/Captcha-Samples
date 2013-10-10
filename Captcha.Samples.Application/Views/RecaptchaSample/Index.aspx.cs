using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Presenters;
using Captcha.Samples.Application.Views.Shared;
using Captcha.Samples.Shared.Web.Mvp.UI.Views;
using WebFormsMvp;

namespace Captcha.Samples.Application.Views.RecaptchaSample
{
	[PresenterBinding(typeof(RecaptchaSamplePresenter))]
	public partial class Index : Page<RecaptchaSampleModel>, IRecaptchaSampleView
	{
		#region Events

		public event EventHandler Submit;

		#endregion

		#region Properties

		public virtual Control ConfirmationControl
		{
			get { return this.confirmationPlaceHolder; }
		}

		public virtual IRecaptchaView RecaptchaView
		{
			get { return this.recaptchaControl; }
		}

		public virtual ListControl ThemeControl
		{
			get { return this.themeDropDownList; }
		}

		public virtual Control ValidationResultControl
		{
			get { return this.validationResultRepeater; }
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnSubmit(object sender, EventArgs e)
		{
			this.OnSubmit(e);
		}

		protected internal virtual void OnSubmit(EventArgs e)
		{
			if(this.Submit != null)
				this.Submit(this, e);
		}

		#endregion
	}
}
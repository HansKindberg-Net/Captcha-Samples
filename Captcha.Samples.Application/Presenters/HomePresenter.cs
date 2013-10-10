using System;
using System.Diagnostics.CodeAnalysis;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Views.Home;
using Captcha.Samples.Shared.Mvp.Models;
using Captcha.Samples.Shared.Mvp.Presenters;

namespace Captcha.Samples.Application.Presenters
{
	public class HomePresenter : Presenter<IHomeView>
	{
		#region Constructors

		public HomePresenter(IHomeView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<HomeModel>();
		}

		#endregion
	}
}
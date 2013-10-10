using System;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Views.Shared;
using Captcha.Samples.Shared.Mvp.Models;
using Captcha.Samples.Shared.Mvp.Presenters;

namespace Captcha.Samples.Application.Presenters
{
	public class LayoutPresenter : Presenter<ILayoutView>
	{
		#region Constructors

		public LayoutPresenter(ILayoutView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
			this.View.PreRender += this.OnViewPreRender;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<LayoutModel>();
		}

		protected internal virtual void OnViewPreRender(object sender, EventArgs e)
		{
			this.View.CultureControl.DataBind();
			this.View.HtmlControl.DataBind();
			this.View.InformationControl.DataBind();
			this.View.NavigationControl.DataBind();
		}

		#endregion
	}
}
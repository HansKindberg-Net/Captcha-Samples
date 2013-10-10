using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Presenters;
using Captcha.Samples.Shared.Web.Mvp.UI.Views;
using WebFormsMvp;

namespace Captcha.Samples.Application.Views.Shared
{
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	[PresenterBinding(typeof(LayoutPresenter))]
	public partial class Layout : MasterPage<LayoutModel>, ILayoutView
	{
		#region Properties

		public virtual Control CultureControl
		{
			get { return this.cultureRepeater; }
		}

		public virtual Control HtmlControl
		{
			get { return this.htmlPlaceHolder; }
		}

		public virtual Control InformationControl
		{
			get { return this.informationPlaceHolder; }
		}

		public virtual Control NavigationControl
		{
			get { return this.navigationPlaceHolder; }
		}

		#endregion
	}
}
using System;
using Captcha.Samples.Shared.IoC;
using Captcha.Samples.Shared.Mvp.Views;
using WebFormsMvp.Web;

namespace Captcha.Samples.Shared.Web.Mvp.UI.Views
{
	public class Page : System.Web.UI.Page, IView
	{
		#region Fields

		private readonly ICultureContext _cultureContext;

		#endregion

		#region Constructors

		public Page() : this(ServiceLocator.Instance.GetService<ICultureContext>()) {}

		protected internal Page(ICultureContext cultureContext)
		{
			if(cultureContext == null)
				throw new ArgumentNullException("cultureContext");

			this._cultureContext = cultureContext;
		}

		#endregion

		#region Properties

		public virtual bool AutoDataBind
		{
			get { return false; }
		}

		protected internal virtual ICultureContext CultureContext
		{
			get { return this._cultureContext; }
		}

		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Methods

		protected override void InitializeCulture()
		{
			this.CultureContext.Initialize();

			base.InitializeCulture();
		}

		protected override void OnInit(EventArgs e)
		{
			PageViewHost.Register(this, this.Context, this.AutoDataBind);

			base.OnInit(e);
		}

		#endregion
	}

	public class Page<TModel> : Page, IView<TModel>
	{
		#region Properties

		public virtual TModel Model { get; set; }

		#endregion
	}
}
using System;
using Captcha.Samples.Shared.Mvp.Models;
using Captcha.Samples.Shared.Mvp.Views;

namespace Captcha.Samples.Shared.Mvp.Presenters
{
	public abstract class Presenter<TView> : WebFormsMvp.Presenter<TView> where TView : class, IView
	{
		#region Fields

		private readonly IModelFactory _modelFactory;

		#endregion

		#region Constructors

		protected Presenter(TView view, IModelFactory modelFactory) : base(view)
		{
			if(modelFactory == null)
				throw new ArgumentNullException("modelFactory");

			this._modelFactory = modelFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IModelFactory ModelFactory
		{
			get { return this._modelFactory; }
		}

		#endregion
	}
}
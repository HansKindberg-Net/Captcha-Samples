using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Captcha.Samples.Shared.Web;

namespace Captcha.Samples.Application.Models
{
	public class LayoutModel
	{
		#region Fields

		private readonly ICultureContext _cultureContext;
		private readonly string _currentFilePath;

		#endregion

		#region Constructors

		public LayoutModel(HttpRequestBase httpRequest, ICultureContext cultureContext)
		{
			if(httpRequest == null)
				throw new ArgumentNullException("httpRequest");

			if(cultureContext == null)
				throw new ArgumentNullException("cultureContext");

			this._cultureContext = cultureContext;
			this._currentFilePath = httpRequest.FilePath;
		}

		#endregion

		#region Properties

		public virtual CultureInfo Culture
		{
			get { return this.CultureContext.Culture; }
		}

		protected virtual ICultureContext CultureContext
		{
			get { return this._cultureContext; }
		}

		public virtual string CultureParameterName
		{
			get { return this.CultureContext.CultureParameterName; }
		}

		public virtual IDictionary<CultureInfo, bool> Cultures
		{
			get { return this.CultureContext.Cultures; }
		}

		public virtual string CurrentFilePath
		{
			get { return this._currentFilePath; }
		}

		public virtual CultureInfo PreferredCulture
		{
			get { return this.CultureContext.PreferredCulture; }
		}

		public virtual CultureInfo UiCulture
		{
			get { return this.CultureContext.UiCulture; }
		}

		#endregion
	}
}
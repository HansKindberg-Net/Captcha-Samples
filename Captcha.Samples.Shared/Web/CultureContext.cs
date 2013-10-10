using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Captcha.Samples.Shared.Web
{
	public class CultureContext : ICultureContext
	{
		#region Fields

		private readonly NameValueCollection _applicationSettings;
		private const string _cultureParameterName = "Culture";
		private IDictionary<CultureInfo, bool> _cultures;
		private readonly HttpContextBase _httpContext;
		private bool _initialized;
		private CultureInfo _preferredCulture;

		#endregion

		#region Constructors

		public CultureContext(HttpContextBase httpContext, NameValueCollection applicationSettings)
		{
			if(httpContext == null)
				throw new ArgumentNullException("httpContext");

			if(applicationSettings == null)
				throw new ArgumentNullException("applicationSettings");

			this._applicationSettings = applicationSettings;
			this._httpContext = httpContext;
		}

		#endregion

		#region Properties

		protected internal virtual NameValueCollection ApplicationSettings
		{
			get { return this._applicationSettings; }
		}

		public virtual CultureInfo Culture
		{
			get
			{
				if(!this.Initialized)
					this.Initialize();

				return Thread.CurrentThread.CurrentCulture;
			}
		}

		public virtual string CultureParameterName
		{
			get { return _cultureParameterName; }
		}

		public virtual IDictionary<CultureInfo, bool> Cultures
		{
			get
			{
				if(this._cultures == null)
					this.PopulateCultures();

				return this._cultures;
			}
		}

		protected internal virtual HttpContextBase HttpContext
		{
			get { return this._httpContext; }
		}

		public virtual bool Initialized
		{
			get { return this._initialized; }
		}

		public virtual CultureInfo PreferredCulture
		{
			get { return this._preferredCulture; }
		}

		public virtual CultureInfo UiCulture
		{
			get
			{
				if(!this.Initialized)
					this.Initialize();

				return Thread.CurrentThread.CurrentUICulture;
			}
		}

		#endregion

		#region Methods

		protected internal virtual bool CultureExists(string cultureName)
		{
			return !string.IsNullOrEmpty(cultureName) && CultureInfo.GetCultures(CultureTypes.AllCultures).Any(culture => culture.Name.Equals(cultureName, StringComparison.OrdinalIgnoreCase));
		}

		protected internal virtual string GetPreferredCultureName(HttpContextBase httpContext)
		{
			if(httpContext == null)
				throw new ArgumentNullException("httpContext");

			string preferredCultureName = null;

			HttpRequestBase httpRequest = httpContext.Request;

			if(httpRequest != null)
			{
				preferredCultureName = httpRequest.QueryString[this.CultureParameterName];

				if(!this.CultureExists(preferredCultureName) && httpRequest.Cookies != null)
				{
					HttpCookie httpCookie = httpRequest.Cookies[this.CultureParameterName];

					if(httpCookie != null)
						preferredCultureName = httpCookie.Value;
				}
			}

			if(!this.CultureExists(preferredCultureName))
			{
				HttpSessionStateBase httpSession = httpContext.Session;

				if(httpSession != null)
					preferredCultureName = httpSession[this.CultureParameterName] as string;
			}

			if(!this.CultureExists(preferredCultureName) && httpRequest != null)
			{
				foreach(var userLanguage in (httpRequest.UserLanguages ?? new string[0]).Where(this.CultureExists))
				{
					preferredCultureName = userLanguage;
					break;
				}
			}

			if(!this.CultureExists(preferredCultureName))
				preferredCultureName = Thread.CurrentThread.CurrentCulture.Name;

			return preferredCultureName;
		}

		public virtual void Initialize()
		{
			if(this.Initialized)
				throw new InvalidOperationException("Already initialized.");

			this.SetCulture(this.GetPreferredCultureName(this.HttpContext));
		}

		protected internal virtual void PopulateCultures()
		{
			this.PopulateCultures(null);
		}

		protected internal virtual void PopulateCultures(CultureInfo preferredCulture)
		{
			List<CultureInfo> cultures = new List<CultureInfo>();

			foreach(var cultureName in (this.ApplicationSettings["Cultures"] ?? string.Empty).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(cultureName => cultureName.Trim()).Where(this.CultureExists))
			{
				CultureInfo culture = CultureInfo.GetCultureInfo(cultureName);

				if(!cultures.Contains(culture))
					cultures.Add(culture);
			}

			foreach(var userLanguage in (this.HttpContext.Request.UserLanguages ?? new string[0]).Where(this.CultureExists))
			{
				CultureInfo culture = CultureInfo.GetCultureInfo(userLanguage);

				if(!cultures.Contains(culture))
					cultures.Add(culture);
			}

			if(preferredCulture != null && !cultures.Contains(preferredCulture))
				cultures.Add(preferredCulture);

			cultures.Sort((firstCulture, secondCulture) => string.Compare(firstCulture.NativeName, secondCulture.NativeName, StringComparison.Ordinal));

			this._cultures = new Dictionary<CultureInfo, bool>();

			foreach(var culture in cultures)
			{
				this._cultures.Add(culture, preferredCulture != null && preferredCulture.Equals(culture));
			}
		}

		protected internal virtual void SavePreferredCulture(HttpResponseBase httpResponse, CultureInfo preferredCulture)
		{
			if(httpResponse == null)
				throw new ArgumentNullException("httpResponse");

			if(preferredCulture == null)
				throw new ArgumentNullException("preferredCulture");

			httpResponse.Cookies.Set(new HttpCookie(this.CultureParameterName, preferredCulture.Name));
		}

		protected internal virtual void SavePreferredCulture(HttpSessionStateBase httpSession, CultureInfo preferredCulture)
		{
			if(httpSession == null)
				throw new ArgumentNullException("httpSession");

			if(preferredCulture == null)
				throw new ArgumentNullException("preferredCulture");

			httpSession[this.CultureParameterName] = preferredCulture.Name;
		}

		public virtual void SetCulture(string preferredCultureName)
		{
			this._preferredCulture = CultureInfo.GetCultureInfo(preferredCultureName);

			this.PopulateCultures(this._preferredCulture);

			CultureInfo culture = this._preferredCulture.IsNeutralCulture ? CultureInfo.CreateSpecificCulture(preferredCultureName) : this._preferredCulture;
			CultureInfo uiCulture = this._preferredCulture.IsNeutralCulture ? this._preferredCulture : this._preferredCulture.Parent;

			this.SetCulture(Thread.CurrentThread, culture, uiCulture);

			if(this.HttpContext != null)
			{
				if(this.HttpContext.Handler != null)
					this.SetCulture(this.HttpContext.Handler, culture, uiCulture);

				if(this.HttpContext.Response != null)
					this.SavePreferredCulture(this.HttpContext.Response, this._preferredCulture);

				if(this.HttpContext.Session != null)
					this.SavePreferredCulture(this.HttpContext.Session, this._preferredCulture);
			}

			this._initialized = true;
		}

		protected internal virtual void SetCulture(IHttpHandler httpHandler, CultureInfo culture, CultureInfo uiCulture)
		{
			if(httpHandler == null)
				throw new ArgumentNullException("httpHandler");

			if(culture == null)
				throw new ArgumentNullException("culture");

			if(uiCulture == null)
				throw new ArgumentNullException("uiCulture");

			Page page = httpHandler as Page;

			if(page == null)
				return;

			page.Culture = culture.Name;
			page.UICulture = uiCulture.Name;
		}

		protected internal virtual void SetCulture(Thread thread, CultureInfo culture, CultureInfo uiCulture)
		{
			if(thread == null)
				throw new ArgumentNullException("thread");

			if(culture == null)
				throw new ArgumentNullException("culture");

			if(uiCulture == null)
				throw new ArgumentNullException("uiCulture");

			thread.CurrentCulture = culture;
			thread.CurrentUICulture = uiCulture;
		}

		#endregion
	}
}
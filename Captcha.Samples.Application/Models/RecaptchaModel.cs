using System;
using System.Collections.Specialized;
using System.Globalization;
using Captcha.Samples.Application.Business.Recaptcha;
using Captcha.Samples.Shared.Web;

namespace Captcha.Samples.Application.Models
{
	public class RecaptchaModel
	{
		#region Fields

		public const Theme DefaultTheme = Theme.Red;
		public const string PrivateKeyName = "RecaptchaPrivateKey";
		public const string PublicKeyName = "RecaptchaPublicKey";
		private readonly CultureInfo _culture;
		private readonly string _privateKey;
		private readonly string _publicKey;
		private Theme? _theme;

		#endregion

		#region Constructors

		public RecaptchaModel(ICultureContext cultureContext, NameValueCollection applicationSettings)
		{
			if(cultureContext == null)
				throw new ArgumentNullException("cultureContext");

			if(applicationSettings == null)
				throw new ArgumentNullException("applicationSettings");

			this._culture = cultureContext.PreferredCulture;
			this._privateKey = applicationSettings[PrivateKeyName];
			this._publicKey = applicationSettings[PublicKeyName];
		}

		#endregion

		#region Properties

		public virtual CultureInfo Culture
		{
			get { return this._culture; }
		}

		public virtual string PrivateKey
		{
			get { return this._privateKey; }
		}

		public virtual string PublicKey
		{
			get { return this._publicKey; }
		}

		public virtual Theme Theme
		{
			get
			{
				if(this._theme == null)
					this._theme = DefaultTheme;

				return this._theme.Value;
			}
			set { this._theme = value; }
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Captcha.Samples.Application.Business.Recaptcha;
using Captcha.Samples.Shared;
using Captcha.Samples.Shared.Validation;
using IRecaptchaValidator = Captcha.Samples.Application.Business.Recaptcha.Validation.IValidator;

namespace Captcha.Samples.Application.Models
{
	public class RecaptchaSampleModel
	{
		#region Fields

		private const string _challengeParameterName = "recaptcha_challenge_field";
		private readonly HttpContextBase _httpContext;
		private readonly string _privateKey;
		private const string _responseParameterName = "recaptcha_response_field";
		private static readonly IEnumerable<string> _themes = Enum.GetNames(typeof(Theme)).Select(theme => theme.ToLowerInvariant());
		private LazyInitialization<IValidationResult> _validationResult;
		private readonly IRecaptchaValidator _validator;

		#endregion

		#region Constructors

		public RecaptchaSampleModel(HttpContextBase httpContext, IRecaptchaValidator validator, NameValueCollection applicationSettings)
		{
			if(httpContext == null)
				throw new ArgumentNullException("httpContext");

			if(validator == null)
				throw new ArgumentNullException("validator");

			if(applicationSettings == null)
				throw new ArgumentNullException("applicationSettings");

			this._httpContext = httpContext;
			this._privateKey = applicationSettings[RecaptchaModel.PrivateKeyName];
			this._validator = validator;
		}

		#endregion

		#region Properties

		public virtual bool Confirm
		{
			get { return this.ValidationResult != null && this.ValidationResult.IsValid; }
		}

		protected internal virtual HttpContextBase HttpContext
		{
			get { return this._httpContext; }
		}

		protected internal virtual string PrivateKey
		{
			get { return this._privateKey; }
		}

		public virtual IEnumerable<string> Themes
		{
			get { return _themes; }
		}

		public virtual bool Validate { get; set; }

		public virtual IEnumerable<string> ValidationMessages
		{
			get
			{
				if(this.ValidationResult == null || this.ValidationResult.IsValid)
					return null;

				return this.ValidationResult.Exceptions.Select(exception => exception.Message);
			}
		}

		public virtual IValidationResult ValidationResult
		{
			get
			{
				if(this._validationResult == null)
				{
					this._validationResult = new LazyInitialization<IValidationResult>();

					if(this.Validate)
					{
						NameValueCollection form = this.HttpContext.Request != null && this.HttpContext.Request.Form != null ? this.HttpContext.Request.Form : new NameValueCollection();
						string remoteIp = this.HttpContext.Request != null ? this.HttpContext.Request.UserHostAddress : string.Empty;
						this._validationResult.Value = this.Validator.Validate(form[_challengeParameterName], form[_responseParameterName], remoteIp, this.PrivateKey);
					}
				}

				return this._validationResult.Value;
			}
		}

		protected internal virtual IRecaptchaValidator Validator
		{
			get { return this._validator; }
		}

		#endregion
	}
}
using System;
using System.Net;
using Captcha.Samples.Shared.Validation;
using Recaptcha;

namespace Captcha.Samples.Application.Business.Recaptcha.Validation
{
	public class Validator : IValidator
	{
		#region Methods

		public virtual IValidationResult Validate(string challenge, string response, string remoteIp, string privateKey)
		{
			return this.Validate(challenge, response, remoteIp, privateKey, null);
		}

		public virtual IValidationResult Validate(string challenge, string response, string remoteIp, string privateKey, IWebProxy proxy)
		{
			ValidationResult validationResult = new ValidationResult();

			try
			{
				RecaptchaValidator recaptchaValidator = new RecaptchaValidator
					{
						Challenge = challenge,
						PrivateKey = privateKey,
						Proxy = proxy,
						RemoteIP = remoteIp,
						Response = response
					};

				RecaptchaResponse recaptchaResponse = recaptchaValidator.Validate();

				if(!recaptchaResponse.IsValid)
					validationResult.Exceptions.Add(new InvalidOperationException(recaptchaResponse.ErrorMessage));
			}
			catch(Exception exception)
			{
				validationResult.Exceptions.Add(exception);
			}

			return validationResult;
		}

		#endregion
	}
}
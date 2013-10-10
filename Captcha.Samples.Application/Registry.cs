using System.Collections.Specialized;
using System.Configuration;
using IRecaptchaValidator = Captcha.Samples.Application.Business.Recaptcha.Validation.IValidator;
using RecaptchaValidator = Captcha.Samples.Application.Business.Recaptcha.Validation.Validator;

namespace Captcha.Samples.Application
{
	public class Registry : Captcha.Samples.Shared.Registry
	{
		#region Constructors

		public Registry()
		{
			this.For<IRecaptchaValidator>().Singleton().Use<RecaptchaValidator>();
			this.For<NameValueCollection>().Singleton().Use(ConfigurationManager.AppSettings);
		}

		#endregion
	}
}
using System.Net;
using Captcha.Samples.Shared.Validation;

namespace Captcha.Samples.Application.Business.Recaptcha.Validation
{
	public interface IValidator
	{
		#region Methods

		IValidationResult Validate(string challenge, string response, string remoteIp, string privateKey);
		IValidationResult Validate(string challenge, string response, string remoteIp, string privateKey, IWebProxy proxy);

		#endregion
	}
}
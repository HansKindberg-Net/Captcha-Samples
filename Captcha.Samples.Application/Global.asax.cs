using System;
using Captcha.Samples.Shared;

namespace Captcha.Samples.Application
{
	public class Global : System.Web.HttpApplication
	{
		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			Bootstrapper.Bootstrap();
		}

		#endregion
	}
}
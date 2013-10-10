using System;

namespace Captcha.Samples.Shared.IoC
{
	public interface IServiceLocator : IServiceProvider
	{
		#region Methods

		T GetService<T>();

		#endregion
	}
}
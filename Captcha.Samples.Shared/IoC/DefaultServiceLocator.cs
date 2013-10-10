using System;

namespace Captcha.Samples.Shared.IoC
{
	public class DefaultServiceLocator : IServiceLocator
	{
		#region Methods

		public virtual object GetService(Type serviceType)
		{
			if(serviceType == null)
				throw new ArgumentNullException("serviceType");

			if(serviceType.IsInterface || serviceType.IsAbstract)
				return null;

			try
			{
				return Activator.CreateInstance(serviceType);
			}
			catch
			{
				return null;
			}
		}

		public virtual T GetService<T>()
		{
			return (T) this.GetService(typeof(T));
		}

		#endregion
	}
}
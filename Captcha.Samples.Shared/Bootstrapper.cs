using System;
using Captcha.Samples.Shared.IoC;
using Captcha.Samples.Shared.IoC.StructureMap;
using HansKindberg.Web.Mvp.IoC.StructureMap.Binder;
using StructureMap;
using WebFormsMvp.Binder;

namespace Captcha.Samples.Shared
{
	public class Bootstrapper : IBootstrapper
	{
		#region Fields

		private static Boolean _hasStarted;

		#endregion

		#region Methods

		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();
			PresenterBinder.Factory = new PresenterFactory(ObjectFactory.Container);
			ServiceLocator.Instance = new StructureMapServiceLocator(ObjectFactory.Container);
		}

		public void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(initializer => { initializer.PullConfigurationFromAppConfig = true; });
		}

		public static void Restart()
		{
			if(_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		#endregion
	}
}
using System;
using StructureMap;

namespace Captcha.Samples.Shared.IoC.StructureMap
{
	public class StructureMapServiceLocator : IServiceLocator
	{
		#region Fields

		private readonly IContainer _container;

		#endregion

		#region Constructors

		public StructureMapServiceLocator(IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");

			this._container = container;
		}

		#endregion

		#region Properties

		protected internal virtual IContainer Container
		{
			get { return this._container; }
		}

		#endregion

		#region Methods

		protected internal virtual object GetConcreteService(Type serviceType)
		{
			try
			{
				// Can't use TryGetInstance here because it won’t create concrete types
				return this.Container.GetInstance(serviceType);
			}
			catch(StructureMapException)
			{
				return null;
			}
		}

		protected internal virtual object GetInterfaceService(Type serviceType)
		{
			return this.Container.TryGetInstance(serviceType);
		}

		public virtual object GetService(Type serviceType)
		{
			if(serviceType == null)
				throw new ArgumentNullException("serviceType");

			if(serviceType.IsInterface || serviceType.IsAbstract)
				return this.GetInterfaceService(serviceType);

			return this.GetConcreteService(serviceType);
		}

		public virtual T GetService<T>()
		{
			return (T) this.GetService(typeof(T));
		}

		#endregion
	}
}
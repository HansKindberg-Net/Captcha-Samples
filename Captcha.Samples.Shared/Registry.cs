using System.Web;
using System.Web.SessionState;
using Captcha.Samples.Shared.IoC;
using Captcha.Samples.Shared.IoC.StructureMap;
using Captcha.Samples.Shared.Mvp.Models;
using Captcha.Samples.Shared.Web;

namespace Captcha.Samples.Shared
{
	public abstract class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			this.For<HttpContext>().Use(() => HttpContext.Current);
			this.For<HttpContextBase>().HybridHttpOrThreadLocalScoped().Use<HttpContextWrapper>();
			this.For<HttpRequest>().Use(() => HttpContext.Current.Request);
			this.For<HttpRequestBase>().HybridHttpOrThreadLocalScoped().Use<HttpRequestWrapper>();
			this.For<HttpResponse>().Use(() => HttpContext.Current.Response);
			this.For<HttpResponseBase>().HybridHttpOrThreadLocalScoped().Use<HttpResponseWrapper>();
			this.For<HttpSessionState>().Use(() => HttpContext.Current.Session);
			this.For<HttpSessionStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpSessionStateWrapper>();
			this.For<ICultureContext>().HybridHttpOrThreadLocalScoped().Use<CultureContext>();
			this.For<IModelFactory>().Singleton().Use<ModelFactory>();
			this.For<IServiceLocator>().Singleton().Use<StructureMapServiceLocator>();
		}

		#endregion
	}
}
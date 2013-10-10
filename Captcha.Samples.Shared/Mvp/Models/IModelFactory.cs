using StructureMap.Pipeline;

namespace Captcha.Samples.Shared.Mvp.Models
{
	public interface IModelFactory
	{
		#region Methods

		TModel Create<TModel>();
		TModel Create<TModel>(ExplicitArguments explicitArguments);

		#endregion
	}
}
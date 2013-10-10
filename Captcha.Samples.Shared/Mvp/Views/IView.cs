namespace Captcha.Samples.Shared.Mvp.Views
{
	public interface IView : WebFormsMvp.IView
	{
		//CultureInfo Culture { get; }
		//CultureInfo UICulture { get; }
	}

	public interface IView<TModel> : IView
	{
		#region Properties

		TModel Model { get; set; }

		#endregion
	}
}
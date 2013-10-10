using Captcha.Samples.Application.Models;
using Captcha.Samples.Application.Presenters;
using Captcha.Samples.Application.Views.Home;
using Captcha.Samples.Shared.Web.Mvp.UI.Views;
using WebFormsMvp;

namespace Captcha.Samples.Application
{
	[PresenterBinding(typeof(HomePresenter))]
	public partial class Index : Page<HomeModel>, IHomeView {}
}
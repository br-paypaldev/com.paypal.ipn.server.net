namespace Controllers {
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Text;
	using System.Net;
	using com.paypal.ipn;
	using com.paypal.ipn.google.ac2dm;
	using com.paypal.ipn.sample;
	
	[HandleError]
	public class HomeController : Controller {
		[AcceptVerbs(HttpVerbs.Post)]
		public void Index( FormCollection post ) {
			InstantPaymentNotification ipn = new InstantPaymentNotification(true);
			ipn.setIPNHandler( new IPNToAC2DMHandler( new SampleModel() ) );
			ipn.listem(post);
		}
	}
}
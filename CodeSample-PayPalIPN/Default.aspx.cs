namespace CodeSamplePayPalIPN {
	using System;
	using System.Web;
	using System.Web.UI;
	using CodeSamplePayPalIPN.IPN;
	
	public partial class Default : System.Web.UI.Page {
		protected virtual void Page_LoadComplete(object sender, EventArgs e) {
			if (Request.HttpMethod == "POST") {
				InstantPaymentNotification ipn = new InstantPaymentNotification();
				ipn.setIPNHandler(new SampleIPNHandler());
				ipn.listem(Request.Form);
			}
		}
	}
}
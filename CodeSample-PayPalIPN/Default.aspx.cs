using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeSample_PayPalIPN.Models.ipn;
using CodeSample_PayPalIPN.Models;

namespace CodeSample_PayPalIPN
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                InstantPaymentNotification ipn = new InstantPaymentNotification();
                ipn.setIPNHandler(new SampleIPNHandler());
                ipn.listem(Request.Form);
            }
        }
    }
}

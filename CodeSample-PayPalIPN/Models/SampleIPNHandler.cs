using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using CodeSample_PayPalIPN.Models.ipn;

namespace CodeSample_PayPalIPN.Models
{
    public class SampleIPNHandler : IPNHandler
    {
        public SampleIPNHandler() { }

        public void handle(bool isVerified, NameValueCollection message)
        {
        }
    }
}

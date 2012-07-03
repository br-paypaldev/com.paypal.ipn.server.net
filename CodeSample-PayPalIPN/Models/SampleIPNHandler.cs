namespace CodeSamplePayPalIPN {
	using System;
	using System.Collections.Specialized;
	using CodeSamplePayPalIPN.IPN;
	
	public class SampleIPNHandler :IPNHandler {
		public SampleIPNHandler() {}
		
		public void handle(bool isVerified, NameValueCollection message) {
		}
	}
}


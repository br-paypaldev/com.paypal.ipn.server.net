namespace com.paypal.ipn.sample {
	using System;
	using com.paypal.ipn.google.auth;
	
	public class SampleModel {
		public SampleModel () {
		}
		
		public string getAuth() {
			String user = "usuario@gmail.com";
			String pswd = "senha";
			String type = "GOOGLE";
			
			//TODO: verificar se existe uma autorização e definir regras de negócio
			//para atualizá-la.
			
			ClientLogin cl = new ClientLogin();
			
			return cl.getAuth(type, user, pswd, "PayPalX-com.paypal.ipn-1.0",
			                  "ac2dm");
		}
		
		public string getRegistrationId( string email ) {
			//TODO: adicionar obtenção do id de registro

			return null;
		}
	}
}
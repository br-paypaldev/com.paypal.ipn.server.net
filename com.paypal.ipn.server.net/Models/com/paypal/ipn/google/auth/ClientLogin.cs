namespace com.paypal.ipn.google.auth {
	using System;
	using System.IO;
	using System.Text;
	using System.Net;

	/// <summary>
	/// Faz a requisição POST ao ClientLogin do Google para obter a autorização de
	/// acesso para contas Google.
	/// 
	/// Author: João Batista Neto
	/// </summary>
	public class ClientLogin {
		/// <summary>
		/// URL do serviço de autorização ClientLogin do Google .
		/// </summary>
		const string URL = "https://www.google.com/accounts/ClientLogin";
		
		/// <summary>
		/// Token de autorização.
		/// </summary>
		private string Auth;
		
		/// <summary>
		/// Obtém o token de autorização do Google utilizando ClientLogin.
		/// </summary>
		/// <param name="accountType">
		/// <see cref="System.String"/> Tipo da conta que está solicitando a
		/// autorização, os valores possíveis são:
		/// <ul>
		/// <li>GOOGLE</li>
		/// <li>HOSTED</li>
		/// <li>HOSTED_OR_GOOGLE</li>
		/// </ul>
		/// </param>
		/// <param name="Email">
		/// <see cref="System.String"/> Email completo do usuário, incluindo o domínio.
		/// </param>
		/// <param name="Passwd">
		/// <see cref="System.String"/> Senha do usuário.
		/// </param>
		/// <param name="source">
		/// Uma <see cref="System.String"/> identificando a aplicação.
		/// </param>
		/// <param name="service">
		/// <see cref="System.String"/> Nome do serviço que será solicitada a autorização.
		/// </param>
		/// <returns>
		/// <see cref="System.String"/> O Token de autorização.
		/// </returns>
		public string getAuth(string accountType,
		                      string Email,
		                      string Passwd,
		                      string source,
		                      string service) {

			if ( Auth == null ) {
				HttpWebRequest request = (HttpWebRequest) WebRequest.Create( URL );
				StringBuilder sb = new StringBuilder();

				sb.Append( "accountType=" + accountType );
				sb.Append( "&Email=" + Email );
				sb.Append( "&Passwd=" + Passwd );
				sb.Append( "&source=" + source );
				sb.Append( "&service=" + service );

				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";				

				using ( Stream stream = request.GetRequestStream() ) {
					UTF8Encoding encoding = new UTF8Encoding();
					byte[] bytes = encoding.GetBytes( sb.ToString() );
					
					stream.Write( bytes , 0 , bytes.Length );
				}

				HttpWebResponse response = (HttpWebResponse) request.GetResponse();

				using ( Stream stream = response.GetResponseStream() ) {
					string data;

					using ( StreamReader reader = new StreamReader( stream , Encoding.UTF8 ) ) {
						while ( (data = reader.ReadLine()) != null ) {
							string[] nv = data.Split( '=' );
							
							if ( nv.Length == 2 && nv[0].Equals( "Auth" ) ) {
								Auth = nv[1];
								break;
							}
						}
						
						reader.Close();
					}
				}
			}
			
			return Auth;
		}
	}
}
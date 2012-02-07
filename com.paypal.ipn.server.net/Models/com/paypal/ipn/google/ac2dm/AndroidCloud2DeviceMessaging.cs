namespace com.paypal.ipn.google.ac2dm {
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.IO;
	using System.Net;
	using System.Net.Security;
	using System.Security.Cryptography.X509Certificates;
	using System.Text;
	using System.Web;

	/// <summary>
	/// A classe AndroidCloud2DeviceMessaging faz integração com o serviço Android
	/// Cloud to Device Messaging (C2DM) para enviar dados para dispositivos Android.
	/// 
	/// Author: João Batista Neto
	/// </summary>
	public class AndroidCloud2DeviceMessaging {
		/// <summary>
		/// URL do serviço C2DM.
		/// </summary>
		public const String URL = "https://android.apis.google.com/c2dm/send";
		
		/// <summary>
		/// Conjunto de pares key=value que serão enviados para o dispositivo.
		/// </summary>
		private Dictionary<string,NameValueCollection> data;
		
		public AndroidCloud2DeviceMessaging () {
			data = new Dictionary<string, NameValueCollection>();
		}
		
		/// <summary>
		/// Adiciona um par key=value que será enviado para o dispositivo android.
		/// </summary>
		/// <param name="key">
		/// <see cref="System.String"/> A chave que será enviada para o dispositivo.
		/// </param>
		/// <param name="value">
		/// <see cref="System.String"/> O valor da chave.
		/// </param>
		/// <param name="collapseKey">
		/// <see cref="System.String"/> Chave de agrupamento que será utilizado pelo
		/// Google para evitar que várias mensagens do mesmo tipo sejam
		/// enviadas para o usuário de uma vez quando o dispositivo fique
		/// online.
		/// </param>
		public void addData(string key,string value,string collapseKey) {
			NameValueCollection nv;
			
			if ( !data.ContainsKey(collapseKey) ) {
				nv = new NameValueCollection();
				
				data.Add( collapseKey , nv );
			} else {
				nv = data[collapseKey];
			}
			
			nv.Set(key,value);
		}
		
		/// <summary>
		/// Remove todas os pares key=value.
		/// </summary>
		public void clear() {
			data.Clear();
		}
		
		/// <summary>
		/// Envia a mensagem para o servidor C2DM.
		/// </summary>
		/// <param name="registrationId">
		/// <see cref="System.String"/> ID de registro do dispositivo android.
		/// </param>
		/// <param name="auth">
		/// <see cref="System.String"/> Token de autorização.
		/// </param>
		/// <seealso cref="com.paypal.ipn.google.auth.ClientLogin#getAuth"/>
		public void send( string registrationId,string auth ) {
			ServicePointManager.ServerCertificateValidationCallback = Validator;
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create( URL );
			
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add( "Authorization" , "GoogleLogin auth=" + auth );
			
			foreach ( string collapseKey in data.Keys ) {
				StringBuilder sb = new StringBuilder();
				NameValueCollection nv = data[collapseKey];
				
				sb.Append("registration_id=" + registrationId);
				sb.Append("&collapse_key=" + collapseKey);
				
				foreach ( string key in nv.AllKeys ) {
					sb.Append("&data." + key );
					sb.Append("=");
					sb.Append(HttpUtility.UrlEncode(nv[key]));
				}
				
				using ( Stream stream = request.GetRequestStream() ) {
					UTF8Encoding encoding = new UTF8Encoding();
					byte[] bytes = encoding.GetBytes( sb.ToString() );
					
					stream.Write( bytes , 0 , bytes.Length );
				}
			}
		}
		
		/// <summary>
		/// O certificado do Google não cobre android.apis.google.com, então será
		/// preciso verificar e fazer a validação do certificado manualmente.
		/// </summary>
		/// <param name="sender">
		/// <see cref="System.Object"/>
		/// </param>
		/// <param name="certificate">
		/// <see cref="X509Certificate"/>
		/// </param>
		/// <param name="chain">
		/// <see cref="X509Chain"/>
		/// </param>
		/// <param name="sslPolicyErrors">
		/// <see cref="SslPolicyErrors"/>
		/// </param>
		/// <returns>
		/// <see cref="System.Boolean"/> Retornamos
		/// </returns>
		bool Validator(
			object sender,
			X509Certificate certificate,
			X509Chain chain,
			SslPolicyErrors sslPolicyErrors ) {
			return true;
		}
	}
}
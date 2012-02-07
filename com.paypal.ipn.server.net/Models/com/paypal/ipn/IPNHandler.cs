namespace com.paypal.ipn {
	using System;
	using System.Web.Mvc;
	
	/// <summary>
	/// Interface para definição de um manipulador de notificação
	/// de pagamento instantânea.
	/// 
	/// Author: João Batista Neto
	/// </summary>
	public interface IPNHandler {
		/// <summary>
		/// Manipula uma notificação de pagamento instantânea recebida pelo PayPal.
		/// </summary>
		/// <param name="isVerified">
		/// <see cref="System.Boolean"/> isVerified Identifica que a mensagem foi
		/// verificada como tendo sido enviada pelo PayPal.
		/// </param>
		/// <param name="form">
		/// <see cref="FormCollection"/> Mensagem completa enviada pelo PayPal.
		/// </param>
		void handle( bool isVerified , FormCollection message );
	}
}
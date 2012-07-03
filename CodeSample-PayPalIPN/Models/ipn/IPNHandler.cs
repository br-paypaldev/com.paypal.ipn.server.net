namespace CodeSamplePayPalIPN.IPN {
	using System;
	using System.Collections.Specialized;

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
		/// <see cref="NameValueCollection"/> Mensagem completa enviada pelo PayPal.
		/// </param>
		void handle(bool isVerified, NameValueCollection message);
	}
}
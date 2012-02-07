namespace com.paypal.ipn.sample {
	using System;
	using System.Web.Mvc;
	using com.paypal.ipn;
	using com.paypal.ipn.google.ac2dm;

	/// <summary>
	/// Manipulador de notificação instantânea de pagamento que envia a
	/// mensagem para dispositivos Android.
	/// </summary>
	public class IPNToAC2DMHandler :IPNHandler {
		private SampleModel model;
		
		public IPNToAC2DMHandler ( SampleModel model ) {
			this.model = model;
		}
		
		/// <summary>
		/// Envia as notificações instantânea de pagamento para dispositivos
		/// Android caso a mensagem tenha sido verificada pelo PayPal.
		/// </summary>
		/// <param name="isVerified">
		/// <see cref="System.Boolean"/> TRUE caso o PayPal tenha verificado
		/// a mensagem.
		/// </param>
		/// <param name="message">
		/// <see cref="FormCollection"/> A mensagem enviada pelo PayPal
		/// </param>
		public void handle( bool isVerified , FormCollection message ) {
			if ( isVerified ) {
				AndroidCloud2DeviceMessaging ac2dm = new AndroidCloud2DeviceMessaging();
				
				foreach ( string field in message ) {
					ac2dm.addData( field , message[field] , "ipn" );
				}
				
				ac2dm.send(model.getRegistrationId(message["receiver_email"]),
				           model.getAuth() );
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace CodeSample_PayPalIPN.Models.ipn
{
    /// <summary>
    /// Observador de Notificações de Pagamento Instantâneo.
    ///
    /// Author: João Batista Neto
    /// </summary>
    public class InstantPaymentNotification
    {
        /// <summary>
        /// Endpoint de produção.
        /// </summary>
        public const string HOST = "https://www.paypal.com";

        /// <summary>
        /// Endpoint do Sandbox
        /// </summary>
        public const string SANDBOX_HOST = "https://www.sandbox.paypal.com";

        /// <summary>
        /// Endpoint que será utilizado na verificação
        /// </summary>
        private string endpoint = InstantPaymentNotification.HOST;

        /// <summary>
        /// Manipulador de notificação instantânea de pagamento.
        /// </summary>
        private IPNHandler handler;

        public InstantPaymentNotification()
            : this(false)
        {
        }

        /// <summary>
        /// Constroi o observador no notificação instantânea de pagamento informando
        /// o ambiente que será utilizado para validação.
        /// </summary>
        /// <param name="sandbox">
        /// <see cref="System.Boolean"/> Define se será utilizado o ambiente de
        /// produção ou o Sandbox.
        /// </param>
        public InstantPaymentNotification(bool sandbox)
        {
            if (sandbox)
            {
                endpoint = InstantPaymentNotification.SANDBOX_HOST;
            }

            endpoint += "/cgi-bin/webscr?cmd=_notify-validate";
        }

        /// <summary>
        /// Aguarda por notificações de pagamento instantânea; Caso uma nova
        /// notificação seja recebida, faz a verificação e notifica um manipulador
        /// com o status (verificada ou não) e a mensagem recebida.
        /// </summary>
        /// <param name="post">
        /// A <see cref="NameValueCollection"/> com os dados postados pelo PayPal
        /// </param>
        /// <seealso cref="InstantPaymentNotification#setIPNHandler()"/>
        public void listem(NameValueCollection post)
        {
            if (handler != null && post["receiver_email"] != null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                StringBuilder sb = new StringBuilder();

                foreach (string field in post)
                {
                    if (sb.Length != 0)
                    {
                        sb.Append("&");
                    }

                    sb.Append(field);
                    sb.Append("=");
                    sb.Append(post[field]);
                }

                using (Stream stream = request.GetRequestStream())
                {
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] bytes = encoding.GetBytes(sb.ToString());

                    stream.Write(bytes, 0, bytes.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string data = reader.ReadToEnd();

                        reader.Close();

                        handler.handle(data.Equals("VERIFIED"), post);
                    }
                }
            }
        }

        /// <summary>
        /// Define o objeto que irá manipular as notificações de pagamento
        /// instantâneas enviadas pelo PayPal.
        /// </summary>
        /// <param name="handler">
        /// <see cref="IPNHandler"/>
        /// </param>
        public void setIPNHandler(IPNHandler handler)
        {
            this.handler = handler;
        }
    }
}


 


using ProjectMVC.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Projecy_MVC.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com" ,587);
			client.EnableSsl = false;
			client.Credentials = new NetworkCredential("abdelhmedm0000@gmail.com" ,"00000000");
			client.Send("abdelhmedm2001@gmail.com" , email.Reciepints, email.Subject ,email.Body);

		}

	}
}

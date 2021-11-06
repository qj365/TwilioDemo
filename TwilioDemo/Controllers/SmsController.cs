using System;
using System.Linq;
using System.Web.Mvc;
using Twilio;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;
using TwilioDemo.Twilio;
namespace TwilioDemo.Controllers
{
    public class SmsController : TwilioController
    {
        // GET: Sms
        public ActionResult SendSms()
        {

            // Initialize the Twilio client
            TwilioClient.Init(Keys.accountSid, Keys.authToken);

            var mediaUrl = new[] {
                new Uri("https://c1.staticflickr.com/3/2899/14341091933_1e92e62d12_b.jpg")
            }.ToList();

            // Send a new outgoing SMS by POSTing to the Messages resource
            var message = MessageResource.Create(
                from: new PhoneNumber(Keys.from),
                to: new PhoneNumber(Keys.to),
                //mediaUrl: mediaUrl,
                body: "Lập trình tích hợp gửi tin nhắn ");

            return Content(message.Sid);

        }

        [HttpPost]
        public ActionResult ReceiveSms()
        {
            var response = new MessagingResponse();
            response.Message("Lập trình tích hợp nhận tin nhắn");
            return TwiML(response);
        }
    }
}
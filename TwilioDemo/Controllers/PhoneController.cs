using System;
using System.Web.Mvc;
using Twilio;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;
using TwilioDemo.Twilio;

namespace TwilioDemo.Controllers
{
    public class PhoneController : TwilioController
    {
        // GET: Phone
        public ActionResult MakeCall()
        {
            // Initialize the Twilio client
            TwilioClient.Init(Keys.accountSid, Keys.authToken);

            // Send a new outgoing SMS by POSTing to the Messages resource
            var call = CallResource.Create(
                from: new PhoneNumber(Keys.from), // From number, must be an SMS-enabled Twilio number
                to: new PhoneNumber(Keys.to), // To number, if using Sandbox see note above
                url: new Uri("http://demo.twilio.com/docs/voice.xml"));
                //twiml: new Twiml("<Response><Say voice=\"alice\">My personalized message</Say></Response>"));
            return Content(call.Sid);

        }

        [HttpPost]
        public ActionResult ReceiveCall()
        {
            var response = new VoiceResponse();
            //response.Say("hello world!");
            response.Play(new Uri("https://api.twilio.com/cowbell.mp3"));
            return TwiML(response);
        }
    }
}
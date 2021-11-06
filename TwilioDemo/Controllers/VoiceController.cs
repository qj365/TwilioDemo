// In Package Manager, run:
// Install-Package Twilio.AspNet.Mvc -DependencyVersion HighestMinor

using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.AspNet.Common;
using Twilio.TwiML;
using System;

public class VoiceController : TwilioController
{
	[HttpPost]
	public TwiMLResult Index()
	{
		var response = new VoiceResponse();
		response.Gather(numDigits: 1, action: new Uri("/voice/gather", UriKind.Relative))
				.Say("For sales, press 1. For support, press 2. For music, press 3. ");

		// If the user doesn't enter input, loop
		response.Redirect(new Uri("/voice", UriKind.Relative));

		return TwiML(response);
	}


	[HttpPost]
	public TwiMLResult Gather(VoiceRequest request)
	{
		var response = new VoiceResponse();

		// If the user entered digits, process their request
		if (!string.IsNullOrEmpty(request.Digits))
		{
			switch (request.Digits)
			{
				case "1":
					response.Say("You selected sales. Good for you!");
					break;
				case "2":
					response.Say("You need support. We will help!");
					break;
				case "3":
					response.Play(new Uri("https://vnno-vn-5-tf-mp3-s1-zmp3.zadn.vn/a6d5b9401f07f659af16/11416612340909235?authen=exp=1636258427~acl=/a6d5b9401f07f659af16/*~hmac=4c49fce722c751bed1eea8ba292ff756&fs=MTYzNjA4NTYyNzA2M3x3ZWJWNnwwfDQyLjExOS4xNTUdUngMTI1"));
					break;
				case "4":
					response.Dial("+84844663836");
					response.Say("Goodbye");
					break;
				default:
					response.Say("Sorry, I don't understand that choice.").Pause();
					response.Redirect(new Uri("/voice", UriKind.Relative));
					break;
			}
		}
		else
		{
			// If no input was sent, redirect to the /voice route
			response.Redirect(new Uri("/voice", UriKind.Relative));
		}

		return TwiML(response);
	}
}

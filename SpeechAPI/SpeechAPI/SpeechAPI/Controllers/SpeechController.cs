using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SpeechAPI.Models;

namespace SpeechAPI.Controllers
{
    public class SpeechController : ApiController
    {
        private static Speech speech = null;

        public SpeechController()
        {
            if (speech==null)
                speech = new Speech();
        }
        public string Get()
        {
            System.Diagnostics.Debug.WriteLine("controller called");
            string value = "";
            if (speech.commands.Count>0)
                value = speech.commands.Dequeue();
            return value;
        }
    }
}

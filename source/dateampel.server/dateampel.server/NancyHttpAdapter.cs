﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace dateampel.server
{
    public class NancyHttpAdapter : NancyModule
    {
        public NancyHttpAdapter()
        {
            Get["/"] = _ => View["ampel_eckdaten_erfassen"];
            Post["/ampel_anlegen"] = _ =>
            {
                var viewModel = new
                {
                    Titel = Request.Form.DateName + " trifft " + Request.Form.InitiatorName,
                    InitiatorEmail = Request.Form.InitiatorEmail,
                    DateEmail = Request.Form.DateEmail
                };
                return View["ampel_quittung", viewModel];
            };

            Get["/ampel/{PartnerId}"] = _ =>
            {
                var viewModel = new
                {
                    AmpelId = _.PartnerId,
                    InitiatorName = "Ralf",
                    DateName = "Antje"
                };
                return View["ampel_dashboard", viewModel];
            };
        }
    }
}
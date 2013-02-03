using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Conventions;

namespace dateampel.server
{
    public class NancyHttpAdapter : NancyModule
    {
        public NancyHttpAdapter()
        {
            Get["/"] = _ => View[@"pages/home"];

            Post["/vote"] = _ =>
            {
                var outputModel = new
                {
                    AmpelCode = "1234",
                    Request.Form.Name,
                    Request.Form.Email
                };
                return View[@"pages/vote", outputModel];
            };

            Post["/voted"] = _ =>
            {
                var outputModel = new
                {
                    Request.Form.AmpelCode,

                    NameOwner = "Ralf",
                    VoteOwner = Request.Form.Vote + ".png",

                    NameDate = "Antje",
                    VoteDate = "grau.png"
                };
                return View[@"pages/result", outputModel];
            };

            Get["/result/{AmpelCode}"] = _ =>
            {
                var outputModel = new
                {
                    _.AmpelCode,

                    NameOwner = "Ralf",
                    VoteOwner = "gelb.png",

                    NameDate = "Antje",
                    VoteDate = "rot.png"
                };
                return View[@"pages/result", outputModel];
            };
        }
    }

    public class ApplicationBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("images", @"images"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
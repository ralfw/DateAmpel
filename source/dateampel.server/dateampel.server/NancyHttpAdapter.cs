using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Nancy;
using Nancy.Conventions;
using Nancy.Responses.Negotiation;
using Parse;
using dateampel.adapter;
using dateampel.adapter.ampelspeicher;
using dateampel.adapter.ampelspeicher.daten;
using dateampel.logik;

namespace dateampel.server
{
    public class NancyHttpAdapter : NancyModule
    {
        public NancyHttpAdapter()
        {
            var safe = new Geheimfach(Path.Combine(Get_bin_path(), "geheim.txt"));
            var parse = new ParseAdapter(safe["parseAppId"], safe["parseWindowsKey"]);
            var interactions = new Integration(parse);

            Get["/parsetest"] = _ =>
                {
                    ParseClient.Initialize(safe["parseAppId"], safe["parseWindowsKey"]);
                    var po = new ParseObject("test");
                    po["name"] = "maria, " + DateTime.Now.ToString();
                    Debug.Print("before save");
                    po.SaveAsync().Wait();
                    Debug.Print("after wait");

                    return "erfolg! " + po.ObjectId;
                };


            Get["/"] = _ => View[@"pages/home"];

            Post["/vote"] = _ =>
                {
                    Negotiator view = null;

                    interactions.Ampel_einrichten((object) Request.Form,
                                                  ampel =>
                                                      {
                                                          var voteOM = Map.Ampel_nach_Vote(ampel);
                                                          view = View[@"pages/vote", voteOM];
                                                      });

                    return view;
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
                //return View[@"pages/result", outputModel];
                return Response.AsRedirect("/result/" + (string)Request.Form.AmpelCode);
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

        private string Get_bin_path()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
        }
    }

    internal class Map
    {
        public static dynamic Ampel_nach_Vote(Ampel ampel)
        {
            dynamic om = new ExpandoObject();
                om.AmpelCode = ampel.Code;
                om.Email = ampel.Owner.Email;
                om.Name = ampel.Owner.Name;
            return om;
        }    
    }

    //public class ApplicationBootstrapper : DefaultNancyBootstrapper
    //{
    //    protected override void ConfigureConventions(NancyConventions nancyConventions)
    //    {
    //        nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("images", @"images"));
    //        base.ConfigureConventions(nancyConventions);
    //    }
    //}
}
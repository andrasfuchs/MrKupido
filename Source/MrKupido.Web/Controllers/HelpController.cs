﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MrKupido.Web.Controllers
{
    public class HelpController : BaseController
    {
        //
        // GET: /Help/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WhoIsMrKupido()
        {
            return View();
        }

        public ActionResult FeatureGuideLevel1()
        {
            return View();
        }

        public ActionResult FeatureGuideLevel2()
        {
            return View();
        }

        public ActionResult BetaTesterGuide()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

		public ActionResult TermsOfUse()
		{
			return View();
		}
		
		public ActionResult VersionChangeLog020()
        {
            return View();
        }
    }
}
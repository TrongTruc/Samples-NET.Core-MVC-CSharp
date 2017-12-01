﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using System.Data;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;

namespace Send_Report_by_Email.Controllers
{
    public class ViewerController : Controller
    {
        static ViewerController()
        {
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetReport()
        {
            // Create the report object
            StiReport report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));

            // Load data from XML file for report template
            DataSet data = new DataSet("Demo");
            data.ReadXml(StiNetCoreHelper.MapPath(this, "Reports/Data/Demo.xml"));

            report.RegData(data);
            
            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        public IActionResult EmailReport()
        {
            StiEmailOptions options = StiNetCoreViewer.GetEmailOptions(this);

            options.AddressFrom = "admin@test.com";
            //options.AddressTo = "manager@test.com";
            //options.Subject = "Quarterly Report";
            //options.Body = "Quarterly report on arrival of the goods.";

            options.Host = "smtp.test.com";
            //options.Port = 465;
            options.UserName = "admin@test.com";
            options.Password = "************";

            return StiNetCoreViewer.EmailReportResult(this, options);
        }
    }
}
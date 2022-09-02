using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rdlc
{
    [Route("api/[controller]/[action]")]
    [Microsoft.AspNetCore.Cors.EnableCors("AllowAllOrigins")]
    public class ReportViewerController : Controller, IReportController
    {
        // Report viewer requires a memory cache to store the information of consecutive client request and
        // have the rendered report viewer information in server.
        private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

        // IHostingEnvironment used with sample to get the application data from wwwroot.
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        // Post action to process the report from server based json parameters and send the result back to the client.
        public ReportViewerController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
        }
        
        // Post action to process the report from server based json parameters and send the result back to the client.
        [HttpPost]
        public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
        {
            return ReportHelper.ProcessReport(jsonArray, this, this._cache);
        }

        // Method will be called to initialize the report information to load the report with ReportHelper for processing.
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            string reportName = reportOption.ReportModel.ReportPath;
            string basePath = _hostingEnvironment.WebRootPath;
            reportOption.ReportModel.ProcessingMode = ProcessingMode.Local;
            FileStream reportStream = new FileStream(basePath + @"\Resources\" + reportName, FileMode.Open, FileAccess.Read);
            //FileStream inputStream = new FileStream(basePath + @"\Resources\Report1.rdlc", FileMode.Open, FileAccess.Read);
            reportOption.ReportModel.Stream = reportStream;

            /*
            reportOption.ReportModel.ProcessingMode = ProcessingMode.Local;
            reportOption.ReportModel.ReportPath = HostingEnvironment.MapPath(@"~/Resources/product-list.rdlc");
            reportOption.ReportModel.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = "list", Value = ProductList.GetData() });
            */
            //reportOption.ReportModel.DataSources.Clear();
            //reportOption.ReportModel.DataSources.Add(new BoldReports.Web.ReportDataSource { Name = "DataSet1", Value = ProductList.GetData() });
        }

        
        // Method will be called when reported is loaded with internally to start to layout process with ReportHelper.
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
        }
        
        //Get action for getting resources from the report
        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        // Method will be called from Report Viewer client to get the image src for Image report item.
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, _cache);
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }
    }
}
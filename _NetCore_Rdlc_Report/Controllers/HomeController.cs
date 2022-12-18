using _NetCore_Rdlc_Report.Models;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _NetCore_Rdlc_Report.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webhosting;

        public HomeController(IWebHostEnvironment webhosting)
        {
            _webhosting = webhosting;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            var dt = new DataTable();
            dt = Getemployeelist();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webhosting.WebRootPath}\\Report\\rptEmployee.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("ReportDataSet", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

            return File(result.MainStream, "application/pdf");

        }
        public DataTable Getemployeelist()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("DeptName");
            dt.Columns.Add("Designation");

            DataRow dataRow;
            for(int i=1;i<=120;i++)
            {
                dataRow = dt.NewRow();
                dataRow["EmpId"] = i;
                dataRow["EmpName"] = "Md. Nayem";
                dataRow["DeptName"] = "IT";
                dataRow["Designation"] = "SF";

                dt.Rows.Add(dataRow);
            }
            return dt;

        }


    }
}

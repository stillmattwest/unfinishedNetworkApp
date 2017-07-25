using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetworkBillingSystem_Alpha.DAL;
using NetworkBillingSystem_Alpha.Models;

namespace NetworkBillingSystem_Alpha.Controllers
{
    public class DepartmentReportController : Controller
    {

        // GET: DepartmentReport Index
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Index()
        {

            ViewBag.DepartmentID = new SelectList(db.Departments.OrderBy(x => x.Name), "DepartmentID", "Name");

            return View();
        }

        // GET: Basic Department Report. Name, interfaces, total connected devices, total connections, all reporting devices
        public ActionResult GetBasicDepartmentReport(string id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            var dept = db.Departments.Find(Convert.ToInt32(id));

            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            data.Add("id", dept.DepartmentID.ToString());
            data.Add("name", dept.Name);
            data.Add("billing-code", dept.BillingCode);
            // retrieve the bdiNumbers from associated BDIs
            var bdis = dept.BDIs;
            List<string> bdiList = new List<string>();
            foreach (var item in bdis)
            {
                bdiList.Add(item.BDINumber);
            }
            data.Add("bdis", bdiList);
            data.Add("total-connected-devices", dept.ConnectedDevices.Count());
            data.Add("total-connections", dept.Connections.Count());

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        // GET: All Connections for a department
        public ActionResult GetAllDepartmentConnections(string id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            var dept = db.Departments.Find(Convert.ToInt32(id));
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();

            data.Add("id", dept.DepartmentID.ToString());
            data.Add("name", dept.Name);
            data.Add("billing-code", dept.BillingCode);
            // retrieve the bdiNumbers from associated BDIs
            var bdis = dept.BDIs;
            List<string> bdiList = new List<string>();
            foreach (var item in bdis)
            {
                bdiList.Add(item.BDINumber);
            }
            data.Add("bdis", bdiList);

            //getConnectionData. Should be a list.
            var connections = dept.Connections;
            List<Dictionary<string, dynamic>> connectionList = new List<Dictionary<string, dynamic>>();
            foreach (var item in connections)
            {
                Dictionary<string, dynamic> connection = new Dictionary<string, dynamic>();
                connection.Add("bdi", item.BDINumber);
                connection.Add("mac", item.ConnectedDevice.Mac);
                connection.Add("reporting-device",item.ReportingDevice.DeviceName);
                connection.Add("date", item.ConnectionDateTime.ToString());
                connectionList.Add(connection);
            }

            data.Add("connections",connectionList);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
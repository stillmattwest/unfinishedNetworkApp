using System.Web.Mvc;
using NetworkBillingSystem_Alpha.DAL;
using NetworkBillingSystem_Alpha.Utilities;

namespace NetworkBillingSystem_Alpha.Controllers
{
    public class ArpTestController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        private SshMethods sshMethods = new SshMethods();
        private BillingDataMethods billingDataMethods = new BillingDataMethods();

        [HttpGet]
        public JsonResult getArp()
        {
            var data = sshMethods.RunSshCommand("show arp")[0].Split(',');

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult getBillingData()
        {
            var result = billingDataMethods.GetBillingData();

            return Json(result, JsonRequestBehavior.AllowGet);
        }




    }
}
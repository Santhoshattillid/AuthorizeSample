using System.Web.Mvc;

namespace AuthorizeSample.Controllers
{
    public class AuthorizeController : Controller
    {
        //
        // GET: /Authorize/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Status()
        {
            ViewBag.Status = Request.QueryString["m"] ?? "";
            ViewBag.Valid = Request.QueryString["Valid"] ?? "";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sim(FormCollection post)
        {
            var response = new AuthorizeNet.SIMResponse(post);
            var isValid = response.Validate(Utilities.MerchantHash, Utilities.ApiLoginID);
            //if (!isValid)
            //    return Redirect("/Authorize/Status?m=failed");
            var returnUrl = Utilities.SiteUrl + "/Authorize/Status?m=" + response.Message + "&Valid=" + isValid;
            return Content(AuthorizeNet.Helpers.CheckoutFormBuilders.Redirecter(returnUrl));

        }

    }
}

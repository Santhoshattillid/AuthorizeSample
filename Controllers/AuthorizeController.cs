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
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Sim(FormCollection post)
        {
            var response = new AuthorizeNet.SIMResponse(post);
            var isValid = response.Validate(Utilities.TransactionKey, Utilities.ApiLoginID);
            if (!isValid)
                return Redirect("/Authorize/Status?m=failed");
            var returnUrl = Utilities.SiteUrl + "/Authorize/Status?m=" + response.Message;
            return Content(AuthorizeNet.Helpers.CheckoutFormBuilders.Redirecter(returnUrl));

        }

    }
}

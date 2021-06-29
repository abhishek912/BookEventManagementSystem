using BookEventManager.Business.Logic;
using BookEventManager.Shared.DTO;
using System.Web.Mvc;
using System.Web.Security;

namespace BookEventManager.UserInterface.Controllers
{
    public class UserController : Controller
    {
        private IWriteData writeObj;
        public UserController(IWriteData writeObj)
        {
            this.writeObj = writeObj;
        }

        // GET: User
        public ActionResult Signup()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserDTO user)
        {
            Validate v = new Validate();
            int result = v.EmailExists(user.Email);
            if (result == -1)
            {
                ModelState.AddModelError("", "Email already exists, try with some other Email Address.");
                return View();
            }
            else
            {
                
                if(ModelState.IsValid)
                {
                    bool signedUp = writeObj.SignUp(user);
                    if (signedUp)
                    {
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Some Error Occured at our end, Please try again later!!!");
                        return View();
                    }
                }
                else
                {
                    //ModelState.AddModelError("", "Some Error Occured at our end, Please try again later!!!");
                    return View();
                }
            }
        }

        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(CredentialDTO cred)
        {
            Validate v = new Validate();
            int result = v.ValidateUser(cred.Email, cred.Password);
            if(result == -1)
            {
                ModelState.AddModelError("", "Invalid Credientials!");
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(cred.Email, false);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
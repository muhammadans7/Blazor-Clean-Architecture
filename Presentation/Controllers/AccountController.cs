using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;

namespace WebApplication1.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                    SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {

           
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    // Store user email in session
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    ViewBag.email = user.Email;
                    // Redirect to the desired page after successful login
                    return RedirectToAction("Enroll", "Enrollment",new { email = user.Email });
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}



// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using CourseraClone.Models;

// namespace CourseraClone.Controllers
// {
//     public class AccountController : Controller
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly SignInManager<IdentityUser> _signInManager;

//         public AccountController(UserManager<IdentityUser> userManager,
//                                     SignInManager<IdentityUser> signInManager)
//         {
//                     _userManager = userManager;
//                     _signInManager = signInManager;
//         }

//         public IActionResult SignUp()
//         {
//             return View();
//         }

//         [HttpPost]
//         public async Task<IActionResult> SignUp(SignupViewModel model)
//         {
//             if (ModelState.IsValid)
//             {
//                 var user = new IdentityUser
//                 {
//                     UserName = model.Email,
//                     Email = model.Email,
//                 };

//                 var result = await _userManager.CreateAsync(user, model.Password);

//                 if (result.Succeeded)
//                 {
//                     await _signInManager.SignInAsync(user, isPersistent: false);

//                     return RedirectToAction("index", "Home");
//                 }

//                 foreach (var error in result.Errors)
//                 {
//                     ModelState.AddModelError("", error.Description);
//                 }

//                 ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

//             }
//             return View(model);
//         }
        
//         [HttpGet]
//         public IActionResult Login()
//         {
//             return View();
//         }
//         [HttpPost]
      
//         public async Task<IActionResult> Login(LoginViewModel user)
//         {
//             if (ModelState.IsValid)
//             {
//                 var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

//                 if (result.Succeeded)
//                 {
//                     return RedirectToAction("Index", "Home");
//                 }

//                 ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

//             }
//             return View(user);
//         } 


//     }
// }

// private readonly UserManager<IdentityUser> _userManager; // Inject UserManager for Identity
        // private readonly SignInManager<IdentityUser> _signInManager; // Inject SignInManager for Identity

        // // Injecting UserManager and SignInManager into the controller
        // public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        // {
        //     _userManager = userManager;
        //     _signInManager = signInManager;
        // }

        // [HttpGet]
        // public IActionResult Login()
        // {
        //     return View();
        // }

        // [HttpPost]
        // public async Task<IActionResult> Login(LoginViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        //         if (result.Succeeded)
        //         {
        //             return RedirectToAction("Index", "Courses");
        //         }
        //         else
        //         {
        //             ModelState.AddModelError("", "Invalid email or password.");
        //         }
        //     }

        //     // Return the same view with validation errors if model state is not valid
        //     return View(model);
        // }

        // [HttpGet]
        // public IActionResult Signup()
        // {
        //     return View();
        // }

        // [HttpPost]
        // public async Task<IActionResult> Signup(SignupViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = new IdentityUser
        //         {
        //             UserName = model.Username,
        //             Email = model.Email
        //         };

        //         var result = await _userManager.CreateAsync(user, model.Password);

        //         if (result.Succeeded)
        //         {
        //             // Optionally sign in the user immediately after registration
        //             await _signInManager.SignInAsync(user, isPersistent: false);
        //             return RedirectToAction("Index", "Courses");
        //         }

        //         // Add errors to ModelState if user creation fails
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError("", error.Description);
        //         }
        //     }

        //     // If the model is not valid, return the same view with validation errors
        //     return View(model);
        // }

        // public IActionResult Profile()
        // {
        //     var model = new UserProfile
        //     {
        //         UserName = "tyrion",  // Replace with actual user data retrieval
        //         Email = "tyrion@gmail.com",  // Replace with actual user data retrieval
        //         FirstName = "John",
        //         LastName = "Doe"
        //     };

        //     return View(model);
        // }


// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using CourseraClone.Models;
// using CourseraClone.Repositories.Interfaces;


// namespace CourseraClone.Controllers
// {
//     public class AccountController : Controller
//     {

       
//         private readonly IUserRepository _userRepository;

//         // Injecting IUserRepository into the controller
//         public AccountController(IUserRepository userRepository)
//         {
//             _userRepository = userRepository;
//         }

//         [HttpGet]
//         public IActionResult Login()
//         {
//             return View();
//         }


//         [HttpPost]
//         public IActionResult Login(LoginViewModel model)
//         {
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     var user = _userRepository.GetUserByEmailAndPassword(model.Email, model.Password);

//                     if (user != null)
//                     {
                        
//                         return RedirectToAction("Index", "Courses");
//                     }
//                     else
//                     {
//                         // Invalid login, add a model error
//                         ModelState.AddModelError("", "Invalid email or password.");
//                         return View(model);
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     // Log the exception (if you have logging setup)
//                     ModelState.AddModelError("", "An error occurred while processing your request.");
//                     // Optionally rethrow the exception
//                 }
//             }

//             // If the model is not valid, return the same view with validation errors
//             return View(model);
//         }

//         [HttpGet]
//         public IActionResult Signup()
//         {
//             return View();
//         }

//         [HttpPost]
//         public IActionResult Signup(SignupViewModel model)
//         {
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     if (_userRepository.UserExists(model.Username, model.Email))
//                     {
//                         // User already exists, add a model error
//                         ModelState.AddModelError("", "User with this username or email already exists.");
//                         return View(model);
//                     }

//                     var user = new User
//                     {
//                         Username = model.Username,
//                         Email = model.Email,
//                         Password = model.Password 
//                     };

//                     _userRepository.AddUser(user);

//                     // Redirect to the Login page after successful registration
//                     return RedirectToAction("Login", "Account");
//                 }
//                 catch (Exception ex)
//                 {
//                     // Log the exception (if you have logging setup)
//                     ModelState.AddModelError("", "An error occurred while processing your request.");
//                 }
//             }

//             // If the model is not valid, return the same view with validation errors
//             return View(model);
//         }

//         public IActionResult Profile()
//         {
//             var model = new UserProfile
//             {
//                 UserName = "tyrion",  // Replace with actual user data retrieval
//                 Email = "tyrion@gmail.com",  // Replace with actual user data retrieval
//                 FirstName = "John",
//                 LastName = "Doe"
//             };

//             return View(model);
//         }

      
//     }
// }






using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MasterProject.Controllers
{
	[Route("[controller]")]
	public class AccountController :Controller
	{
		private readonly UserManager<AppUser> _UserManager;
		private readonly SignInManager<AppUser> _SigninManager;
		private readonly AppDbContext _Context;
		private readonly Service1 _Service1;
		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager,AppDbContext context,Service1 service1)
		{
			_UserManager = userManager;
			_SigninManager = signinManager;
			_Context = context;
			_Service1 = service1;
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginView)
		{
			ViewData["State"] = null;
			if (!ModelState.IsValid)
			{
				return View(loginView);
			}
			AppUser? user= await _UserManager.FindByEmailAsync(loginView.Email);
			if(user is null)
			{
				ViewData["State"] = "No User exists with the provided Email";
				return View();
			}
			bool PasswordCheck = await _UserManager.CheckPasswordAsync(user, loginView.Password);
			if (PasswordCheck)
			{
				var LoginResult = await _SigninManager.PasswordSignInAsync(user, loginView.Password ,true ,true);
				if (LoginResult.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewData["State"] = "Something went wrong while Logging the user in";
					return View();
				}

			}
			ViewData["State"] = "Email or Password is wrong!";
			return View(loginView);

		
		}
		[HttpGet]
		[Route("[action]")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> Register(RegisterViewModel registerView)
		{
            using (_Context.Database.BeginTransaction())
            {
                try
                {
                    ViewData["State"] = null;
                    if (!ModelState.IsValid)
                    {

                        return View(registerView);
                    }
                    IdentityUser? user = await _UserManager.FindByEmailAsync(registerView.Email);
                    if (user is not null)
                    {
                        ViewData["State"] = "User exists";
                        return View(registerView);
                    }
                    AppUser UserToRegister = new AppUser
                    {
                        Email = registerView.Email,
                        UserName = registerView.Email
                    };
                    var CreationResult = await _UserManager.CreateAsync(UserToRegister, registerView.Password);
                    if (CreationResult.Succeeded)
                    {
                        var result = await _UserManager.AddToRoleAsync(UserToRegister, "User");
                        if (result.Succeeded)
                        {
							_Context.SaveChanges();
                            _Context.Database.CommitTransaction();
                            return RedirectToAction("Index","Home");
						}
						else
						{
                            ViewData["State"] = "something went wrong assining the user Role";
                            throw new Exception();
						}

                    }
					await _Context.SaveChangesAsync();
                    _Context.Database.CommitTransaction();
                    return RedirectToAction("Index", "Home");
				}
				catch
				{
                    _Context.Database.RollbackTransaction();
					return View(registerView);

                }
				
			}
			
        }

		[HttpGet]
		[Route("[action]")]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Logout()
        {
			await _SigninManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
        }

		[HttpGet]
		[Route("[action]")]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> Dashboard()
		{
			AppUser user = await _Service1.GetLoggedInUserAsync();
			ICollection<ToDoModel> Todos=await _Context.ToDos.Where(T => T.AppUserId == user.Id).ToListAsync();
			return View(Todos);


		}

		[HttpGet]
		[Route("[action]")]
		[Authorize(Roles = "Admin,User")]
		public IActionResult AddToDo()
		{
			return View();
		}


        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> AddToDo(AddToDoViewModel addToDoView)
		{
			if (!ModelState.IsValid)
			{
				return View(addToDoView);
			}
			AppUser user = await _Service1.GetLoggedInUserAsync();


			ToDoModel ToDo = new ToDoModel
			{
				Title = addToDoView.Title,
				Description = addToDoView.Description,
				DueTime = DateTime.SpecifyKind(addToDoView.DueTime, DateTimeKind.Utc),
				AppUserId = user.Id

			};

			_Context.Add<ToDoModel>(ToDo);
			_Context.SaveChanges();
			return RedirectToAction("Dashboard", "Account");

		}


    }
}


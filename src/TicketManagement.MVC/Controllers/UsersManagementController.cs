﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketManagement.Common.Identity;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersManagementController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IStringLocalizer<UsersManagementController> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly ILogger<RoleManager<Role>> _logger;

        public UsersManagementController(
            RoleManager<Role> roleManager,
            IStringLocalizer<UsersManagementController> localizer,
            UserManager<User> userManager,
            IUserStore<User> userStore,
            ILogger<RoleManager<Role>> logger)
        {
            _logger = logger;
            _roleManager = roleManager;
            _localizer = localizer;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUser model)
        {
            model.UserName = model.Email;
            model.NormalizedEmail = string.Format(model.Email).ToUpper();
            model.NormalizedUserName = string.Format(model.Email).ToUpper();

            var user = new User();

            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                user.FirstName = model.FirstName;
                user.SurName = model.SurName;
                user.PhoneNumber = model.PhoneNumber;
                user.TimeZone = DateTimeOffset.Now.Offset.ToString();
                user.Language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;

                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Index");
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<User>)_userStore;
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            CreateUser model = new ()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SurName = user.SurName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateUser model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            if (ModelState.IsValid)
            {
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.SurName = model.SurName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ChangeRole(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRole model = new ()
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles,
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, IEnumerable<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var addedRoles = roles.Except(userRoles);

                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}
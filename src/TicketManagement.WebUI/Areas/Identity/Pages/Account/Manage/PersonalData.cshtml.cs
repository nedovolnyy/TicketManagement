using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly UsersManagementApiClient _usersManagementApiClient;
        private readonly string _userId;

        public PersonalDataModel(
            ILogger<PersonalDataModel> logger,
            UsersManagementApiClient usersManagementApiClient)
        {
            _logger = logger;
            _usersManagementApiClient = usersManagementApiClient;
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _usersManagementApiClient.GetByIdUserAsync(_userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userId}'.");
            }

            return Page();
        }
    }
}

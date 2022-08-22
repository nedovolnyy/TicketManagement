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

        public PersonalDataModel(
            ILogger<PersonalDataModel> logger,
            UsersManagementApiClient usersManagementApiClient)
        {
            _logger = logger;
            _usersManagementApiClient = usersManagementApiClient;
        }

        public async Task<IActionResult> OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _usersManagementApiClient.GetByIdUserAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            return Page();
        }
    }
}

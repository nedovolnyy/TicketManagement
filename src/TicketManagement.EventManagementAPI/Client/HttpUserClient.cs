using TicketManagement.EventManagementAPI.Contracts;

namespace TicketManagement.EventManagementAPI.Client
{
    public interface IUserClient
    {
        public Task<string> Register(UserModel userModel, CancellationToken cancellationToken = default);

        public Task<string> Login(UserModel userModel, CancellationToken cancellationToken = default);

        public Task ValidateToken(string token, CancellationToken cancellationToken = default);
    }

    internal class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Register(UserModel userModel, CancellationToken cancellationToken = default)
            => await AuthorizeInternal(userModel, "api/users/register", cancellationToken);

        public async Task<string> Login(UserModel userModel, CancellationToken cancellationToken = default)
            =>await AuthorizeInternal(userModel, "api/users/login", cancellationToken);

        public async Task ValidateToken(string token, CancellationToken cancellationToken = default)
        {
            var address = $"api/users/validate?token={token}";
            var message = await _httpClient.GetAsync(address, cancellationToken);
            message.EnsureSuccessStatusCode();
        }

        private async Task<string> AuthorizeInternal(UserModel userModel, string path, CancellationToken cancellationToken)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(userModel.Login), nameof(UserModel.Login) },
                { new StringContent(userModel.Password), nameof(UserModel.Password) },
            };
            var result = await _httpClient.PostAsync(path, form, cancellationToken);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}

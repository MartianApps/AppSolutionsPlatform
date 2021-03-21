using AppSolutions.Platform.Models.UserManagement;
using AppSolutions.Platform.Services.Helpers;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.TeamServer.Controllers
{
    public class UserController : Controller
    {
        private HttpRequestHelper _requestHelper;

        public UserController(IConfiguration configuration)
        {
            var baseUrl = configuration["AppSettings:TeamServerBaseUrl"];
            _requestHelper = new HttpRequestHelper(baseUrl);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(ClientRegistrationRequest formRequest)
        {
            formRequest.EncryptedPassword = StringCypher.Encrypt(formRequest.EncryptedPassword);

            var response = await _requestHelper.PostData<ClientRegistrationResponse>("api/RegisteredClients", formRequest);

            return Content($"Hello {formRequest.FirstName}");
        }
    }
}

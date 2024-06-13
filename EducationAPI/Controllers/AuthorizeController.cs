using EducationAPI.Common;
using EducationAPI.DTO;
using EducationAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        AuthorizeService authrorizeService = new AuthorizeService();

        [HttpPost]
        [Route("Login")]
        public async Task<CommonResponse<UserLoginModel>> Login(LoginDTO login)
        {
            return await authrorizeService.Login(login);
        }
    }
}

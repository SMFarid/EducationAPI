using EducationAPI.Common;
using EducationAPI.DTO;
using EducationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class AuthorizeController : ControllerBase
    {
        AuthorizeService authrorizeService = new AuthorizeService();

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<CommonResponse<UserLoginModel>> Login(LoginDTO login)
        {
            return await authrorizeService.Login(login);
        }
    }
}

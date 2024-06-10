using EducationAPI.Common;
using EducationAPI.DTO;

namespace EducationAPI.Services
{
    public class AuthorizeService
    {
        public AuthorizeService() { }

        public async Task<CommonResponse<UserLoginModel>> Login (LoginDTO login)
        {
            var res = new CommonResponse<UserLoginModel>();

            return res;
        }
    }
}

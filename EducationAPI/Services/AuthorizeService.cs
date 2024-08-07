﻿using Azure.Messaging;
using EducationAPI.Common;
using EducationAPI.DTO;
using EducationAPI.Repositories;

namespace EducationAPI.Services
{
    public class AuthorizeService
    {
        AuditorRepository repository = new AuditorRepository();
        public AuthorizeService() { }

        public async Task<CommonResponse<UserLoginModel>> Login (LoginDTO login)
        {
            var res = new CommonResponse<UserLoginModel>();
            try
            {
                var auditor = await repository.getAuditor(login.username, login.password);
                if (auditor == null)
                {
                    res.Errors = new List<Error> { new Error { Message = " Auditor not found!" } };
                    return res;
                }

                UserLoginModel userLoginModel = new UserLoginModel
                {
                    NameAr = auditor.NameAr,
                    NameEn = auditor.NameEn,
                    username = auditor.Username,
                    password = auditor.Password,
                    email = auditor.Email,
                    AuditorID = auditor.Id
                };
                res.Data = userLoginModel;
            } catch (Exception ex)
            {
                res.Errors = new List<Error> { new Error { Message = ex.ToString() } };
                return res;
            }
            
            return res;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WuJinAPI.JWT;
using WuJinAPI.Models;
using WuJinAPI.Resources;
using WuJinAPI.Services;

namespace WuJinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController:ControllerBase
    {
        private readonly ITokenHelper tokenHelper = null;
        public LoginController(ITokenHelper _tokenHelper)
        {
            tokenHelper = _tokenHelper;
        }
        [HttpPost]
        public ReturnModel Login([FromBody] UserDto user)
        {
            UserService userService = new UserService();
            UserController userController = new UserController(userService);

            var ret = new ReturnModel();

            try
            {
                if (string.IsNullOrWhiteSpace(user.LoginID) || string.IsNullOrWhiteSpace(user.Password))
                {
                    ret.Code = 201;
                    ret.Msg = "用户名密码不能为空";
                    return ret;
                }
                var result = userController.ValidateUser(user.LoginID, user.Password);
                if (result)
                {
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
                    {
                        { "loginID", user.LoginID }
                    };
                    ret.Code = 200;
                    ret.Msg = "登录成功";
                    ret.TnToken = tokenHelper.CreateToken(keyValuePairs);
                }
            }
            catch (Exception ex)
            {
                ret.Code = 500;
                ret.Msg = "登录失败:" + ex.Message;
            }
            return ret;
        }
        [HttpGet]

        [HttpGet]
        public ReturnModel ValiToken(string tokenStr)
        {
            var ret = new ReturnModel
            {
                TnToken = new TnToken()
            };
            bool isvilidate = tokenHelper.ValiToken(tokenStr);
            if (isvilidate)
            {
                ret.Code = 200;
                ret.Msg = "Token验证成功";
                ret.TnToken.TokenStr = tokenStr;
            }
            else
            {
                ret.Code = 500;
                ret.Msg = "Token验证失败";
                ret.TnToken.TokenStr = tokenStr;
            }
            return ret;
        }
        [HttpGet]
        public ReturnModel ValiTokenState(string tokenStr)
        {
            var ret = new ReturnModel
            {
                TnToken = new TnToken()
            };
            string loginID = "";
            TokenType tokenType = tokenHelper.ValiTokenState(tokenStr, a => a["iss"] == "WYY" && a["aud"] == "EveryTestOne", action => { loginID = action["loginID"]; });
            if (tokenType == TokenType.Fail)
            {
                ret.Code = 202;
                ret.Msg = "token验证失败";
                return ret;
            }
            if (tokenType == TokenType.Expired)
            {
                ret.Code = 205;
                ret.Msg = "token已经过期";
                return ret;
            }

            //..............其他逻辑
            var data = new List<Dictionary<string, string>>();
            var bb = new Dictionary<string, string>
            {
                { "Wyy", "123456" }
            };
            data.Add(bb);
            ret.Code = 200;
            ret.Msg = "访问成功!";
            ret.Data = data;
            return ret;
        }
    }
}

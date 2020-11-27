using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WuJinAPI.JWT;
using WuJinAPI.Models;

namespace WuJinAPI.Filter
{
    public class TokenFilter : Attribute, IActionFilter
    {
        private ITokenHelper tokenHelper;
        public TokenFilter(ITokenHelper _tokenHelper)
        {
            tokenHelper = _tokenHelper;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ReturnModel ret = new ReturnModel();
            object tokenobj = context.ActionArguments["token"];
            if (tokenobj == null)
            {
                ret.Code = 201;
                ret.Msg = "token不能为空";
                context.Result = new JsonResult(ret);
                return;
            }
            string token = tokenobj.ToString();
            string userId = "";
            TokenType tokenType = tokenHelper.ValiTokenState(token, a => a["iss"] == "WYY" && a["aud"] == "EveryTestOne", action => { userId = action["userId"]; });
            if (tokenType == TokenType.Fail)
            {
                ret.Code = 202;
                ret.Msg = "token验证失败";
                context.Result = new JsonResult(ret);
                return;
            }
            if (tokenType == TokenType.Expired)
            {
                ret.Code = 205;
                ret.Msg = "token已经过期";
                context.Result = new JsonResult(ret);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                //给控制器传递参数(需要什么参数其实可以做成可以配置的，在过滤器里边加字段即可)
                //context.ActionArguments.Add("userId", Convert.ToInt32(userId));
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WuJinAPI.JWT
{
    public interface ITokenHelper
    {
        //根据一个对象通过反射提供负载生成token
        TnToken CreateToken<T>(T user) where T : class;
        //根据键值对提供负载生成token
        TnToken CreateToken(Dictionary<string, string> keyValuePairs);
        //Token验证
        bool ValiToken(string encodeJwt, Func<Dictionary<string, string>, bool> validatePayLoad = null);
        //带返回状态的Token状态
        TokenType ValiTokenState(string encodeJwt, Func<Dictionary<string, string>, bool> validatePayLoad, Action<Dictionary<string, string>> action);
    }
}

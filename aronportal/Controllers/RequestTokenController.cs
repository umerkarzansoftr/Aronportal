using aronportal.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aronportal.Controllers
{
    public class RequestTokenController : ApiController
    {
        public HttpResponseMessage Get(string username, string password)
        {
            if (CheckUser(username, password))
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             JwtAuthManager.GenerateJWTToken(username));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
             "Invalid Request");
            }
        }
        public bool CheckUser(string username, string password)
        {
            // for demo purpose, I am simply checking username and password with predefined strings. you can have your own logic as per requirement.
            if (username == "codeadda" && password == "abc123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Data.Session
{
    public class Session
    {
        public void SavePlayerIdToSession(int id, HttpContext httpContext)
        {
            httpContext.Session.SetString("CurrentPlayerId", id.ToString());
        }

        public int GetPlayerIdFromSession(HttpContext httpContext)
        {
            string sessionString = httpContext.Session.GetString("CurrentPlayerId");
            return sessionString != null ? int.Parse(sessionString) : -1;
        }
    }
}

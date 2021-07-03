#region

using System.Linq;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace comrade.WebApi.Bases
{
    public class ComradeController : ControllerBase
    {
        [NonAction]
        protected int? GetUserId()
        {
            return User != null ? int.Parse(User.Claims.First(i => i.Type == "Key").Value) : 0;
        }
    }
}
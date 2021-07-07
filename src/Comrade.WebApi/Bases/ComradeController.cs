﻿#region

using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Comrade.WebApi.Bases
{
    public class ComradeController : ControllerBase
    {
        [NonAction]
        protected int? GetUserId()
        {
            return User != null
                ? int.Parse(User.Claims.First(i => i.Type == "Key").Value,
                    CultureInfo.CurrentCulture)
                : 0;
        }
    }
}
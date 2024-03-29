﻿#region

using System.Collections.Generic;
using System.Globalization;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Paginations;
using Comrade.Core.Messages;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Application.Bases
{
    public class PageResultDto<T> : ResultDto, IPageResultDto<T>
        where T : Dto
    {
        public PageResultDto(IList<T> data)
        {
            Data = data;
            Code = data == null ? (int) EnumResponse.ErrorNotFound : (int) EnumResponse.Success;
            Success = data != null;
            Message = data == null
                ? BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture)
                : string.Empty;
        }

        public PageResultDto(PaginationFilter pagination, IList<T> data)
        {
            Data = data;
            PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : null;
            PageSize = pagination.PageNumber >= 1 ? pagination.PageSize : null;
            NextPage = pagination.PageNumber + 1;
            PreviusPage = pagination.PageNumber > 1 ? pagination.PageNumber - 1 : null;
            Code = data == null ? (int) EnumResponse.ErrorNotFound : (int) EnumResponse.Success;
            Success = data != null;
            Message = data == null
                ? BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture)
                : string.Empty;
        }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? NextPage { get; set; }
        public int? PreviusPage { get; set; }

        public IList<T>? Data { get; set; }
    }
}
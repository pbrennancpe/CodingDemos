using System;
using Ticketing.Models.Enums;

namespace Ticketing.Extensions
{
    public static class StringExtensions
    {
        public static Status ToStatusEnum(this string param)
        {
            return param?.ToLowerInvariant() switch
            {
                "open" or "0" => Status.Open,
                "inprogress" or "in progress" or "progress" or "1" => Status.InProgress,
                "closed" or "2" => Status.Closed,
                _ => Status.Error
            };
        }
    }
}
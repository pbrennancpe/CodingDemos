using System;
using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models.Enums
{

    public enum Status

    {
        [Display(Name = "Open")]
        Open,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Closed")]
        Closed,
        [Display(Name = "Error")]
        Error
    }



}


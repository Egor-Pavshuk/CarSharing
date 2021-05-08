using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCarSharing2.Models
{
    public class PagingInfoView
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
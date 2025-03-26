using GS.Web.Framework.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace GS.NewAPI.Models
{
    public class DonViBoPhanModel : BaseGSApiModel
    {
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }

    }
}

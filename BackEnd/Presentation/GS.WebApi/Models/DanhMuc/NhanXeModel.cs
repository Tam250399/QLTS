//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace GS.WebApi.Models.DanhMuc
{
    public class NhanXeModel : BaseGSApiModel
    {
        public NhanXeModel()
        {

        }
        [Required(ErrorMessage ="MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        [Required(ErrorMessage = "DB_ID null")]
        public int? DB_ID { get; set; }
    }
    
}


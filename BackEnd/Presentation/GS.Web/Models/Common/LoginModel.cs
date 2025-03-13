using GS.Core;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using GS.Web.Validators.Customer;

namespace GS.Web.Models.Common
{
    //[Validator(typeof(LoginValidator))]
    public partial class LoginModel : BaseGSModel
    {
        public bool CheckoutAsGuest { get; set; }

        [DataType(DataType.EmailAddress)]
        [GSResourceDisplayName("Account.Login.Fields.Email")]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }
        [GSResourceDisplayName("Account.Login.Fields.UserName")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [NoTrim]
        [GSResourceDisplayName("Account.Login.Fields.Password")]
        public string Password { get; set; }

        [GSResourceDisplayName("Account.Login.Fields.RememberMe")]
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }
        public string messageResult { get; set; }
    }
    public class TestArrayModel//: BaseGSEntityModel
    {
        //public decimal ID { get; set; }
        ////public IFormCollection Form { get; set; }

        ///// <summary>
        ///// Gets or sets property to store any custom values for models 
        ///// </summary>
        //public Dictionary<string, object> CustomProperties { get; set; }
        public string Ten { get; set; }
        public int Tuoi { get; set; }
        [UIHint("InputAddon")]
        public double? CanNang { get; set; }
        public decimal? SoLuong { get; set; }

    }
    public class TestModel
    {
        public TestModel()
        {
            this.ddlDonViCha = new List<SelectListItem>();
            this.ddlDonViCon = new List<SelectListItem>();
        }
        [GSResourceDisplayName("AppWork.WorkFile.Fields.Test")]
        [UIHint("WorkFile")]
        public string WorkFileIds { get; set; }
        [UIHint("Date")]
        public DateTime WorkDate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? WorkDateNull { get; set; }
        public Decimal WorkNumber { get; set; }
        public Decimal? WorkNumberNull { get; set; }
        public Int64 Int64Number { get; set; }
        public Int32 IntNumber { get; set; }
        [UIHint("WorkCurrency")]
        public String WorkCurrency { get; set; }
        public bool KiemTraXacThuChuKySo { get; set; }
        public string TestId { get; set; }
        [UIHint("InputAddon")]
        public decimal? TestInputAddon { get; set; }
        public string testJson { get; set; }
        public int DonViCha { get; set; }
        public List<SelectListItem> ddlDonViCha { get; set; }
        public int DonViCon { get; set; }
        public List<SelectListItem> ddlDonViCon { get; set; }
	}
    public class TestMapperModel:BaseGSEntityModel
    {
        public decimal PARENT_ID { get; set; }
    }
    public class TestMapperEntity:BaseEntity
    {
        public TestMapperEntity()
        {
            ID = 10;

        }
        public decimal? PARENT_ID { get; set; }
    }
}
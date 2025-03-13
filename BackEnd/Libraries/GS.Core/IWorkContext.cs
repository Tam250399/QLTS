using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;

namespace GS.Core
{
    /// <summary>
    /// Represents work context
    /// </summary>
    public interface IWorkContext
    {
        //ToaAn CurrentToaAn { get; set; }
        ///// <summary>
        ///// Gets or sets the current customer
        ///// </summary>
        NguoiDung CurrentCustomer { get; set; }

        /// <summary>
        /// Gets or sets the current DonVi
        /// </summary>
        InfoCacheDonvi CurrentDonVi { get; set; }

        ///// <summary>
        ///// Gets the current vendor (logged-in manager)
        ///// </summary>
        //Vendor CurrentVendor { get; }

        ///// <summary>
        ///// Gets or sets current user working language
        ///// </summary>
        //Language WorkingLanguage { get; set; }

        ///// <summary>
        ///// Gets or sets current user working currency
        ///// </summary>
        //Currency WorkingCurrency { get; set; }

        ///// <summary>
        ///// Gets or sets current tax display type
        ///// </summary>
        //TaxDisplayType TaxDisplayType { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }
        //NguoiDung OriginalCustomerIfImpersonated { get; set; }
    }
}

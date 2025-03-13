using GS.Core.Configuration;

namespace GS.Core.Domain.CauHinh
{
    public class CauHinhNguoiDung : ICauHinh
    {

    }
    /// <summary>
    /// Thiet dat cac gia tri mac dinh lien quan den nguoi dung
    /// </summary>
    public static partial class GSNguoiDung
    {
        #region System customer roles

        /// <summary>
        /// Gets a system name of 'administrators' customer role
        /// </summary>
        public static string AdministratorsRoleName => "Administrators";
        public static int AdministratorsRoleId => 1;

        #endregion


    }
}

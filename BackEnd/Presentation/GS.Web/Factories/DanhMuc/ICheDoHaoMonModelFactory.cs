//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
namespace GS.Web.Factories.DanhMuc
{
    public partial interface ICheDoHaoMonModelFactory
    {
        #region CheDoHaoMon

        CheDoHaoMonSearchModel PrepareCheDoHaoMonSearchModel(CheDoHaoMonSearchModel searchModel);

        CheDoHaoMonListModel PrepareCheDoHaoMonListModel(CheDoHaoMonSearchModel searchModel);

        CheDoHaoMonModel PrepareCheDoHaoMonModel(CheDoHaoMonModel model, CheDoHaoMon item, bool excludeProperties = false);

        void PrepareCheDoHaoMon(CheDoHaoMonModel model, CheDoHaoMon item);
        bool CheckMaCheDoHaoMon(decimal id, string ma);
        #endregion
    }
}

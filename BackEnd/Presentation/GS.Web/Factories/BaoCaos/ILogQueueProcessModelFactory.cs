//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BaoCaos;
using GS.Web.Models.BaoCaos;
namespace GS.Web.Factories.BaoCaos
{
    public partial interface ILogQueueProcessModelFactory 
    {    
        #region LogQueueProcess
    
        LogQueueProcessSearchModel PrepareLogQueueProcessSearchModel(LogQueueProcessSearchModel searchModel);
        
        LogQueueProcessListModel PrepareLogQueueProcessListModel(LogQueueProcessSearchModel searchModel);
        
        LogQueueProcessModel PrepareLogQueueProcessModel(LogQueueProcessModel model, LogQueueProcess item, bool excludeProperties = false);
        
        void PrepareLogQueueProcess(LogQueueProcessModel model, LogQueueProcess item);
        
        #endregion        
	}
}

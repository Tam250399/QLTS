//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/6/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
namespace GS.Web.Factories.HeThong
{
    public partial interface IScheduleTaskModelFactory 
    {    
        #region ScheduleTask
    
        ScheduleTaskSearchModel PrepareScheduleTaskSearchModel(ScheduleTaskSearchModel searchModel);
        
        ScheduleTaskListModel PrepareScheduleTaskListModel(ScheduleTaskSearchModel searchModel);
        
        ScheduleTaskModel PrepareScheduleTaskModel(ScheduleTaskModel model, ScheduleTask item, bool excludeProperties = false);
        
        void PrepareScheduleTask(ScheduleTaskModel model, ScheduleTask item);
        
        #endregion        
	}
}

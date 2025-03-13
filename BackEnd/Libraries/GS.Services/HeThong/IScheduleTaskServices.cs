//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.HeThong;

namespace GS.Services.HeThong
{
    public partial interface IScheduleTaskService 
    {    
    #region ScheduleTask
        IList<ScheduleTask> GetAllScheduleTasks();
        IPagedList <ScheduleTask> SearchScheduleTasks(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        ScheduleTask GetScheduleTaskById(decimal Id);
        ScheduleTask GetScheduleTaskByName(string Name);
        IList<ScheduleTask> GetScheduleTaskByIds(decimal[] newsIds);
        void InsertScheduleTask(ScheduleTask entity);
        void UpdateScheduleTask(ScheduleTask entity);
        void DeleteScheduleTask(ScheduleTask entity);    
     #endregion
	}
}

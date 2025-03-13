using GS.Core.Configuration;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Services.HeThong;
using Quartz;
using Quartz.Impl;
using System;
using System.Linq;

namespace GS.Services.Tasks
{
    /// <summary>
    /// Represents task manager
    /// </summary>
    public partial class TaskManager
    {
        #region Fields

        private GSConfig _gsConfig { get; set; }

        #endregion Fields

        #region Ctor

        private TaskManager()
        {

        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Initializes the task manager
        /// </summary>
        public void Initialize(GSConfig gsConfig)
        {
            this._gsConfig = gsConfig;
        }

        /// <summary>
        /// Starts the task manager
        /// </summary>
        public async void Start()
        {
            //kiem tra neu co cau hinh job schedule
            if (!_gsConfig.IsJobScheduler)
                return;
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();


            // get a scheduler
            IScheduler sched = await schedFact.GetScheduler();
            await sched.Start();

            //setup job
            var taskService = EngineContext.Current.Resolve<IScheduleTaskService>();

            var scheduleTasks = taskService
                .GetAllScheduleTasks()
                .OrderBy(x => x.SECONDS)
                .ToList();

            foreach (var scheduleTask in scheduleTasks)
            {
                var job = CreateJob(scheduleTask);
                var trigger = CreateTrigger(scheduleTask);

                await sched.ScheduleJob(job, trigger);
            }

            #region Task cần cài đặt lại trong data base;
            #region Job moi ngay chay chốt tài sản

            //define the job and tie it to our HelloJob class
            //IJobDetail jobChotTaiSanTask = JobBuilder.Create<ChotTaiSanTask>()
            //     .WithIdentity("CalculateTaiSan", "GroupJobTaiSan")
            //     .Build();

            //ITrigger triggerChotTaiSanTask = TriggerBuilder.Create()
            //    .WithIdentity("CalculateTaiSanTrigger", "GroupJobTaiSan")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(_gsConfig.TimeJobChotTaiSan)
            //        .RepeatForever())           //Lặp lại mãi mãi
            //    .Build();
            //await sched.ScheduleJob(jobChotTaiSanTask, triggerChotTaiSanTask);

            #endregion Job moi ngay chay chốt tài sản

            #region Job chạy 1 phút một lần để render báo cáo

            //IJobDetail jobRenderReport = JobBuilder.Create<RenderReportTask>()
            //   .WithIdentity("RenderReportJob", "GroupJobReport")
            //   .Build();

            /////sau 15 giây sẽ chạy job
            //ITrigger triggerRenderReport = TriggerBuilder.Create()
            //    .WithIdentity("RenderReportJobTrigger", "GroupJobReport")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(1)   //1 phút chạy một lần, render báo cáo
            //        .RepeatForever())           //Lặp lại mãi mãi
            //    .Build();

            //await sched.ScheduleJob(jobRenderReport, triggerRenderReport);

            #endregion Job chạy 1 phút một lần để render báo cáo  
            #region Job chạy 1 phút một lần để render báo cáo đồng bộ

            //IJobDetail jobRenderReportDongBo = JobBuilder.Create<RenderReportDongBoTask>()
            //   .WithIdentity("RenderReportDongBoJob", "GroupJobReportDongBo")
            //   .Build();

            /////sau 15 giây sẽ chạy job
            //ITrigger triggerRenderReportDongBo = TriggerBuilder.Create()
            //    .WithIdentity("RenderReportDongBoJobTrigger", "GroupJobReportDongBo")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(1)   //1 phút chạy một lần, render báo cáo
            //        .RepeatForever())           //Lặp lại mãi mãi
            //    .Build();

            //await sched.ScheduleJob(jobRenderReportDongBo, triggerRenderReportDongBo);

            #endregion Job chạy 1 phút một lần để render báo cáo đồng bộ

            #region Job chạy 1 phút một lần để đồng bộ tài sản

            //IJobDetail jobDongBoApi = JobBuilder.Create<DongBoTask>()
            //  .WithIdentity("DongBoJob", "GroupDongBoJobApi")
            //  .Build();

            //        ///sau 15 giây sẽ chạy job
            //        ITrigger triggerDonBoApi = TriggerBuilder.Create()
            //            .WithIdentity("triggerDongBoJobApi", "GroupDongBoJobApi")
            //            .StartNow()
            //            .WithSimpleSchedule(x => x
            //                .WithIntervalInMinutes(1)   //1 phút chạy một lần, send request api
            //                .RepeatForever())           //Lặp lại mãi mãi
            //            .Build();

            //await sched.ScheduleJob(jobDongBoApi, triggerDonBoApi);

            #endregion Job chạy 1 phút một lần để đồng bộ tài sản

            #region Job chạy 1 phút một lần để đồng bộ queue

            //IJobDetail jobCallDongBoQueueApi = JobBuilder.Create<DongBoQueueTask>()
            //   .WithIdentity("DongBoQueueJob", "GroupDongBoQueueJobApi")
            //   .Build();

            /////sau 15 giây sẽ chạy job
            //ITrigger triggerCallDongBoQueueApi = TriggerBuilder.Create()
            //    .WithIdentity("triggerDongBoQueueJobApi", "GroupDongBoQueueJobApi")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(1)   //1 phút chạy một lần, send request api
            //        .RepeatForever())           //Lặp lại mãi mãi
            //    .Build();

            //await sched.ScheduleJob(jobCallDongBoQueueApi, triggerCallDongBoQueueApi);

            #endregion Job chạy 1 phút một lần để đồng bộ queue      
            #region Job Duyệt tài sản hao mòn
            //IJobDetail jobDuyetHM = JobBuilder.Create<>()
            //   .WithIdentity("RenderReportJob", "TSGroupJobReport")
            //   .Build();

            /////sau 15 giây sẽ chạy job
            //ITrigger triggerDuyetHM = TriggerBuilder.Create()
            //	.WithIdentity("duyetHMJobTrigger", "TSGroupJobReport")
            //	.StartNow()
            //	.WithSimpleSchedule(x => x
            //		.WithIntervalInMinutes(1)   //1 phút chạy một lần, render báo cáo
            //		.RepeatForever())           //Lặp lại mãi mãi
            //	.Build();

            //await sched.ScheduleJob(jobRenderReport, triggerRenderReport);
            #endregion

            #region TS nhận tài sản temp -> tài sản
            //IJobDetail jobNhanTS = JobBuilder.Create<>()
            //   .WithIdentity("RenderReportJob", "TSJobReport")
            //   .Build();

            /////sau 15 giây sẽ chạy job
            //ITrigger triggerNhanTS = TriggerBuilder.Create()
            //	.WithIdentity("NhanTSJobTrigger", "TSJobReport")
            //	.StartNow()
            //	.WithSimpleSchedule(x => x
            //		.WithIntervalInMinutes(1)   //1 phút chạy một lần, render báo cáo
            //		.RepeatForever())           //Lặp lại mãi mãi
            //	.Build();

            //await sched.ScheduleJob(jobRenderReport, triggerRenderReport);
            #endregion
            #region Job đồng bộ tài sản-biến động
            //#region Job moi ngay chay 1 lan luc 01h:00 đẩy biến động tăng mới sang kho

            ////define the job and tie it to our HelloJob class
            //IJobDetail jobDongBoBienDongTangMoiTask = JobBuilder.Create<DongBoBienDongTangMoiTask>()
            //     .WithIdentity("DongBoBienDongTangMoi", "DongBo")
            //     .Build();

            ////Trigger the job to run now, and then every day
            //ITrigger triggerDongBoBienDongTangMoiTask = TriggerBuilder.Create()
            ////.WithIdentity("DongBoBienDongTangMoiTrigger", "DongBo")
            ////.StartNow()
            ////.WithSimpleSchedule(x => x
            ////    .WithIntervalInMinutes(5)   //1 phút chạy một lần, render báo cáo
            ////    .RepeatForever())           //Lặp lại mãi mãi
            ////.Build();
            //.WithDailyTimeIntervalSchedule
            //  (s =>
            //  s.WithIntervalInHours(24)
            //    .OnEveryDay()
            //    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(1, 0)) //jobj chay 1 lan/ ngay luc 01h:00 -> dua vao cau hinh
            //  )
            //.Build();
            //await sched.ScheduleJob(jobDongBoBienDongTangMoiTask, triggerDongBoBienDongTangMoiTask);

            //#endregion
            //#region Job moi ngay chay 1 lan luc 02h:00 Get mã tài sản của kho

            ////define the job and tie it to our HelloJob class
            //IJobDetail JobGetMaTaiSanKho = JobBuilder.Create<GetMaTaiSanKhoTask>()
            //     .WithIdentity("GetMaTaiSanKho", "DongBo")
            //     .Build();

            ////Trigger the job to run now, and then every day
            //ITrigger triggerGetMaTaiSanKhoTask = TriggerBuilder.Create()
            ////.WithIdentity("GetMaTaiSanKhoTrigger", "DongBo")
            ////.StartNow()
            ////.WithSimpleSchedule(x => x
            ////    .WithIntervalInMinutes(4)
            ////    .RepeatForever())
            ////.Build();
            //.WithDailyTimeIntervalSchedule
            //  (s =>
            //    s.WithIntervalInHours(24)
            //    .OnEveryDay()
            //    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(2, 0)) //jobj chay 1 lan/ ngay luc 01h:00 -> dua vao cau hinh
            //  )
            //.Build();
            //await sched.ScheduleJob(JobGetMaTaiSanKho, triggerGetMaTaiSanKhoTask);
            //#endregion 
            //#region Job moi ngay chay 1 lan luc 03h:00 đẩy các biến động khác tăng mới

            ////define the job and tie it to our HelloJob class
            //IJobDetail JobDongBoBienDongKhacTask = JobBuilder.Create<DongBoBienDongKhacTask>()
            //     .WithIdentity("DongBoBienDongKhac", "DongBo")
            //     .Build();

            ////Trigger the job to run now, and then every day
            //ITrigger triggerDongBoBienDongKhacTask = TriggerBuilder.Create()
            ////.WithIdentity("DongBoBienDongKhacTrigger", "DongBo")
            ////.StartNow()
            ////.WithSimpleSchedule(x => x
            ////    .WithIntervalInMinutes(4)   //1 phút chạy một lần, render báo cáo
            ////    .RepeatForever())           //Lặp lại mãi mãi
            ////.Build();
            //.WithDailyTimeIntervalSchedule
            //  (s =>
            //    s.WithIntervalInHours(24)
            //    .OnEveryDay()
            //    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0)) //jobj chay 1 lan/ ngay luc 01h:00 -> dua vao cau hinh
            //  )
            //.Build();
            //await sched.ScheduleJob(JobDongBoBienDongKhacTask, triggerDongBoBienDongKhacTask);

            //#endregion
            //#region Job moi ngay chay 1 lan luc 04h:00 check các biến động đã được đồng bộ qua hay chưa

            ////define the job and tie it to our HelloJob class
            //IJobDetail JobCheckLogTask = JobBuilder.Create<CheckLogTask>()
            //     .WithIdentity("CheckLogBienDong", "DongBo")
            //     .Build();

            ////Trigger the job to run now, and then every day
            //ITrigger triggerCheckLogTask = TriggerBuilder.Create()
            ////.WithIdentity("DongBoBienDongKhacTrigger", "DongBo")
            ////.StartNow()
            ////.WithSimpleSchedule(x => x
            ////    .WithIntervalInMinutes(4)   //1 phút chạy một lần, render báo cáo
            ////    .RepeatForever())           //Lặp lại mãi mãi
            ////.Build();
            //.WithDailyTimeIntervalSchedule
            //  (s =>
            //    s.WithIntervalInHours(24)
            //    .OnEveryDay()
            //    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(4, 0)) //jobj chay 1 lan/ ngay luc 01h:00 -> dua vao cau hinh
            //  )
            //.Build();
            //await sched.ScheduleJob(JobCheckLogTask, triggerCheckLogTask);

            //#endregion
            #endregion

            #region Job đồng bộ tài sản đã nhận từ kho

            //IJobDetail jobDongBoTaiSanDaNhanReport = JobBuilder.Create<DongBoTaiSanDaNhanTask>()
            //   .WithIdentity("RenderDongBoTaiSanDaNhanJob", "GroupJobDongBoTaiSanDaNhan")
            //   .Build();

            //ITrigger triggerDongBoTaiSanDaNhanReport = TriggerBuilder.Create()
            //    .WithIdentity("RenderDongBoTaiSanDaNhanJobTrigger", "GroupJobDongBoTaiSanDaNhan")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(1)   //1 phút chạy một lần
            //        .RepeatForever())           //Lặp lại mãi mãi
            //    .Build();

            //await sched.ScheduleJob(jobDongBoTaiSanDaNhanReport, triggerDongBoTaiSanDaNhanReport);

            #endregion

            #endregion

        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets the task manger instance
        /// </summary>
        public static TaskManager Instance { get; } = new TaskManager();
        private static IJobDetail CreateJob(ScheduleTask schedule)
        {
            var type = Type.GetType(schedule.TYPE) ??
                //ensure that it works fine when only the type name is specified (do not require fully qualified names)
                AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetType(schedule.TYPE))
                .FirstOrDefault(t => t != null);
            if (type == null)
                throw new Exception($"Schedule task ({schedule.TYPE}) cannot by instantiated");
            return JobBuilder
                .Create(type)
                .WithIdentity($"{schedule.NAME}.job", $"Group.{schedule.NAME}")
                .WithDescription(schedule.NAME)
                .Build();
        }

        private static ITrigger CreateTrigger(ScheduleTask schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.NAME}.trigger", $"Group.{schedule.NAME}")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(schedule.SECONDS)   //thời gian lặp
                    .RepeatForever())           //Lặp lại mãi mãi
                .WithDescription(schedule.NAME)
                .Build();
        }
        #endregion Properties
    }
}
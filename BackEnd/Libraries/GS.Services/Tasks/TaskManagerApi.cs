using GS.Core.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.Tasks
{
	public partial class TaskManagerApi
    {
		#region Fields

		private GSConfig _gsConfig { get; set; }

		#endregion Fields

		#region Ctor

		private TaskManagerApi()
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

			

			#region Job quét tài sản đồng bộ sang kho

			//IJobDetail jobDongBoTaiSanKho = JobBuilder.Create<DongBoTaiSanKhoTask>()
			//   .WithIdentity("DongBoTaiSanKhoTask", "DongBoTaiSanKho")
			//   .Build();

			//ITrigger triggerDongBoTaiSanKho = TriggerBuilder.Create()
			//	.WithIdentity("DongBoTaiSanKhoTaskTrigger", "DongBoTaiSanKho")
			//	.StartNow()
			//	.WithSimpleSchedule(x => x
			//		.WithIntervalInMinutes(2)
			//		.RepeatForever())
			//	.Build();

			//await sched.ScheduleJob(jobDongBoTaiSanKho, triggerDongBoTaiSanKho);

			#endregion
		}

		#endregion Methods

		#region Properties

		/// <summary>
		/// Gets the task manger instance
		/// </summary>
		public static TaskManagerApi Instance { get; } = new TaskManagerApi();

		#endregion Properties
	}
}

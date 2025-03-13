using GS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.CauHinh
{
	public class CauHinhThreadBaoCao:ICauHinh
	{
		public decimal ThreadBaoCao_MaxThreadWaiting { get; set; }
		public decimal ThreadBaoCao_MaxThreadRunning { get; set; }
		public decimal ThreadBaoCao_TimePerCheck { get; set; }
		public decimal ThreadBaoCao_MaxWaitingTimePerThread { get; set; }
		public bool ThreadBaoCao_isContinueWhenOverMaxTime { get; set; }
	}
}

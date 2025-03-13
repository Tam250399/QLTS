using GS.Web.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
	public class CauHinhThreadBaoCaoModel : BaseGSModel, ICauHinhModel
	{
		[UIHint("InputAddon")]
		public decimal ThreadBaoCao_MaxThreadWaiting { get; set; }
		[UIHint("InputAddon")]
		public decimal ThreadBaoCao_MaxThreadRunning { get; set; }
		[UIHint("InputAddon")]
		public decimal ThreadBaoCao_TimePerCheck { get; set; }
		[UIHint("InputAddon")]
		public decimal ThreadBaoCao_MaxWaitingTimePerThread { get; set; }
		public bool ThreadBaoCao_isContinueWhenOverMaxTime { get; set; }
	}
}
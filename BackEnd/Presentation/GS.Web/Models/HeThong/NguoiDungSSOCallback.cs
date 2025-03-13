using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.HeThong
{
	public class NguoiDungSSOCallback
	{
		public NguoiDungSSOCallback()
		{
			profile = new ProfileSSOCallback();
		}
		public string id_token { get; set; }
		public string session_state { get; set; }
		public string access_token { get; set; }
		public string refresh_token { get; set; }
		public string token_type { get; set; }
		public string scope { get; set; }
		public ProfileSSOCallback profile { get; set; }
		public decimal? expires_at { get; set; }
	}
	public class ProfileSSOCallback
	{
		public string s_hash { get; set; }
		public string sid { get; set; }
		public string sub { get; set; }
		public decimal? auth_time { get; set; }
		public string preferred_username { get; set; }
		public string name { get; set; }
	}

	public class UserSSO
	{
		public string name { get; set; }
	}
}

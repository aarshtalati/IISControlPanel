using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IISControlPanel.Models
{
	public enum AppPoolStatus
	{
		Running = 2,
		Stopped = 4
	}

	public class IISAppPoolRecord
	{
		public string Name { get; set; }
		public string NativeGuid { get; set; }
		public AppPoolStatus Status { get; set; }
	}
}
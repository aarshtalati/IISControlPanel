using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IISControlPanel.Models;

namespace IISControlPanel
{
	public partial class IIS : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Write("Computer Name : " + System.Environment.MachineName + "<br/>");

			if (IsPostBack)
				return;

			LoadAppPools();
		}

		protected void LoadAppPools(object sender = null, EventArgs e = null)
		{
			tblAppPools.DataSource = GetApplicationPools();
			tblAppPools.DataBind();
		}

		public IEnumerable<IISAppPoolRecord> GetApplicationPools()
		{
			DirectoryEntry root = GetDirectoryEntry(@"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools");
			if (root == null)
				return null;

			var myAppPool = Request.ServerVariables["APP_POOL_ID"];

			var appPoolNames = from DirectoryEntry entry in root.Children
							   let status = (AppPoolStatus)(new DirectoryEntry(entry.Path)).InvokeGet("AppPoolState")
							   where
									string.Equals(myAppPool, entry.Name) == false
							   select new IISAppPoolRecord { NativeGuid = entry.NativeGuid, Name = entry.Name, Status = status };
			return appPoolNames;
		}

		private DirectoryEntry GetDirectoryEntry(string path)
		{
			DirectoryEntry root = null;
			try
			{
				root = new DirectoryEntry(path);
			}

			catch (Exception ex)
			{
				Response.Write("Error Getting Root Entry : " + ex.Message + "<br/>");
			}
			return root;
		}

		protected void stopAppPool(object sender, EventArgs e)
		{
			Button btStop = sender as Button;
			if (btStop == null)
				return;

			string appPoolName = btStop.CommandName;
			string appPoolPath = @"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools/" + appPoolName;
			try
			{
				DirectoryEntry w3svc = new DirectoryEntry(appPoolPath);
				var v = w3svc.Name;
				w3svc.Invoke("Stop", null);
				LoadAppPools();
			}
			catch (Exception ex)
			{
				Response.Write(ex.ToString());
			}
		}

		protected void startAppPool(object sender, EventArgs e)
		{
			Button btStart = sender as Button;
			if (btStart == null)
				return;

			string appPoolName = btStart.CommandName;
			string appPoolPath = @"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools/" + appPoolName;
			try
			{
				DirectoryEntry w3svc = new DirectoryEntry(appPoolPath);
				w3svc.Invoke("Start", null);
				LoadAppPools();
			}
			catch (Exception ex)
			{
				Response.Write(ex.ToString());
			}
		}

		protected void recycleAppPool(object sender, EventArgs e)
		{
			Button btStart = sender as Button;
			if (btStart == null)
				return;

			string appPoolName = btStart.CommandName;
			string appPoolPath = @"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools/" + appPoolName;
			try
			{
				DirectoryEntry w3svc = new DirectoryEntry(appPoolPath);
				w3svc.Invoke("Recycle");
				LoadAppPools();
			}
			catch (Exception ex)
			{
				Response.Write(ex.ToString());
			}
		}
	}
}
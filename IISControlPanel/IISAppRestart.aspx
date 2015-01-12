<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IISAppRestart.aspx.cs" Inherits="IISControlPanel.IIS" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Restart Application Pool</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:Repeater ID="tblAppPools" runat="server">
				<HeaderTemplate>
					<table border="1" width="100%">
						<thead>
							<tr>
								<th>Native GUID</th>
								<th>Name</th>
								<th>Status</th>
								<th>Action
									<asp:Button ID="reload" runat="server" Text="Refresh" OnClick="LoadAppPools" />
								</th>
							</tr>
						</thead>
						<tbody>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td><%#Eval("NativeGuid")%></td>
						<td><%#Eval("Name")%></td>
						<td><%#Eval("Status")%></td>
						<td>
							<asp:Button ID="btStart" runat="server" Visible='<%# Eval("Status").ToString() == "Stopped" %>' CommandName='<%# Eval("Name") %>' OnClick="startAppPool" Text="Start" />
							<asp:Button ID="btStop" runat="server" Visible='<%# Eval("Status").ToString() == "Running" %>' CommandName='<%# Eval("Name") %>' OnClick="stopAppPool" Text="Stop" />
							<asp:Button ID="btRecycle" runat="server" Visible='<%# Eval("Status").ToString() == "Running" %>' CommandName='<%# Eval("Name") %>' OnClick="recycleAppPool" Text="Recycle" />
						</td>
					</tr>
				</ItemTemplate>
				<FooterTemplate>
					</tbody>
					<tfoot>
						<tr>
							<td colspan="4">
								<a href="Index.aspx">Home</a>
							</td>
						</tr>
					</tfoot>
				</FooterTemplate>
			</asp:Repeater>

			<label><b>Status:</b></label><br />
			<asp:Label ID="lblStatus" runat="server" /><br />

		</div>
	</form>
</body>
</html>

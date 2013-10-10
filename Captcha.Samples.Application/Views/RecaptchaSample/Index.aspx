<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Layout.master" AutoEventWireup="false" CodeBehind="Index.aspx.cs" Inherits="Captcha.Samples.Application.Views.RecaptchaSample.Index" %>
<%@ Register TagPrefix="UserControls" TagName="Recaptcha" Src="~/Views/Shared/Recaptcha.ascx" %>
<asp:Content ContentPlaceHolderID="titleContent" runat="server">Captcha samples - Recaptcha</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" runat="server">
	<h1>Recaptcha</h1>
	<p><a href="http://www.google.com/recaptcha">http://www.google.com/recaptcha</a></p>
	<p><a href="https://developers.google.com/recaptcha/">https://developers.google.com/recaptcha/</a></p>
	<p>You can customize the display of the captcha by changing the theme. You can also change the language for the captcha by changing the language up to the right.</p>
	<asp:PlaceHolder id="confirmationPlaceHolder" Visible="<%# this.Model.Confirm %>" runat="server">
		<div class="alert alert-success">
			<h2>OK</h2>
			<p>Valideringen lyckades</p>
		</div>	
	</asp:PlaceHolder>
	<asp:Repeater id="validationResultRepeater" DataSource="<%# this.Model.ValidationMessages %>" runat="server">
		<HeaderTemplate><div class="alert alert-error">
			<h2>Fel</h2>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li><%# Container.DataItem %></li>
		</ItemTemplate>
		<FooterTemplate></ul></div></FooterTemplate>
	</asp:Repeater>
	<form runat="server">
		<asp:DropDownList id="themeDropDownList" AutoPostBack="true" DataSource="<%# this.Model.Themes %>" runat="server" />
		<UserControls:Recaptcha id="recaptchaControl" runat="server" />
		<asp:Button Text="Validate" OnClick="OnSubmit" runat="server" />
	</form>
</asp:Content>

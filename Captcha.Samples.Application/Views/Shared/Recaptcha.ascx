<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Recaptcha.ascx.cs" Inherits="Captcha.Samples.Application.Views.Shared.Recaptcha" %>
<script type="text/javascript">
	var RecaptchaOptions = { lang : '<%# this.Model.Culture.Name %>', theme : '<%# this.Model.Theme.ToString().ToLowerInvariant() %>', tabindex : 0 };
</script>
<script type="text/javascript" src="//www.google.com/recaptcha/api/challenge?k=<%# this.Model.PublicKey %>&hl=<%# this.Model.Culture.Name %>"></script>
<noscript>
	<iframe src="//www.google.com/recaptcha/api/noscript?k=<%# this.Model.PublicKey %>&hl=<%# this.Model.Culture.Name %>" width="500" height="300" frameborder="0">
	</iframe><br /><textarea name="recaptcha_challenge_field" rows="3" cols="40"></textarea><input name="recaptcha_response_field" value="manual_challenge" type="hidden" />
</noscript>

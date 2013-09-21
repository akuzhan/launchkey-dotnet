<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="LaunchKey.Examples.AspNetWebForms.Controls.LoginControl" %>

<div style="text-align: center">
    <div>
        <label for="launchkeyUsername">LaunchKey Username</label>
        <input type="text" id="launchkeyUsername" class="launchkeyUsername" size="30" autofocus />
    </div>
    <div>
        <a href="javascript:void(null)" class="launchkeyLogin" id="launchkeyLoginButton"><span class="icon">&nbsp;</span><span class="text">Log in</span></a>
    </div>
    

    </div>
<script type="text/javascript">

    $(function () {
        var $lkLoginButton = $("#launchkeyLoginButton");
        var $lkLoginText = $("#launchkeyLoginButton .text");
        var $lkUsernameTextBox = $("#launchkeyUsername");

        var fnSubmitAuth = function () {
            if ($.trim($lkUsernameTextBox.val()).length > 0) {
                $lkUsernameTextBox.attr("disabled", "disabled");
                $lkLoginText.text("Contacting LaunchKey ... ");
                $.ajax({
                    type: "POST",
                    url: "<%=Request.ApplicationPath.TrimEnd('/')%>/LaunchKeyJsonWebService.asmx/Login",
                    data: JSON.stringify({ username: $lkUsernameTextBox.val() }),
                    dataType: "JSON",
                    contentType: 'application/json; charset=utf-8',
                    success: function (authResult) {
                        if (authResult.hasOwnProperty("d"))
                            authResult = authResult.d;

                        if (authResult.Successful) {
                            $lkLoginText.text("Launch Request Sent ... ");
                            var pollWait = false;
                            var pollCount = 0;
                            var intHandle = window.setInterval(function () {
                                if (!pollWait) {
                                    pollWait = true;
                                    pollCount++;
                                    $lkLoginText.text("Waiting ... ");

                                    // check if login has been processed
                                    $.ajax({
                                        type: "POST",
                                        url: "<%=Request.ApplicationPath.TrimEnd('/')%>/LaunchKeyJsonWebService.asmx/LoginPoll",
                                        data: JSON.stringify({ authRequest: authResult.AuthRequest }),
                                        dataType: "JSON",
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (result) {
                                            if (result.hasOwnProperty("d"))
                                                result = result.d;

                                            if (result.Successful) {
                                                if (result.Waiting) {
                                                    pollWait = false;
                                                } else if (result.Accepted) {
                                                    window.clearTimeout(intHandle);
                                                    $lkLoginText.text("Device responded");
                                                    window.setTimeout(function () { window.location.href = result.RedirectUrl; }, 1000, null);
                                                } else {
                                                    window.clearTimeout(intHandle);
                                                    $lkLoginText.text("Device denied access :(");
                                                    window.setTimeout(function () {
                                                        $lkLoginText.text("Log in");
                                                        $lkUsernameTextBox.removeAttr("disabled");
                                                    }, 3000, null);
                                                }

                                            } else {
                                                // retry
                                                pollWait = false;
                                            }

                                        }
                                    });
                                }
                            }, 2000);
                        } else {
                            // auth request failed.
                            $lkLoginText.text("Login Request Failed :(");
                            window.setTimeout(function () {
                                $lkLoginText.text("Log in");
                                $lkUsernameTextBox.removeAttr("disabled");
                            }, 3000, null);
                        }
                    }
                });
            } else {
                $lkUsernameTextBox.focus();

            }
        }



        $lkLoginButton.click(fnSubmitAuth);
        $lkUsernameTextBox.keydown(function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
            }
        });
        $lkUsernameTextBox.keyup(function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                fnSubmitAuth();
            }
        });
    });
    </script>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace LaunchKey.Examples.AspMvc.Models
{
	public class LoginModel
	{
		public string LaunchKeyUserName { get; set; }
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}


	public class LoginNewUserModel
	{
		[HiddenInput]
		public string AuthRequest { get; set; }

		[StringLength(30)]
		[Required]
		[Display(Name = "Hi! What should we call you?")]
		public string FriendlyName { get; set; }
	}
}

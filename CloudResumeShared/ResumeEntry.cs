#nullable disable
using System;

namespace CloudResumeShared{
	public class ResumeEntry{
		public string Name {get;set;}
		public string Description {get;set;}
		public DateTime StartDate {get;set;}
		public DateTime EndDate {get;set;}
		public string Postion {get;set;}
		public string Url {get;set;}
		public string ImageUrl {get;set;}
		public string ImageAlt {get;set;}
	}
}

namespace Shared.Dtos;

public class GoogleAuthResults
{
	public record GetTokenResult
	{
		public string access_token { get; set; } = string.Empty;
		public int expires_in { get; set; }
		public string scope { get; set; } = string.Empty;
		public string token_type { get; set; } = string.Empty;
		public string id_token { get; set; } = string.Empty;
	}

	public record GetUserResult
	{
		public string id { get; set; } = string.Empty;
		public string email { get; set; } = string.Empty;
		public bool verified_email { get; set; }
		public string name { get; set; } = string.Empty;
		public DateTime birthday { get; set; } = DateTime.UtcNow;
		public string given_name { get; set; } = String.Empty;
		public string family_name { get; set; } = String.Empty;
		public string picture { get; set; } = String.Empty;
		public string locale { get; set; } = String.Empty;
	}
}
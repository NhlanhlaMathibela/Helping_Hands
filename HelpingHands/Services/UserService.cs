namespace HelpingHands.Services
{
	public class UserService
	{

		private readonly IHttpContextAccessor _httpContextAccessor;
		private ISession _session => _httpContextAccessor.HttpContext.Session;

		public UserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public void TestGet()
		{
			var userId = _session.GetString("UserId");
		
		}
	}
}

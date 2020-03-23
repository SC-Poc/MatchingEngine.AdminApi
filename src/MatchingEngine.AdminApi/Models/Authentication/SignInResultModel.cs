namespace MatchingEngine.AdminApi.Models.Authentication
{
    /// <summary>
    /// Represents result of sing in.
    /// </summary>
    public class SignInResultModel
    {
        /// <summary>
        /// The admin authentication token.
        /// </summary>
        public string Token { get; set; }
    } 
}

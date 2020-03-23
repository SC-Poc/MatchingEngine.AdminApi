namespace MatchingEngine.AdminApi.Models.Authentication
{
    /// <summary>
    /// Represents credentials to sing in.
    /// </summary>
    public class SignInModel
    {
        /// <summary>
        /// The email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password. 
        /// </summary>
        public string Password { get; set; }
    }
}

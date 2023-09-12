using System.ComponentModel.DataAnnotations;

namespace Wedding.Shared.Models
{
    /// <summary>
    /// Account type
    /// Mainly handle login form and redirect to specific form
    /// </summary>
    public class Account : IEquatable<Account>
    {
        [Required(ErrorMessage = "Identifiant obligatoire")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Mot de passe obligatoire")]
        public string? Secret { get; set; }

        /// <summary>
        /// Override Equals for easier compare
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Account? other)
        {
            if (this is null) return false;
            if (Id != null && Secret != null)
            {
                return Id.Equals(other?.Id) && Secret.Equals(other?.Secret);
            }
            else
            {
                return false;
            }           
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Wedding.Shared.Models
{
    /// <summary>
    /// Type de compte 
    /// Utilisé se connecter et permettre d'accéder au formulaire dédié
    /// </summary>
    public class Account : IEquatable<Account>
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Secret { get; set; }

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
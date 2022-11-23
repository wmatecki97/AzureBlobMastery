using System.ComponentModel.DataAnnotations;

namespace AzureBlobMastery.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}

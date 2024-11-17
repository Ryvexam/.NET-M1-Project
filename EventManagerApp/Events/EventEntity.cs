using System.ComponentModel.DataAnnotations;

namespace MonApp.Events
{
    public class EventEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Le titre doit contenir entre 5 et 40 caractères")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "L'adresse est obligatoire")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Le code postal est obligatoire")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le code postal doit être au format français (5 chiffres)")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire")]
        public string City { get; set; }

        [Required(ErrorMessage = "La date et heure de début sont obligatoires")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "La durée ou date de fin est obligatoire")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Le nombre maximum d'inscriptions est obligatoire")]
        [Range(1, 100000, ErrorMessage = "Le nombre maximum d'inscriptions doit être entre 1 et 100 000")]
        public int MaxRegistrations { get; set; }

        [Required(ErrorMessage = "L'état est obligatoire")]
        public EventStatus Status { get; set; }

        public int? MinimumAge { get; set; }

        public List<string>? Tags { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public string? CreatedBy { get; set; }

    }

    public enum EventStatus
    {
        Draft,
        Published,
        Cancelled
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MonApp.Events
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly DbContext _dbContext;
        private DbSet<EventEntity> EventSet => _dbContext.Set<EventEntity>();

        public EventsController(ILogger<EventsController> logger, DbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] CreateEventRequest request)
        {
            if (request.StartDateTime >= request.EndDateTime)
            {
                return Problem(
                    detail: "La date de fin doit être postérieure à la date de début",
                    statusCode: 400);
            }

            var isDuplicateEventTitle = EventSet.Any(e => e.Title == request.Title);
            if (isDuplicateEventTitle)
            {
                return Problem(
                    detail: $"Un événement avec le titre '{request.Title}' existe déjà",
                    statusCode: 400);
            }

            var newEvent = new EventEntity
            {
                Title = request.Title,
                Description = request.Description,
                Address = request.Address,
                PostalCode = request.PostalCode,
                City = request.City,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                MaxRegistrations = request.MaxRegistrations,
                Status = EventStatus.Draft,
                MinimumAge = request.MinimumAge,
                Tags = request.Tags,
                ImageUrl = request.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = User.Identity.Name
            };

            Validator.ValidateObject(newEvent, new ValidationContext(newEvent), true);

            EventSet.Add(newEvent);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Événement '{request.Title}' créé avec une capacité de {request.MaxRegistrations} personnes");

            return Ok(newEvent.Id);
        }

        [HttpGet]
        public IList<EventResponse> GetAllEvents()
        {
            return EventSet
                .Select(e => MapToResponse(e))
                .ToList();
        }

        private static EventResponse MapToResponse(EventEntity entity)
        {
            return new EventResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Address = entity.Address,
                PostalCode = entity.PostalCode,
                City = entity.City,
                StartDateTime = entity.StartDateTime,
                EndDateTime = entity.EndDateTime,
                MaxRegistrations = entity.MaxRegistrations,
                Status = entity.Status,
                MinimumAge = entity.MinimumAge,
                Tags = entity.Tags,
                ImageUrl = entity.ImageUrl,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy
            };
        }
    }

    public class CreateEventRequest
    {
        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Le titre doit contenir entre 5 et 40 caractères")]
        public string Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{5}$")]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        [Range(1, 100000)]
        public int MaxRegistrations { get; set; }

        public int? MinimumAge { get; set; }
        public List<string>? Tags { get; set; }
        public string? ImageUrl { get; set; }
        
        [Required]
        public string? CreatedBy { get; set; }

    }

    public class EventResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MaxRegistrations { get; set; }
        public EventStatus Status { get; set; }
        public int? MinimumAge { get; set; }
        public List<string>? Tags { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string? CreatedBy { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Event title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Event date is required")]
        public DateTime Date { get; set; } = DateTime.Now.AddDays(7);
        
        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string Location { get; set; } = string.Empty;
        
        [Range(1, 10000, ErrorMessage = "Max attendees must be between 1 and 10,000")]
        public int MaxAttendees { get; set; } = 50;
        
        public int CurrentAttendees { get; set; } = 0;
        
        public List<string> RegisteredUsers { get; set; } = new();
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
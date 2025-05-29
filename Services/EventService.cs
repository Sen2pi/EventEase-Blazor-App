using EventEase.Models;

namespace EventEase.Services
{
    public class EventService
    {
        private List<EventModel> events = new();
        private int nextId = 1;
        
        public EventService()
        {
            // Initialize with sample data
            SeedData();
        }
        
        public Task<List<EventModel>> GetEventsAsync()
        {
            return Task.FromResult(events.Where(e => e.IsActive).ToList());
        }
        
        public Task<EventModel?> GetEventByIdAsync(int id)
        {
            var eventItem = events.FirstOrDefault(e => e.Id == id && e.IsActive);
            return Task.FromResult(eventItem);
        }
        
        public Task<EventModel> CreateEventAsync(EventModel eventModel)
        {
            eventModel.Id = nextId++;
            eventModel.CreatedAt = DateTime.Now;
            events.Add(eventModel);
            return Task.FromResult(eventModel);
        }
        
        public Task<EventModel> UpdateEventAsync(EventModel eventModel)
        {
            var existingEvent = events.FirstOrDefault(e => e.Id == eventModel.Id);
            if (existingEvent != null)
            {
                var index = events.IndexOf(existingEvent);
                events[index] = eventModel;
            }
            return Task.FromResult(eventModel);
        }
        
        public Task<bool> DeleteEventAsync(int id)
        {
            var eventItem = events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null)
            {
                eventItem.IsActive = false;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        
        public Task<bool> RegisterUserAsync(int eventId, RegistrationModel registration)
        {
            var eventItem = events.FirstOrDefault(e => e.Id == eventId);
            if (eventItem != null && eventItem.CurrentAttendees < eventItem.MaxAttendees)
            {
                eventItem.RegisteredUsers.Add($"{registration.FirstName} {registration.LastName}");
                eventItem.CurrentAttendees++;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        
        private void SeedData()
        {
            events.AddRange(new[]
            {
                new EventModel
                {
                    Id = nextId++,
                    Title = "Tech Conference 2024",
                    Description = "Annual technology conference featuring the latest innovations",
                    Date = DateTime.Now.AddDays(30),
                    Location = "Convention Center, Downtown",
                    MaxAttendees = 500,
                    CurrentAttendees = 127
                },
                new EventModel
                {
                    Id = nextId++,
                    Title = "Startup Networking Event",
                    Description = "Connect with entrepreneurs and investors",
                    Date = DateTime.Now.AddDays(15),
                    Location = "Innovation Hub",
                    MaxAttendees = 100,
                    CurrentAttendees = 45
                }
            });
        }
    }
}

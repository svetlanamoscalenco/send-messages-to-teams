using System.ComponentModel.DataAnnotations;

namespace TeamsMessages.Models
{
    public class MessageViewModel
    {
        [Display(Name = "Channel Name")]
        public string? ChannelName { get; set; }

        [Display(Name = "Message")]
        public string? Message { get; set; }
        
        public string? UserDisplayName { get; set; }

        public string? PostAction { get; set; }
    }
}
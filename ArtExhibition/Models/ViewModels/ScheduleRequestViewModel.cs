using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArtExhibition.Models.ViewModels
{
    public class ScheduleRequestViewModel
    {
        [Required]
        public int RoomId { get; set; }

        public IEnumerable<SelectListItem>? Rooms { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }


}
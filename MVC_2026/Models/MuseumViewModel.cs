using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace MVC2026.Models
{
    public class MuseumViewModel
    {
        public List<SelectListItem> States { get; set; }

        public List<SelectListItem> MuseumCategory { get; set; }

        public int? SelectedCategoryId { get; set; }

        public int? SelectedStateId { get; set; }

    }
}

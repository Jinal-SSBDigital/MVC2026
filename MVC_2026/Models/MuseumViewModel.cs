using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC2026.Models
{
    public class MuseumViewModel
    {
        public List<SelectListItem> States { get; set; }

        public int SelectedStateId { get; set; }

    }
}

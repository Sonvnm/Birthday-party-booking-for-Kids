using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace BirthdayPartyBookingForKids_Client.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.BirthdayPartyBookingForKids_DBContext _context;

        public DetailsModel(BusinessObject.Models.BirthdayPartyBookingForKids_DBContext context)
        {
            _context = context;
        }

      public BusinessObject.Models.User User { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }
            return Page();
        }
    }
}

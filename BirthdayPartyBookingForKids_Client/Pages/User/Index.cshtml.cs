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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.BirthdayPartyBookingForKids_DBContext _context;

        public IndexModel(BusinessObject.Models.BirthdayPartyBookingForKids_DBContext context)
        {
            _context = context;
        }

        public IList<BusinessObject.Models.User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.Role).ToListAsync();
            }
        }
    }
}

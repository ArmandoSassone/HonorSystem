﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;

namespace HonorSystem.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public CreateModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdBoss"] = new SelectList(_context.Bosses, "IdBoss", "IdBoss");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Items == null || Item == null)
            {
                return Page();
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

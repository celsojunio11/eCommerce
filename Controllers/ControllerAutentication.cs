using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCommerce.Models.DataContext;
using eCommerce.Models.Table;

namespace eCommerce.Controllers
{
      public class AutenticationController : Controller
      {
            private readonly eCommerceContext _context;

            public AutenticationController(eCommerceContext context)
            {
                  _context = context;
            }

            // GET: Autentication
            public async Task<IActionResult> Index()
            {
                  var Autentications = await _context.Autentications.Include("IdPessoaNavigation.PessoaFisica")
                      .Include("IdPessoaNavigation.PessoaJuridica")
                      .ToListAsync();
                  return View(Autentications);
            }

            // GET: Autentication/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                  if (id == null)
                  {
                        return NotFound();
                  }

                  var Autentication = await _context.Autentications
                      .Include(c => c.IdPessoaNavigation)
                      .FirstOrDefaultAsync(m => m.IdPessoa == id);
                  if (Autentication == null)
                  {
                        return NotFound();

                  }

                  return View(Autentication);
            }

            // GET: Autentication/Create
            public IActionResult Create()
            {
                  ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "IdPessoa", "Nome");
                  return View();
            }

            // POST: Autentication/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("IdPessoa,IsPreferencial")] Autentication Autentication)
            {
                  if (ModelState.IsValid)
                  {
                        _context.Add(Autentication);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                  }
                  ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "IdPessoaa", "Nome", Autentication.IdPessoa);
                  return View(Autentication);
            }

            // GET: Autentication/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                  if (id == null)
                  {
                        return NotFound();
                  }

                  var Autentication = await _context.Autentications.Include(c => c.IdPessoaNavigation.PessoaFisica)
                      .Include(c => c.IdPessoaNavigation.PessoaJuridica)
                      .SingleAsync(c => c.IdPessoa == id);
                  if (Autentication == null)
                  {
                        return NotFound();
                  }
                  // ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "IdPessoa", "Nome", Autentication.IdPessoa);
                  return View(Autentication);
            }

            // POST: Autentication/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("IdPessoa, IsPreferencial, IdPessoaNavigation,")] Autentication Autentication)
            {
                  if (id != Autentication.IdPessoa)
                  {
                        return NotFound();
                  }

                  if (ModelState.IsValid)
                  {
                        try
                        {
                              _context.Update(Autentication);
                              await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                              if (!AutenticationExists(Autentication.IdPessoa))
                              {
                                    return NotFound();
                              }
                              else
                              {
                                    throw;
                              }
                        }
                        return RedirectToAction(nameof(Index));
                  }
                  ViewData["IdPessoa"] = new SelectList(_context.Pessoas, "IdPessoa", "Nome", Autentication.IdPessoa);
                  return View(Autentication);
            }

            // GET: Autentication/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                  if (id == null)
                  {
                        return NotFound();
                  }

                  var Autentication = await _context.Autentications
                      .Include(c => c.IdPessoaNavigation)
                      .FirstOrDefaultAsync(m => m.IdPessoa == id);
                  if (Autentication == null)
                  {
                        return NotFound();
                  }

                  return View(Autentication);
            }

            // POST: Autentication/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                  var Autentication = await _context.Autentications.FindAsync(id);
                  _context.Autentications.Remove(Autentication);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
            }

            private bool AutenticationExists(int id)
            {
                  return _context.Autentications.Any(e => e.IdPessoa == id);
            }
      }
}
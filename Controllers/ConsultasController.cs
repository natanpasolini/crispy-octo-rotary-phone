using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaConsultasUVV.Data;
using SistemaConsultasUVV.Models;

namespace SistemaConsultasUVV.Controllers;

[Authorize]
public class ConsultasController : Controller
{
    private readonly AppDbContext _context;

    public ConsultasController(AppDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        var claimId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(claimId ?? "0");
    }

    // GET: Consultas
    public async Task<IActionResult> Index()
    {
        int userId = GetCurrentUserId();
        var consultas = await _context.Consultas
            .Where(c => c.UsuarioId == userId)
            .OrderBy(c => c.DataHora)
            .ToListAsync();

        return View(consultas);
    }

    // GET: Consultas/Create
    public IActionResult Create()
    {
        return View(new Consulta { DataHora = DateTime.Now.AddDays(1) });
    }

    // POST: Consultas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Consulta consulta)
    {
        consulta.UsuarioId = GetCurrentUserId();
        ModelState.Remove(nameof(consulta.Usuario));

        if (!ModelState.IsValid)
            return View(consulta);

        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Consulta cadastrada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Consultas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        int userId = GetCurrentUserId();
        var consulta = await _context.Consultas
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == userId);

        if (consulta == null)
            return NotFound();

        return View(consulta);
    }

    // POST: Consultas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Consulta consulta)
    {
        if (id != consulta.Id)
            return NotFound();

        int userId = GetCurrentUserId();

        // Previne edição de consulta pertencente a outro usuário
        var existingConsulta = await _context.Consultas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == userId);

        if (existingConsulta == null)
            return Forbid();

        consulta.UsuarioId = userId;
        ModelState.Remove(nameof(consulta.Usuario));

        if (!ModelState.IsValid)
            return View(consulta);

        _context.Update(consulta);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Consulta atualizada com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    // GET: Consultas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        int userId = GetCurrentUserId();
        var consulta = await _context.Consultas
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == userId);

        if (consulta == null)
            return NotFound();

        return View(consulta);
    }

    // POST: Consultas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        int userId = GetCurrentUserId();
        var consulta = await _context.Consultas
            .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == userId);

        if (consulta != null)
        {
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Consulta cancelada/excluída com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }
}

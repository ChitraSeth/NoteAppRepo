using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesWebApp.Data;
using NotesWebApp.Models;
using NotesWebApp.Models.Entites;
using System.Security.Claims;

namespace NotesWebApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NoteAppDBContext _context;

        public NoteController(NoteAppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Notes = await _context.Notes.ToListAsync();
            return View(Notes);
        }


        [HttpGet]
        public IActionResult AddNote()
        {
            var model = new AddNoteViewModel();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(AddNoteViewModel model)
        {
            var note = new Note
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,

            };

            await _context.Notes.AddAsync(note);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var note = await _context.Notes.FindAsync(id);

            if (note != null)
            {
                var model = new NoteEditModel
                {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    Content = note.Content
                };
                return View(model);
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NoteEditModel model)
        {
            var note = await _context.Notes.FindAsync(model.Id);
            if (note != null)
            {
                note.Title = model.Title;
                note.Description = model.Description;
                note.Content = model.Content;

                _context.Update(note);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note != null)
            {
                _context.Remove(note);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> Search(int id)
        //{

        //        return View();
        //    }


        //    return RedirectToAction("Index");
        //}

        public IActionResult Search(SearchModel srcModel)
        {


        IEnumerable<Note> result = Enumerable.Empty<Note>();

        if (!string.IsNullOrEmpty(srcModel.SearchText))
        {
            result = _context.Notes.Where(n => n.Title.Contains(srcModel.SearchText));
        }

         var sResult = result.Select(n => new Note
            {
                Content = n.Content,
                Description = n.Description,
                Id = n.Id,
                Title = n.Title
            });

            if (sResult == null)
            {
                sResult = Enumerable.Empty<Note>();
            }

            var model = new SearchModel
            {
                
                SearchText = "",
                SearchResult = sResult
            };

            return View(model);


        }
    }

}


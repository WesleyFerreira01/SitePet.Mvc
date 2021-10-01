using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitePet.Mvc.Data;
using SitePet.Mvc.Models;
using SitePet.Mvc.Services.Interfaces;
using SitePet.Mvc.ViewModels;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace SitePet.Mvc.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        private readonly IPetRepository _pet;
        private readonly IMapper _mapper;

        public PetsController(IPetRepository pet,
                              IMapper mapper)
        {
            _pet = pet;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PetViewModel>>(await _pet.MostrarTodos()));
        }

        [AllowAnonymous]
        [Route("caes")]
        public async Task<IActionResult> IndexCaes()
        {
            return View(_mapper.Map<IEnumerable<PetViewModel>>(await _pet.MostrarCaes()));
        }

        [AllowAnonymous]
        [Route("gatos")]
        public async Task<IActionResult> IndexGatos()
        {
            return View(_mapper.Map<IEnumerable<PetViewModel>>(await _pet.MostrarGatos()));
        }



        // GET: Pets/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var petViewModel = await ObterPet(id);

            if (petViewModel == null)
            {
                return NotFound();
            }

            return View(petViewModel);
        }

        public IActionResult UsuarioCadastrado()
        {
            return View();
        }

        // GET: Pets/Create
        [ClaimsAuthorize("Pets", "Criar")]
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetViewModel petViewModel)
        {
            if (!ModelState.IsValid) return View(petViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(petViewModel.ImagemUpload, imgPrefixo))
            {
                return View(petViewModel);
            }

            petViewModel.Imagem = imgPrefixo + petViewModel.ImagemUpload.FileName;


            await _pet.Adicionar(_mapper.Map<Pet>(petViewModel));

            return RedirectToAction(nameof(Index));
        }

        // GET: Pets/Edit/5
       
        public async Task<IActionResult> Edit(int id)
        {
            var pet = await ObterPet(id);

            if (pet == null) return NotFound();

            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PetViewModel petViewModel)
        {
            if (id != petViewModel.Id)
            {
                return NotFound();
            }

            var petsAtualizao = await ObterPet(id);

            petViewModel.Imagem = petsAtualizao.Imagem;

            if (!ModelState.IsValid) return View(petViewModel);

            if (petViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(petViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(petViewModel);
                }

                petsAtualizao.Imagem = imgPrefixo + petViewModel.ImagemUpload.FileName;

            }

            petsAtualizao.Nome = petViewModel.Nome;
            petsAtualizao.Raca = petViewModel.Raca;
            petsAtualizao.Tipo = petViewModel.Tipo;
            petsAtualizao.Genero = petViewModel.Tipo;

            await _pet.Atualizar(_mapper.Map<Pet>(petsAtualizao));

            return RedirectToAction(nameof(Index));



        }

        // GET: Pets/Delete/5
    
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await ObterPet(id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await ObterPet(id);

            if (pet == null)
            {
                return NotFound();
            }

            await _pet.Remover(id);


            return RedirectToAction(nameof(Index));
        }



        private async Task<PetViewModel> ObterPet(int id)
        {
            var pet = _mapper.Map<PetViewModel>(await _pet.MostrarPorId(id));
            return pet;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}

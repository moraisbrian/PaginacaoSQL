using Microsoft.AspNetCore.Mvc;
using PaginacaoSQL.Models;
using PaginacaoSQL.Repositories;
using System.Collections.Generic;

namespace PaginacaoSQL.Controllers
{
    public class ValoresController : Controller
    {
        private readonly IValorRepository _repository;

        public ValoresController(IValorRepository repository)
        {
            _repository = repository;
        }

        private const int Lines = 100;

        public IActionResult Index()
        {
            var pages = _repository.ObterTotalPaginas(Lines);
            var initialPage = _repository.ObterValores(1, Lines);

            SetViewBagValues(pages, 1, 1);

            return View(initialPage);
        }

        public IActionResult GetPage(int page, int sectionStartPage)
        {
            var pages = _repository.ObterTotalPaginas(Lines);
            var content = _repository.ObterValores(page, Lines);

            SetViewBagValues(pages, page, sectionStartPage);
            
            return View("Index", content);
        }

        public IActionResult GetNextSection(int lastSectionPage)
        {
            var sectionStartPage = lastSectionPage + 1;
            var content = _repository.ObterValores(sectionStartPage, Lines);
            var pages = _repository.ObterTotalPaginas(Lines);

            SetViewBagValues(pages, sectionStartPage, sectionStartPage);

            return View("Index", content);
        }

        public IActionResult GetPreviousSection(int sectionStartPage)
        {
            var currentPage = sectionStartPage - 20;
            var content = _repository.ObterValores(currentPage, Lines);
            var pages = _repository.ObterTotalPaginas(Lines);

            SetViewBagValues(pages, currentPage, currentPage);

            return View("Index", content);
        }

        private void SetViewBagValues(int pages, int currentPage, int sectionStartPage)
        {
            ViewBag.Pages = pages;
            ViewBag.CurrentPage = currentPage;
            ViewBag.SectionStartPage = sectionStartPage;
        }
    }
}
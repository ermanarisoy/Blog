using Blog.Web.Interface;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, ISubjectRepository subjectRepository, IPostRepository postRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
            _postRepository = postRepository;
        }
        //public async Task<IActionResult> IndexAsync()
        //{
        //    var subjects = await _subjectRepository.GetSubjects();
        //    return View(subjects);
        //}

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var subjects = await _subjectRepository.GetSubjects();

            if (!String.IsNullOrEmpty(searchString))
            {
                subjects = subjects.Where(s => s.LastName.ToLower().Contains(searchString.ToLower())
                                       || s.FirstName.ToLower().Contains(searchString.ToLower()) || s.Content.ToLower().Contains(searchString.ToLower()) || s.Title.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    subjects = subjects.OrderByDescending(s => s.LastName);
                    break;
                case "conten_desc":
                    subjects = subjects.OrderBy(s => s.Content);
                    break;
                case "date_desc":
                    subjects = subjects.OrderByDescending(s => s.CreationDate);
                    break;
                default:
                    subjects = subjects.OrderByDescending(s => s.CreationDate);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Subject>.CreateAsync(subjects, pageNumber ?? 1, pageSize));
        }

        public IActionResult Subject()
        {
            return View();
        }

        public IActionResult CreateSubject(Subject subject)
        {
            var res = _subjectRepository.CreateSubject(subject).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Post(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public IActionResult CreatePost(Post post)
        {
            var res = _postRepository.CreatePost(post).Result;
            return RedirectToAction("Index");
        }

        public IActionResult Posts(string id)
        {
            ViewBag.Id = id;
            var posts = _postRepository.GetPost(id).Result;
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
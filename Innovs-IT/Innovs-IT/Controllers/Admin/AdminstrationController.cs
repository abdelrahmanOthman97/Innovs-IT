using EM.Models;
using EM.ViewModal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminstrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminstrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;

        }

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<IdentityRole> Allroles = roleManager.Roles;
            return View(Allroles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(createRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {

                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleViewModel.RoleName
                }
                ;
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {

                    return RedirectToAction("index", "Adminstration");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(createRoleViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.error = $"Role wit Id{Id} not Found";

                return View("notFound");
            }
            var model = new editRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,



            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);

                }
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditRole(editRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.error = $"Role with Id{model.Id} not Found";

                return View("notFound");
            }

            if (ModelState.IsValid)
            {

                role.Name = model.RoleName;

                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("index", "Adminstration");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string Id)
        {
            ViewBag.roleId = Id;


            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.error = $"Role wit Id{Id} not Found";

                return View("notFound");
            }
            var model = new List<userRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userrolviewmodel = new userRoleViewModel()
                {
                    UserId = user.Id.ToString(),
                    UserName = user.UserName


                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {

                    userrolviewmodel.IsSelected = true;
                }
                else
                {
                    userrolviewmodel.IsSelected = false;
                }
                model.Add(userrolviewmodel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(string id, List<userRoleViewModel> model)
        {

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.error = $"Role wit Id{id} not Found";

                return View("notFound");
            }

            foreach (var userrole in model )
            {
                var user = await userManager.FindByIdAsync(userrole.UserId);
                IdentityResult result = null;
                if (userrole.IsSelected && !await userManager.IsInRoleAsync(user, role.Name))
                {
                    

                result =await    userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!userrole.IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                        {

                    result = await userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {

                    continue;
                }
              
            }
          

            return RedirectToAction("EditRole",new { Id=id});
        }

    }

}
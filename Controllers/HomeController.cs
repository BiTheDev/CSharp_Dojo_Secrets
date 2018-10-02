using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoSecrets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DojoSecrets.Controllers
{
    public class HomeController : Controller
    {
        private SecretContext _context;
        
            public HomeController(SecretContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                return View("index");
            }
            [HttpPost("RegisterProcess")]
            public IActionResult Register(RegisterViewModel user){
                    if(ModelState.IsValid){
                        var userList = _context.users.Where(p => p.email== user.register_email).FirstOrDefault();
                            if(userList != null){
                                if(user.register_email == userList.email){
                                    ModelState.AddModelError("register_email", "email exists");
                                    return View("index");
                                }
                        }
                                           
                    PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                    user.register_password = Hasher.HashPassword(user, user.register_password);
                    users User = new users(){
                        first_name = user.first_name,
                        last_name = user.last_name,
                        email = user.register_email,
                        password = user.register_password,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    _context.Add(User);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("Id", (int)User.id);
                    return RedirectToAction("Secrets");
                }else{
                    return View("index");
                }
            }

            [HttpPost("LoginProcess")]
            public IActionResult Login(LoginViewModel User){
                if(ModelState.IsValid){
                    List<users> users = _context.users.Where(p => p.email== User.login_Email).ToList();
                    foreach (var user in users)
                    {
                        if(user != null && User.login_Password != null)
                            {
                                var Hasher = new PasswordHasher<users>();
                                if( 0 !=Hasher.VerifyHashedPassword(user, user.password, User.login_Password)){
                                    HttpContext.Session.SetInt32("Id", (int)user.id);
                                    int? id = HttpContext.Session.GetInt32("Id");

                                return RedirectToAction("Secrets");
                            }
                        }else{
                            ModelState.AddModelError("login_Email", "not a vaild email");
                            return View("index");
                        }
                    }       
                }
                return View("index");
            }

            [HttpPost("CreatSecret")]
            public IActionResult Create(string secret){
                int? id = HttpContext.Session.GetInt32("Id");
                secrets newSecrets = new secrets(){
                    usersid = (int)id,
                    secret = secret,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now

                };
                _context.Add(newSecrets);
                _context.SaveChanges();
                return RedirectToAction("Secrets");
            }

            [HttpPost("delete")]
            public IActionResult Delete(){
                int? id = HttpContext.Session.GetInt32("Id");     
                return RedirectToAction("Secrets");
            }

            [HttpPost("like")]
            public IActionResult like(int secretid){
                int? id = HttpContext.Session.GetInt32("Id");
                likes likesecret = new likes{
                    usersid = (int)id,
                    secretsid = secretid

                };
                _context.Add(likesecret);
                _context.SaveChanges();
                return RedirectToAction("Secrets");
            }

            [HttpGet("SecretsWall")]

            public IActionResult Secrets(){
                List<secrets> allSecret = _context.secrets.Include(x=>x.like).OrderByDescending(p=>p.created_at).Take(5).ToList();
                int? id = HttpContext.Session.GetInt32("Id");
                users user = _context.users.Where(x=>x.id == (int)id).FirstOrDefault();     
                ViewBag.allSecret = allSecret;
                ViewBag.user = user;
                return View("Secret");
            }


            [HttpGet("topsecret")]
            public IActionResult TopSecret(){
                // List<likes> topsecret = _context.likes.Include(x=>x.secret).OrderByDescending(p=>p.like).ToList();
                List<secrets> topsecret = _context.secrets.Include(x=>x.like).OrderByDescending(x=>x.like.Count).ToList();
                int? id = HttpContext.Session.GetInt32("Id"); 
                ViewBag.id = (int)id;
                ViewBag.topsecret = topsecret;
                return View("topsecret");
            }


            [HttpGet("logout")]
            public IActionResult logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
       
    }
}

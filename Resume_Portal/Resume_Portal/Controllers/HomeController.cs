using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resume_Portal.Models;
using Microsoft.AspNetCore.WebUtilities;
using Resum_Portal.Models;
using Microsoft.AspNetCore.Identity.UI;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Session;
using static iTextSharp.text.pdf.PdfDocument;
using System.Collections;

namespace Resume_Portal.Controllers
{
   
    public class HomeController : Controller
    {
        const string UserMail = "_UserMail";
        const string otp = "_otp";
        private readonly PdfSearchService _pdfSearchService;
        private readonly ILogger<HomeController> _logger;

        private readonly ResumePortalArivaniContext db;

        public HomeController(ResumePortalArivaniContext context,ILogger<HomeController> logger)
        {
            _logger = logger;
            db = context;
            _pdfSearchService = new PdfSearchService(new ResumePortalArivaniContext());

        }

        [Authorize(Roles ="Admin")]
        // GET: Home
        public async Task<IActionResult> Index()
        {
            return View(await db.TblDesignations.ToListAsync());
        }
        [HttpPost]
       [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Email,Name,Mobile,Gender,Designation,DateOfJoining,Photo,Address,Password,Role,Status,CreatedBy")] TblUser TblUser, IFormFile Photo)
        {
            if (TblUser.Email != null && TblUser.Name != null && TblUser.Gender != null && TblUser.Designation != null && TblUser.DateOfJoining != null && Photo != null && TblUser.Address != null && TblUser.Password != null &&TblUser.CreatedBy != null)
            {
                if (ModelState.IsValid)
                {
                    // Check if email already exists
                    if (db.TblUsers.Any(u => u.Email == TblUser.Email))
                    {
                        ModelState.AddModelError("Email", "Email address already exists.");
                        ViewBag.msg = "Email address already exists.";
                        return View();
                    }

                    // Additional validation checks
                    if (string.IsNullOrEmpty(TblUser.Email) || TblUser.Email.IndexOf("@") <= 0 || !TblUser.Email.Contains("."))
                    {
                        ViewBag.msg = "Invalid Email";
                        return View();
                    }
                    if (TblUser.Mobile.HasValue)
                    {
                        if ((TblUser.Mobile.ToString().Length != 10 || (TblUser.Mobile.ToString()[0] != '7') && (TblUser.Mobile.ToString()[0] != '8') && (TblUser.Mobile.ToString()[0] != '9')))
                        {
                            ViewBag.msg = "Invalid Mobile Number";
                            return View();
                        }
                    }
                    if (TblUser.Password.Length < 8)
                    {
                        ViewBag.msg = "Password must be at least 8 characters long";
                        // return View();
                    }

                    // Handle file upload
                    if (Photo != null && Photo.Length > 0)
                    {
                        var filename = Path.GetFileName(Photo.FileName);
                        var uploadfolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile");
                        var path = Path.Combine(uploadfolder,filename);
                            using (var stream = System.IO.File.Create(path))
                            {
                                await Photo.CopyToAsync(stream);
                            }
                            TblUser.Photo = filename;
                    }

                    // Add user to database
                    //TblUser.CreatedBy = ; // Assuming CreatedBy is set to current user
                    db.TblUsers.Add(TblUser);
                    db.SaveChanges();

                    ViewBag.msg = "Record Added";
                    return RedirectToAction("Users");

                }
                else
                {
                    // Model validation failed
                    ViewBag.msg = "Invalid data submitted";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "All filled Required";
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            return View(await db.TblUsers.ToListAsync());
        }
        [Authorize]
        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUser = await db.TblUsers.FindAsync(id);
            var designation = db.TblDesignations.ToList();
            var viewModel = new DashBoardModel
            {
                Users = null, // No need to populate this for non-admin
                Resumes = null,
                Designations = designation,
                UserData = tblUser,
                Resumesdata = null

            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Email,Name,Mobile,Gender,Designation,DateOfJoining,Photo,Address,Password,Role,Status,CreatedBy")] TblUser TblUser, IFormFile Photo)
        {
            if (TblUser.Email != null && TblUser.Name != null && TblUser.Gender != null && TblUser.Designation != null && TblUser.DateOfJoining != null && Photo != null && TblUser.Address != null && TblUser.Password != null && TblUser.CreatedBy != null)
            {
                if (ModelState.IsValid)
                {
                   
                    // Additional validation checks
                    if (string.IsNullOrEmpty(TblUser.Email) || TblUser.Email.IndexOf("@") <= 0 || !TblUser.Email.Contains("."))
                    {
                        ViewBag.msg = "Invalid Email";
                        return View();
                    }
                    if (TblUser.Mobile.HasValue)
                    {
                        if ((TblUser.Mobile.ToString().Length != 10 || (TblUser.Mobile.ToString()[0] != '7') && (TblUser.Mobile.ToString()[0] != '8') && (TblUser.Mobile.ToString()[0] != '9')))
                        {
                            ViewBag.msg = "Invalid Mobile Number";
                            return View();
                        }
                    }
                    if (TblUser.Password.Length < 8)
                    {
                        ViewBag.msg = "Password must be at least 8 characters long";
                        // return View();
                    }

                    // Handle file upload
                    if (Photo != null && Photo.Length > 0)
                    {
                        var filename = Path.GetFileName(Photo.FileName);
                        var uploadfolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile");
                        var path = Path.Combine(uploadfolder, filename);
                        using (var stream = System.IO.File.Create(path))
                        {
                            await Photo.CopyToAsync(stream);
                        }
                        TblUser.Photo = filename;
                    }

                    // Add user to database
                    //TblUser.CreatedBy = ; // Assuming CreatedBy is set to current user
                    db.TblUsers.Update(TblUser);
                    db.SaveChanges();

                    ViewBag.msg = "Record Updated";
                    return RedirectToAction("Users");

                }
                else
                {
                    // Model validation failed
                    ViewBag.msg = "Invalid data submitted";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "All filled Required";
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUser = await db.TblUsers
                .FirstOrDefaultAsync(m => m.Email == id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return View(tblUser);
        }
        [Authorize(Roles = "Admin")]
        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tblUser = await db.TblUsers.FindAsync(id);
            if (tblUser != null)
            {
                db.TblUsers.Remove(tblUser);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatusUser(string id)
        {
            //string decodedEmail = HttpUtility.UrlDecode(Request.QueryString["id"]);
            var tblUser =await db.TblUsers.FindAsync(id);
            if (tblUser.Status.Equals(true))
            {
                tblUser.Status = false;
            }
            else
            {
                tblUser.Status = true;
            }
            db.Entry(tblUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        public async Task<IActionResult> ChangeStatus(string? id)
        {
            //string decodedEmail = HttpUtility.UrlDecode(Request.QueryString["id"]);
            TblDesignation TblDesignation = db.TblDesignations.Find(id);
            if (TblDesignation.Status.Equals(true))
            {
                TblDesignation.Status = false;
            }
            else
            {
                TblDesignation.Status = true;
            }
            db.Entry(TblDesignation).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Designation");
        }
       
        public ActionResult ChangeStatusResume(int? id)
        {
            TblResume tbl_resume = db.TblResumes.Find(id);
            if (tbl_resume.Status.Equals(true))
            {
                tbl_resume.Status = false;
            }
            else
            {
                tbl_resume.Status = true;
            }
            db.Entry(tbl_resume).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserData");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllResumeData(string? SearchString)
        {
            if (SearchString != null)
            {
                
                var searchResults = _pdfSearchService.SearchPdfDocuments(SearchString);
                var viewModel = new DashBoardModel
                {
                    Users = null, // No need to populate this for non-admin
                    Resumes = null,
                    Designations = null,
                    UserData = null,
                    Resumesdata = searchResults
                };

                return View(viewModel); // Return search results view
               
                
            }
            else
            {
                var resume = db.TblResumes.ToList();
                var viewModel = new DashBoardModel
                {
                    Users = null, // No need to populate this for non-admin
                    Resumes = null,
                    Designations = null,
                    UserData = null,
                    Resumesdata = resume
                };

                return View(viewModel);
            }
        }
        private bool TblUserExists(string id)
        {
            return db.TblUsers.Any(e => e.Email == id);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Designation()
        {
            return View(await db.TblDesignations.ToListAsync());
        }
        public ActionResult CreateDesignation()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDesignation([Bind("Designation,Status")] TblDesignation tbl_designation)
        {
            if (tbl_designation.Designation != null)
            {
                if (ModelState.IsValid)
                {

                    if (db.TblDesignations.Any(u => u.Designation == tbl_designation.Designation))
                    {
                        ModelState.AddModelError("Designation", "Designation address already exists.");
                        ViewBag.msg = "Designation address already exists.";
                        return View();
                    }
                    db.TblDesignations.Add(tbl_designation);
                    db.SaveChanges();
                    ViewBag.msg = "Record Added";
                    Task.WaitAll(Task.Delay(2000));
                    // System.Threading.Thread.Sleep(2000);
                    return RedirectToAction("Designation");
                }
                else
                {
                    ViewBag.msg = "Something Missing ! Try Again..";
                    return View();
                }

            }
            ViewBag.msg = "All Filled Required";
            return View();

        }
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            //ViewData["ReturnUrl"] = returnurl;
            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(string email, string Password)
        {
            
            if (email != null && Password != null)
            {

                var user = db.TblUsers.FirstOrDefault(u => u.Email == email && u.Password == Password);
                if (user != null)
                {
                    if (user.Status != false)
                    {
                        var claims = new List<Claim>
                        {
                          new Claim(ClaimTypes.Name, email),
                          new Claim(ClaimTypes.Role, user.Role),
                          new Claim("FullName", user.Name),
                          new Claim("ProfilePic", user.Photo),
                          new Claim("RoleName", user.Role),

                        };
                        var claimidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authproprties = new AuthenticationProperties
                        {
                            IsPersistent = false
                        };
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimidentity), authproprties);
                        ViewBag.error = "Please Wait.....";
                        if (user.Role.Equals("Admin"))
                        {
                            // ViewBag.error = "Admin Login";
                            // System.Threading.Thread.Sleep(3000);
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            // ViewBag.error = "User Login";
                            // System.Threading.Thread.Sleep(3000);
                            return RedirectToAction("Dashboard");
                        }
                        // return RedirectToAction("Create");
                    }
                    else
                    {
                        ViewBag.error = "You Are Blocked Admin Side";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Username or Password Mismatched";
                    return View();
                }
                // tbl_User tbl_User = db.tbl_User.Find(username);
                // return Content("<script>alert('ok');</script>");
            }
            else
            {
                ViewBag.error = "Filled Username & Password";
                return View();
            }
          
        }
        public ActionResult Dashboard()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var fullName = User.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
            var profilePic = User.Claims.FirstOrDefault(c => c.Type == "ProfilePic")?.Value;

            ViewBag.userrole = userRole;
            if (userRole != null && userRole.Equals("Admin"))
            {
                var users = db.TblUsers.ToList();  // Retrieve all users for Admin

                var viewModel = new DashBoardModel
                {
                    Users = users, // No need to populate this for non-admin
                    Resumes = null,
                    Designations = null,
                    UserData = null,
                    Resumesdata = null
                };

                return View(viewModel);
            }
            else
            {
                var query = db.TblUsers.FirstOrDefault(u => u.Email == User.Identity.Name);
                var viewModel = new DashBoardModel
                {
                    Users = null, // No need to populate this for non-admin
                    Resumes = null,
                    Designations = null,
                    UserData = query,
                    Resumesdata = null

                };

                return View(viewModel);
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var query = db.TblUsers.FirstOrDefault(u => u.Email == User.Identity.Name);
            var viewModel = new DashBoardModel
            {
                Users = null, // No need to populate this for non-admin
                Resumes = null,
                Designations = null,
                UserData = query,
                Resumesdata = null

            };

            return View(viewModel);
        }
        public IActionResult DefaultPage()
        {
            return View();
        }
        public async Task<IActionResult> UserData(int page = 1, int pageSize = 10)
        {
            // Get resumes associated with the logged-in user
            var userEmail = User.Identity.Name;
            var resumesQuery = db.TblResumes.Where(r => r.Email == userEmail);

            // Pagination: Calculate skip and take based on page number and page size
            var totalResumes = resumesQuery.Count();
            var resumes = resumesQuery.OrderBy(r => r.Id)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            var designation = db.TblDesignations.ToList();

            var viewModel = new DashBoardModel
            {
                Users = null,
                UserData = null,
                Resumes = null,
                Designations = designation,
                Resumesdata = resumes,
                CurrentPage = page,
                PageSize = pageSize,
                TotalResumes = totalResumes

            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResume(DataModel model)
        {
            if (model.Email != null && model.Designation != null && model.Resumes != null && model.Title != null && model.KeyWords != null && model.Status != null)
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in model.Resumes)
                    {
                        if (item.FileName.EndsWith(".pdf"))
                        {
                            //Get the directory path based on designation
                            var dirpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",model.Designation);
                            if (!Directory.Exists(dirpath))
                            {
                                Directory.CreateDirectory(dirpath);
                            }
                            var filname = item.FileName;
                            var filePath = Path.Combine(dirpath, filname);
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await item.CopyToAsync(stream);
                            }
                            var tbl_resume = new TblResume();
                            {
                                tbl_resume.Email = model.Email;
                                tbl_resume.Designation = model.Designation;
                                tbl_resume.Resume = filname;
                                tbl_resume.Title = model.Title;
                                tbl_resume.KeyWords = model.KeyWords;
                                tbl_resume.Status = model.Status;
                                tbl_resume.UploadedBy = model.UploadedBy;
                                tbl_resume.FilePath = filePath;
                            }     // Create new Product instance

                            db.TblResumes.Add(tbl_resume);
                            db.SaveChanges();
                        }
                        else
                        {
                            ViewBag.msg = "Only Pdf File Acceptable..";
                            // return View();
                        }
                    }
                    ViewBag.msg = "Record Added";
                    Task.WaitAll(Task.Delay(2000));
                    return RedirectToAction("UserData");
                }
                else
                {
                    ViewBag.msg = "Something Wrong";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "All Fields Required";
                return View();
            }
        }
       
        public async Task<IActionResult> EditResume(int?id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblResume = await db.TblResumes.FindAsync(id);
            ArrayList list = new ArrayList();
            list.Add(tblResume.Title);
            list.Add(tblResume.Designation);
            list.Add(tblResume.KeyWords);
            list.Add(tblResume.FilePath);
            list.Add(tblResume.Resume);
            list.Add(tblResume.Id);
           
            return Json(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateResume(DataModel model)
        {
            if (model.Id!=null&&model.Email != null && model.Designation != null && model.Title != null && model.KeyWords != null && model.Status != null)
            {
                if (ModelState.IsValid)
                {
                    TblResume tbl_resume = db.TblResumes.Find(model.Id);
                        //tbl_resume.Id = model.Id;
                        tbl_resume.Email = model.Email;
                        tbl_resume.Designation = model.Designation;
                        tbl_resume.Title = model.Title;
                        tbl_resume.KeyWords = model.KeyWords;
                        tbl_resume.Status = model.Status;
                        tbl_resume.UploadedBy = model.UploadedBy;

                    db.Entry(tbl_resume).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msg = "Record Updated";
                    Task.WaitAll(Task.Delay(2000));
                    return RedirectToAction("UserData");
                }
                else
                {
                    ViewBag.msg = "Something Wrong";
                    return View();
                }
            }
            else
            {
                ViewBag.msg = "All Fields Required";
                return View();
            }
        }

        public IActionResult ChangeStatusAllresume(int? id)
        {
            TblResume tbl_resume = db.TblResumes.Find(id);
            if (tbl_resume.Status.Equals(true))
            {
                tbl_resume.Status = false;
            }
            else
            {
                tbl_resume.Status = true;
            }
            db.Entry(tbl_resume).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllResumeData");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string Username, string password, string cpassword, string opassword)
        {

            if (string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "New password cannot be empty.";
                ModelState.AddModelError("newPassword", "New password cannot be empty.");
                return View();
            }
            if (password == cpassword)
            {
                using (var context = new ResumePortalArivaniContext())
                {
                    var user = context.TblUsers.FirstOrDefault(u => u.Email == Username);
                    if (user != null)
                    {
                        if (user.Password != opassword)
                        {
                            ViewBag.Message = "Check Old Password";
                            return View();
                        }
                        else
                        {
                            user.Password = password;
                            context.SaveChanges();

                            ViewBag.Message = "Password changed successfully";
                            return RedirectToAction("Login");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "User not found.");
                        ViewBag.Message = "Email not found.";
                        return View(); // Return to the view indicating user not found
                    }
                }
            }
            else
            {
                ViewBag.Message = "Password Not Matched";
                return View();
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string Email)
        {
            using (var context = new ResumePortalArivaniContext())
            {
                var user = context.TblUsers.FirstOrDefault(u => u.Email == Email);
                if (user != null)
                {
                   // new Claim("Email", user.Email);
                   HttpContext.Session.SetString(UserMail, user.Email);
                    Random rnd = new Random();

                   // new Claim("otp", (rnd.Next(100000, 999999)).ToString());
                    HttpContext.Session.SetString(otp, (rnd.Next(100000, 999999)).ToString());
                    //Session["otp"] = (rnd.Next(100000, 999999)).ToString();
                    MailMessage message = new MailMessage("nileshsrivastav9422@gmail.com", Email);
                    message.Subject = "Your Otp from Resume Portal";
                    message.Body = $"Hello {user.Name}<br/> your otp is {HttpContext.Session.GetString(otp)}\n\n <br/>Thank you";
                    message.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("nileshsrivastav9422@gmail.com", "lemu bbcy pydu cixz");
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                    ViewBag.message = "Otp is Send Your Email..";
                    return RedirectToAction("VerifyOTP");
                }
                else
                {
                    ViewBag.message = "Email is Not Resistered";
                    return View();
                }
            }


        }
        public ActionResult VerifyOTP()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyOTP(string OTP)
        {
            if (HttpContext.Session.GetString(otp).Equals(OTP))
            {
                ViewBag.message = "OTP Verified";
                return RedirectToAction("forgotform");
            }
            else
            {
                ViewBag.message = "Not Matched";
                return View();
            }
        }
        public ActionResult forgotform()
        {
            ViewBag.val = HttpContext.Session.GetString(UserMail).ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult forgotform(string password, string cpassword, string username)
        {
            if (password == cpassword)
            {
                using (var context = new ResumePortalArivaniContext())
                {
                    var user = context.TblUsers.FirstOrDefault(u => u.Email == username);
                    if (user != null)
                    {


                        user.Password = password;
                        context.SaveChanges();

                        ViewBag.Message = "Password changed successfully";
                        return RedirectToAction("Login");

                    }
                    else
                    {
                        ModelState.AddModelError("Username", "User not found.");
                        ViewBag.Message = "Something Mismatch.";
                        return View(); // Return to the view indicating user not found
                    }
                }
            }
            else
            {
                ViewBag.Message = "Password Not Matched";
                return View();
            }
        }
    }
}

using Biodent.DataAccess;
using Biodent.Models;
using Biodent.Web.Common;
using Biodent.Web.Hubs;
using Biodent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;

namespace Biodent.Web.Controllers
{
	public class ChatsController : Controller
	{
		UsersDAL _users;
		MessageDAL _message;
		int usersid = 0;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatsController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _users = new UsersDAL();
            _message = new MessageDAL();
        }

		public IActionResult Index()
		{
			var users = _users.GetAllClinic();
			return View(users);
		}
		public IActionResult Create(int id)
		{
			usersid = id;
			ChatingModel chats = new ChatingModel();
			chats.message.ToUsersId = usersid;
            chats.Messages = _message.GetByUsersId(usersid);

            return View(chats);
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Create(ChatingModel chats)
		{
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_CurrentUser);

            chats.message.FromUsersId = currentUser.UsersID;
			chats.message.TextDate = DateTime.Now;

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", currentUser.UsersName, chats.message.MessageText);
            _message.Insert(chats.message);
            //return View();
            return RedirectToAction("Create");
		}

        public IActionResult ChatBox(int id)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            ChatingModel chats = new ChatingModel();
            chats.Messages = _message.GetByUsersId(currentUser.UsersID);
            chats.message.FromUsersId = id;
            return View(chats);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChatBox(ChatingModel chats)
        {
            var currentUser = HttpContext.Session.GetObject<UsersModel>(SystemConstants.Session_PublicCurrentUser);
            chats.message.ToUsersId = currentUser.UsersID;
            chats.message.TextDate = DateTime.Now;
            _message.Insert(chats.message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", currentUser.UsersName, chats.message.MessageText);

            //return View();
            return RedirectToAction("ChatBox");
        }

        public IActionResult ChatList()
        {
            var users = _users.GetAdminList();
            return View(users);
        }
    }
}

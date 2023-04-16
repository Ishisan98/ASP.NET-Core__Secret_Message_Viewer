using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secret_Message_Viewer.Data;
using Secret_Message_Viewer.Models;

namespace Secret_Message_Viewer.Controllers
{
    public class MessageController : Controller
    {
        private AppSettingsDbContext _context;                  //any name can be given for this service, eg dbContext, _dbContext

        public MessageController(AppSettingsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // Create Secret Message //
        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save (Message message)
        {
            if (message != null)
            {
                string msg = message.Text;

                // Start from here
                
                MsgAttribute newMsgAttribute = new MsgAttribute();  
                newMsgAttribute.UnixTimeS = DateTimeOffset.Now.ToUnixTimeSeconds();
                newMsgAttribute.Destroyed = false;
                _context.MessageAttributes.Add(newMsgAttribute);                        //create a new row or a new object of MsgAttribute in database

                Message newMessage = new Message();
                newMessage.Text = msg;
                newMessage.MsgAttribute = newMsgAttribute;                              //linked with newMsgAttribute (one to one rs)                             
                _context.Messages.Add(newMessage);

                _context.SaveChanges();                                                 //update database

                ViewData["key"] = newMessage.Id;
                return View();
            }
            return View("Create");
        }




        // Redeem Secret Code //
        public IActionResult Redeem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Read (Message message)         //this message obj is used as form, not an obj from database
        {
            if (message != null)
            {
                Guid key = message.Id;
                string msg = null;

                //Start from here
                Message messageFromDb = _context.Messages.Include(m => m.MsgAttribute).FirstOrDefault(x => x.Id == key);       //retrieving message obj from "Messages" Table inside database using Id where Id = key
                if (messageFromDb != null)
                {
                    messageFromDb.MsgAttribute.Destroyed = true;        // Upon successful retrieval of the secret message, mark it as destroyed
                    _context.SaveChanges();

                    ViewData["text"] = messageFromDb.Text;
                    return View();
                }
                else
                {
                    ViewData["text"] = "Message is not available";
                    return View();
                }
            }
            return View();
        }
    }
}

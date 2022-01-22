using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A2.Data;
using A2.Models;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using A2.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace A2.Controllers
{
    [Route("api")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IWebAPIRepo _repository;

        public StaffController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult Register(User user)
        {
            IEnumerable<User> users = _repository.GetUsers();
            User res = users.FirstOrDefault(u => u.UserName == user.UserName);

            if (res != null)
            {
                return Ok("Username not available.");
            }

            User c = new User
            {
                Address = user.Address,
                Password = user.Password,
                UserName = user.UserName
            };
            User addedUser = _repository.Register(c);
            return Ok("User successfully registered.");
        }

        [Authorize(AuthenticationSchemes="BasicAuth")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("GetVersionA")]
        public ActionResult<String> GetVersionA()
        {
            return Ok("v1");
        }

        [Authorize(AuthenticationSchemes = "BasicAuth")]
        [Authorize(Policy = "UserOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("PurchaseItem")]
        public ActionResult PurchaseItem(OrderInDTO order)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string userName = c.Value;

            Order addedOrder = new Order
            {
                UserName = userName,
                ProductID = order.ProductID,
                Quantity = order.Quantity
            };

            Order added = _repository.PurchaseItem(addedOrder);
            return CreatedAtAction("PurchaseItem", added);
        }

        [Authorize(AuthenticationSchemes = "BasicAuth")]
        [Authorize(Policy = "UserOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpGet("PurchaseSingleItem/{productId}")]
        public ActionResult PurchaseSingleItem(string productId)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("userName");
            string userName = c.Value;

            Order addedOrder = new Order
            {
                UserName = userName,
                ProductID = int.Parse(productId),
                Quantity = 1
            };

            Order added = _repository.PurchaseItem(addedOrder);
            return CreatedAtAction("PurchaseSingleItem", added);
        }
    }
}
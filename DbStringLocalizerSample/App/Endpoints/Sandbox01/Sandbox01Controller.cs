using DbStringLocalizerSample.App.Endpoints.Sandbox01.Spec;
using DbStringLocalizerSample.Dummy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace DbStringLocalizerSample.App.Endpoints.Sandbox01
{
    [ApiController]
    [Route("api/sandbox01")]
    public class Sandbox01Controller : ControllerBase
    {
        private readonly IStringLocalizer<Item> _itemLocalizer;
        private readonly IStringLocalizer<Message> _messageLocalizer;

        public Sandbox01Controller(IStringLocalizer<Item> itemLocalizer, IStringLocalizer<Message> messageLocalizer)
        {
            _itemLocalizer = itemLocalizer;
            _messageLocalizer = messageLocalizer;
        }

        [HttpGet("message01")]
        public IActionResult Message01()
        {
            string item = _itemLocalizer["Item01"];
            string mes = _messageLocalizer["M0001", item];
            return Content(mes);
        }

        [HttpGet("message02")]
        public IActionResult Message02()
        {
            string mes = _messageLocalizer["M0002"];
            return Content(mes);
        }

        [HttpGet("all-items")]
        public IActionResult AllItems()
        {
            string strings = string.Join(", ", _itemLocalizer.GetAllStrings().Select(x => (string)x));
            return Content(strings);
        }

        [HttpGet("all-messages")]
        public IActionResult AllMessages()
        {
            string strings = string.Join(", ", _messageLocalizer.GetAllStrings().Select(x => (string)x));
            return Content(strings);
        }

        /// <summary>
        /// カルチャーの変更
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("change-culture")]
        public IActionResult ChangeCulture([FromBody] ChangeCultureRequest request)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(request.CultureName))
            );
            return Ok();
        }

    }
}

using System.Linq;
using System.Web;
using System.Web.Http;
using BLL;

namespace Web2.Controllers
{
    public class ListingsApiController : ApiController
    {
        // GET api/listings
        public IHttpActionResult Get()
        {
            var listings = Listing.GetAll();

            var result = listings.Select(l => new
            {
                id = l.Id.ToString(),
                cardName = l.CardName,
                cardGame = l.CardGame,
                cardImageUrl = l.CardImageUrl,
                photoUrl = string.IsNullOrEmpty(l.PhotoUrl) ? null : VirtualPathUtility.ToAbsolute(l.PhotoUrl),
                notes = l.Notes,
                quantity = l.Quantity,
                sellerName = l.SellerName,
                sellerPhone = l.SellerPhone,
                sellerEmail = l.SellerEmail
            });

            return Ok(result);
        }
    }
}
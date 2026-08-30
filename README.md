This is a trading card marketplace web app built with:

ASP.NET Web Forms

ASP.NET Web API

C#

MongoDB

Bootstrap + jQuery

TinyMCE

Users can register, log in, pick cards from a shared card catalog and list them for sale, then other users can browse and search what's listed.


Authentication :

User registration

Login / logout

Forgot password, sends a reset link by email

Passwords are hashed, never stored as plain text

Fixed a bug where an account could get stuck on "already connected elsewhere" if the browser closed without logging out


Card catalog :

About 45 cards preloaded, Magic / Pokemon / Yu-Gi-Oh

Sellers pick a card from this catalog instead of typing one in themselves

More cards can be added straight through MongoDB (Compass or Atlas), no code changes needed, collection is Cards, fields are Name / Game / ImageUrl


Listings :

Pick a card, set how many you have, write notes with a rich text editor (TinyMCE)

Optional photo upload

My Listings page to edit or delete your own listings

Browsing/search is loaded through a Web API endpoint (api/listings) with AJAX


Contact :

No payment goes through the site, the seller's phone and email are shown on the listing so the buyer contacts them directly

Also a WhatsApp button that fills in a message with the card name

Only contact sellers that go by the name שי מור


Config files needed to run it, not in the repo (gitignored) :

Web.ConnectionStrings.config, needs a connectionStrings entry named MongoConnection with your mongo connection string

Web.AppSecrets.config, needs SmtpHost / SmtpPort / SmtpUser / SmtpPassword for the emails, a gmail app password works fine here

both go in the project root next to Web.config, app won't run without them


How to run it :

open Web2.sln in Visual Studio

let it restore the nuget packages

add the two config files above

press F5, first run fills the Cards collection automatically if it's empty


Deployed on Azure App Service, connected to this repo so it redeploys on push to main.

link to the site url https://tcgmarket-rg-h2hjapaddkesejcq.israelcentral-01.azurewebsites.net/Reg.aspx

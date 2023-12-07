using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace despesas_backend_api_net_core.Controllers
{
    [Authorize("Bearer")]
    public class AuthController : Controller
    {       
        public AuthController() { }
        protected int IdUsuario
        {
            get
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = HttpContext.Request.Headers["Authorization"].ToString();
                var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
                var idUsuario = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "IdUsuario")?.Value.ToInteger();
                return idUsuario.Equals(null) ? 0 : idUsuario.Value;
            }
        }

        public override AcceptedResult Accepted()
        {
            return base.Accepted();
        }

        public override AcceptedResult Accepted([ActionResultObjectValue] object? value)
        {
            return base.Accepted(value);
        }

        public override AcceptedResult Accepted(Uri uri)
        {
            return base.Accepted(uri);
        }

        public override AcceptedResult Accepted(string? uri)
        {
            return base.Accepted(uri);
        }

        public override AcceptedResult Accepted(string? uri, [ActionResultObjectValue] object? value)
        {
            return base.Accepted(uri, value);
        }

        public override AcceptedResult Accepted(Uri uri, [ActionResultObjectValue] object? value)
        {
            return base.Accepted(uri, value);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName)
        {
            return base.AcceptedAtAction(actionName);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, string? controllerName)
        {
            return base.AcceptedAtAction(actionName, controllerName);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtAction(actionName, value);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, string? controllerName, [ActionResultObjectValue] object? routeValues)
        {
            return base.AcceptedAtAction(actionName, controllerName, routeValues);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtAction(actionName, routeValues, value);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, string? controllerName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtAction(actionName, controllerName, routeValues, value);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute([ActionResultObjectValue] object? routeValues)
        {
            return base.AcceptedAtRoute(routeValues);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(string? routeName)
        {
            return base.AcceptedAtRoute(routeName);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(string? routeName, object? routeValues)
        {
            return base.AcceptedAtRoute(routeName, routeValues);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtRoute(routeValues, value);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(string? routeName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtRoute(routeName, routeValues, value);
        }

        public override BadRequestResult BadRequest()
        {
            return base.BadRequest();
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error)
        {
            return base.BadRequest(error);
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] ModelStateDictionary modelState)
        {
            return base.BadRequest(modelState);
        }

        public override ChallengeResult Challenge()
        {
            return base.Challenge();
        }

        public override ChallengeResult Challenge(params string[] authenticationSchemes)
        {
            return base.Challenge(authenticationSchemes);
        }

        public override ChallengeResult Challenge(AuthenticationProperties properties)
        {
            return base.Challenge(properties);
        }

        public override ChallengeResult Challenge(AuthenticationProperties properties, params string[] authenticationSchemes)
        {
            return base.Challenge(properties, authenticationSchemes);
        }

        public override ConflictResult Conflict()
        {
            return base.Conflict();
        }

        public override ConflictObjectResult Conflict([ActionResultObjectValue] object? error)
        {
            return base.Conflict(error);
        }

        public override ConflictObjectResult Conflict([ActionResultObjectValue] ModelStateDictionary modelState)
        {
            return base.Conflict(modelState);
        }

        public override ContentResult Content(string content)
        {
            return base.Content(content);
        }

        public override ContentResult Content(string content, string contentType)
        {
            return base.Content(content, contentType);
        }

        public override ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            return base.Content(content, contentType, contentEncoding);
        }

        public override ContentResult Content(string content, MediaTypeHeaderValue? contentType)
        {
            return base.Content(content, contentType);
        }

        public override CreatedResult Created(string uri, [ActionResultObjectValue] object? value)
        {
            return base.Created(uri, value);
        }
        public override CreatedResult Created(Uri uri, [ActionResultObjectValue] object? value)
        {
            return base.Created(uri, value);
        }

        public override CreatedAtActionResult CreatedAtAction(string? actionName, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtAction(actionName, value);
        }

        public override CreatedAtActionResult CreatedAtAction(string? actionName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtAction(actionName, routeValues, value);
        }

        public override CreatedAtActionResult CreatedAtAction(string? actionName, string? controllerName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtAction(actionName, controllerName, routeValues, value);
        }

        public override CreatedAtRouteResult CreatedAtRoute(string? routeName, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtRoute(routeName, value);
        }

        public override CreatedAtRouteResult CreatedAtRoute(object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtRoute(routeValues, value);
        }

        public override CreatedAtRouteResult CreatedAtRoute(string? routeName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtRoute(routeName, routeValues, value);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override FileContentResult File(byte[] fileContents, string contentType)
        {
            return base.File(fileContents, contentType);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, enableRangeProcessing);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName)
        {
            return base.File(fileContents, contentType, fileDownloadName);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileContents, contentType, lastModified, entityTag);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileContents, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType)
        {
            return base.File(fileStream, contentType);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName)
        {
            return base.File(fileStream, contentType, fileDownloadName);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileStream, contentType, lastModified, entityTag);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileStream, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType)
        {
            return base.File(virtualPath, contentType);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName)
        {
            return base.File(virtualPath, contentType, fileDownloadName);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(virtualPath, contentType, lastModified, entityTag);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(virtualPath, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override ForbidResult Forbid()
        {
            return base.Forbid();
        }

        public override ForbidResult Forbid(params string[] authenticationSchemes)
        {
            return base.Forbid(authenticationSchemes);
        }

        public override ForbidResult Forbid(AuthenticationProperties properties)
        {
            return base.Forbid(properties);
        }

        public override ForbidResult Forbid(AuthenticationProperties properties, params string[] authenticationSchemes)
        {
            return base.Forbid(properties, authenticationSchemes);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override JsonResult Json(object? data)
        {
            return base.Json(data);
        }

        public override JsonResult Json(object? data, object? serializerSettings)
        {
            return base.Json(data, serializerSettings);
        }

        public override LocalRedirectResult LocalRedirect(string localUrl)
        {
            return base.LocalRedirect(localUrl);
        }

        public override LocalRedirectResult LocalRedirectPermanent(string localUrl)
        {
            return base.LocalRedirectPermanent(localUrl);
        }

        public override LocalRedirectResult LocalRedirectPermanentPreserveMethod(string localUrl)
        {
            return base.LocalRedirectPermanentPreserveMethod(localUrl);
        }

        public override LocalRedirectResult LocalRedirectPreserveMethod(string localUrl)
        {
            return base.LocalRedirectPreserveMethod(localUrl);
        }

        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }

        public override NotFoundResult NotFound()
        {
            return base.NotFound();
        }

        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object? value)
        {
            return base.NotFound(value);
        }

        public override OkResult Ok()
        {
            return base.Ok();
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            return base.Ok(value);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override PartialViewResult PartialView()
        {
            return base.PartialView();
        }

        public override PartialViewResult PartialView(string? viewName)
        {
            return base.PartialView(viewName);
        }

        public override PartialViewResult PartialView(object? model)
        {
            return base.PartialView(model);
        }

        public override PartialViewResult PartialView(string? viewName, object? model)
        {
            return base.PartialView(viewName, model);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType)
        {
            return base.PhysicalFile(physicalPath, contentType);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.PhysicalFile(physicalPath, contentType, lastModified, entityTag);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override ObjectResult Problem(string? detail = null, string? instance = null, int? statusCode = null, string? title = null, string? type = null)
        {
            return base.Problem(detail, instance, statusCode, title, type);
        }

        public override RedirectResult Redirect(string url)
        {
            return base.Redirect(url);
        }

        public override RedirectResult RedirectPermanent(string url)
        {
            return base.RedirectPermanent(url);
        }

        public override RedirectResult RedirectPermanentPreserveMethod(string url)
        {
            return base.RedirectPermanentPreserveMethod(url);
        }

        public override RedirectResult RedirectPreserveMethod(string url)
        {
            return base.RedirectPreserveMethod(url);
        }

        public override RedirectToActionResult RedirectToAction()
        {
            return base.RedirectToAction();
        }

        public override RedirectToActionResult RedirectToAction(string? actionName)
        {
            return base.RedirectToAction(actionName);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, object? routeValues)
        {
            return base.RedirectToAction(actionName, routeValues);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName)
        {
            return base.RedirectToAction(actionName, controllerName);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName, object? routeValues)
        {
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName, string? fragment)
        {
            return base.RedirectToAction(actionName, controllerName, fragment);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName, object? routeValues, string? fragment)
        {
            return base.RedirectToAction(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName)
        {
            return base.RedirectToActionPermanent(actionName);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, object? routeValues)
        {
            return base.RedirectToActionPermanent(actionName, routeValues);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName)
        {
            return base.RedirectToActionPermanent(actionName, controllerName);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName, string? fragment)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, fragment);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName, object? routeValues)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName, object? routeValues, string? fragment)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToActionResult RedirectToActionPermanentPreserveMethod(string? actionName = null, string? controllerName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToActionPermanentPreserveMethod(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToActionResult RedirectToActionPreserveMethod(string? actionName = null, string? controllerName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToActionPreserveMethod(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPage(string pageName)
        {
            return base.RedirectToPage(pageName);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, object? routeValues)
        {
            return base.RedirectToPage(pageName, routeValues);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler)
        {
            return base.RedirectToPage(pageName, pageHandler);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler, object? routeValues)
        {
            return base.RedirectToPage(pageName, pageHandler, routeValues);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler, string? fragment)
        {
            return base.RedirectToPage(pageName, pageHandler, fragment);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler, object? routeValues, string? fragment)
        {
            return base.RedirectToPage(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName)
        {
            return base.RedirectToPagePermanent(pageName);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, object? routeValues)
        {
            return base.RedirectToPagePermanent(pageName, routeValues);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, string? pageHandler)
        {
            return base.RedirectToPagePermanent(pageName, pageHandler);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, string? pageHandler, string? fragment)
        {
            return base.RedirectToPagePermanent(pageName, pageHandler, fragment);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, string? pageHandler, object? routeValues, string? fragment)
        {
            return base.RedirectToPagePermanent(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPagePermanentPreserveMethod(string pageName, string? pageHandler = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToPagePermanentPreserveMethod(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPagePreserveMethod(string pageName, string? pageHandler = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToPagePreserveMethod(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName)
        {
            return base.RedirectToRoute(routeName);
        }

        public override RedirectToRouteResult RedirectToRoute(object? routeValues)
        {
            return base.RedirectToRoute(routeValues);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName, object? routeValues)
        {
            return base.RedirectToRoute(routeName, routeValues);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName, string? fragment)
        {
            return base.RedirectToRoute(routeName, fragment);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName, object? routeValues, string? fragment)
        {
            return base.RedirectToRoute(routeName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName)
        {
            return base.RedirectToRoutePermanent(routeName);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(object? routeValues)
        {
            return base.RedirectToRoutePermanent(routeValues);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName, object? routeValues)
        {
            return base.RedirectToRoutePermanent(routeName, routeValues);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName, string? fragment)
        {
            return base.RedirectToRoutePermanent(routeName, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName, object? routeValues, string? fragment)
        {
            return base.RedirectToRoutePermanent(routeName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePermanentPreserveMethod(string? routeName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToRoutePermanentPreserveMethod(routeName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePreserveMethod(string? routeName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToRoutePreserveMethod(routeName, routeValues, fragment);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal)
        {
            return base.SignIn(principal);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal, string authenticationScheme)
        {
            return base.SignIn(principal, authenticationScheme);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return base.SignIn(principal, properties);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal, AuthenticationProperties properties, string authenticationScheme)
        {
            return base.SignIn(principal, properties, authenticationScheme);
        }

        public override SignOutResult SignOut()
        {
            return base.SignOut();
        }

        public override SignOutResult SignOut(AuthenticationProperties properties)
        {
            return base.SignOut(properties);
        }

        public override SignOutResult SignOut(params string[] authenticationSchemes)
        {
            return base.SignOut(authenticationSchemes);
        }

        public override SignOutResult SignOut(AuthenticationProperties properties, params string[] authenticationSchemes)
        {
            return base.SignOut(properties, authenticationSchemes);
        }

        public override StatusCodeResult StatusCode([ActionResultStatusCode] int statusCode)
        {
            return base.StatusCode(statusCode);
        }

        public override ObjectResult StatusCode([ActionResultStatusCode] int statusCode, [ActionResultObjectValue] object? value)
        {
            return base.StatusCode(statusCode, value);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public override Task<bool> TryUpdateModelAsync<TModel>(TModel model)
        {
            return base.TryUpdateModelAsync(model);
        }

        public override Task<bool> TryUpdateModelAsync<TModel>(TModel model, string prefix)
        {
            return base.TryUpdateModelAsync(model, prefix);
        }

        public override Task<bool> TryUpdateModelAsync<TModel>(TModel model, string prefix, IValueProvider valueProvider)
        {
            return base.TryUpdateModelAsync(model, prefix, valueProvider);
        }

        public override Task<bool> TryUpdateModelAsync(object model, Type modelType, string prefix)
        {
            return base.TryUpdateModelAsync(model, modelType, prefix);
        }

        public override bool TryValidateModel(object model)
        {
            return base.TryValidateModel(model);
        }

        public override bool TryValidateModel(object model, string? prefix)
        {
            return base.TryValidateModel(model, prefix);
        }

        public override UnauthorizedResult Unauthorized()
        {
            return base.Unauthorized();
        }

        public override UnauthorizedObjectResult Unauthorized([ActionResultObjectValue] object? value)
        {
            return base.Unauthorized(value);
        }

        public override UnprocessableEntityResult UnprocessableEntity()
        {
            return base.UnprocessableEntity();
        }

        public override UnprocessableEntityObjectResult UnprocessableEntity([ActionResultObjectValue] object? error)
        {
            return base.UnprocessableEntity(error);
        }

        public override UnprocessableEntityObjectResult UnprocessableEntity([ActionResultObjectValue] ModelStateDictionary modelState)
        {
            return base.UnprocessableEntity(modelState);
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ValidationProblemDetails descriptor)
        {
            return base.ValidationProblem(descriptor);
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            return base.ValidationProblem(modelStateDictionary);
        }

        public override ActionResult ValidationProblem()
        {
            return base.ValidationProblem();
        }

        public override ActionResult ValidationProblem(string? detail = null, string? instance = null, int? statusCode = null, string? title = null, string? type = null, [ActionResultObjectValue] ModelStateDictionary? modelStateDictionary = null)
        {
            return base.ValidationProblem(detail, instance, statusCode, title, type, modelStateDictionary);
        }

        public override ViewResult View()
        {
            return base.View();
        }

        public override ViewResult View(string? viewName)
        {
            return base.View(viewName);
        }

        public override ViewResult View(object? model)
        {
            return base.View(model);
        }

        public override ViewResult View(string? viewName, object? model)
        {
            return base.View(viewName, model);
        }

        public override ViewComponentResult ViewComponent(string componentName)
        {
            return base.ViewComponent(componentName);
        }

        public override ViewComponentResult ViewComponent(Type componentType)
        {
            return base.ViewComponent(componentType);
        }

        public override ViewComponentResult ViewComponent(string componentName, object? arguments)
        {
            return base.ViewComponent(componentName, arguments);
        }

        public override ViewComponentResult ViewComponent(Type componentType, object? arguments)
        {
            return base.ViewComponent(componentType, arguments);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}

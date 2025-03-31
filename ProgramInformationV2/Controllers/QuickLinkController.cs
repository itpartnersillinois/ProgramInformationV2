using Microsoft.AspNetCore.Mvc;
using ProgramInformationV2.Data.Cache;
using ProgramInformationV2.Data.DataHelpers;

namespace ProgramInformationV2.Controllers {

    [Route("[controller]")]
    public class QuickLinkController(CacheHolder cacheHolder, SecurityHelper securityHelper) : Controller {
        private readonly CacheHolder _cacheHolder = cacheHolder;
        private readonly SecurityHelper _securityHelper = securityHelper;

        [Route("course/{id}")]
        [HttpGet]
        public async Task<IActionResult> Course(string id) {
            var result = await Set(id);
            return string.IsNullOrWhiteSpace(result) ? Redirect("/course/general") : Content(result);
        }

        [Route("credential/{id}")]
        [HttpGet]
        public async Task<IActionResult> Credential(string id) {
            var result = await Set(id);
            return string.IsNullOrWhiteSpace(result) ? Redirect("/credential/general") : Content(result);
        }

        [Route("program/{id}")]
        [HttpGet]
        public async Task<IActionResult> Program(string id) {
            var result = await Set(id);
            return string.IsNullOrWhiteSpace(result) ? Redirect("/program/general") : Content(result);
        }

        [Route("requirementset/{id}")]
        [HttpGet]
        public async Task<IActionResult> RequirementSet(string id) {
            var result = await Set(id);
            return string.IsNullOrWhiteSpace(result) ? Redirect("/requirementset/general") : Content(result);
        }

        [Route("section/{id}")]
        [HttpGet]
        public async Task<IActionResult> Section(string id) {
            var result = await Set(id);
            return string.IsNullOrWhiteSpace(result) ? Redirect("/section/general") : Content(result);
        }

        private async Task<string> Set(string id) {
            if (string.IsNullOrEmpty(id)) {
                return "Error: ID needs to be added";
            }
            var netId = User.Identities.FirstOrDefault()?.Name;
            if (string.IsNullOrWhiteSpace(netId)) {
                return "Error: Net ID not found";
            }
            var sourceName = id.Split('-')[0];
            if (!await _securityHelper.ConfirmNetIdCanAccessSource(sourceName, netId)) {
                return $"Error: Net ID not allowed for source {sourceName} / {netId}";
            }
            _cacheHolder.SetCacheSource(netId, sourceName);
            _cacheHolder.SetCacheItem(netId, id);
            return "";
        }
    }
}